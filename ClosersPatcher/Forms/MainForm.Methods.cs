/*
 * This file is part of Closers Patcher.
 * Copyright (C) 2016-2017 Miyu, Dramiel Leayal
 * 
 * Closers Patcher is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * Closers Patcher is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with Closers Patcher. If not, see <http://www.gnu.org/licenses/>.
 */

using ClosersPatcher.General;
using ClosersPatcher.Helpers;
using ClosersPatcher.Helpers.GlobalVariables;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Xml;

namespace ClosersPatcher.Forms
{
    internal partial class MainForm
    {
        internal void RestoreFromTray()
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            this.Show();

            this.NotifyIcon.Visible = false;
        }

        internal void ResetTranslation(Language language)
        {
            DeleteTranslationIni(language);
            this.LabelNewTranslations.Text = StringLoader.GetText("form_label_new_translation", language.ToString(), Methods.DateToString(language.LastUpdate));
        }

        private static void DeleteTranslationIni(Language language)
        {
            string iniPath = Path.Combine(language.Path, Strings.IniName.Translation);
            if (Directory.Exists(Path.GetDirectoryName(iniPath)))
            {
                File.Delete(iniPath);
            }
        }

        private static string GetClosersKRPathFromRegistry()
        {
            if (!Environment.Is64BitOperatingSystem)
            {
                string value = Methods.GetRegistryValue(Strings.Registry.KR.RegistryKey, Strings.Registry.KR.Key32Path, Strings.Registry.KR.FolderPath);

                return value;
            }
            else
            {
                string value = Methods.GetRegistryValue(Strings.Registry.KR.RegistryKey, Strings.Registry.KR.Key64Path, Strings.Registry.KR.FolderPath);

                if (value == String.Empty)
                {
                    value = Methods.GetRegistryValue(Strings.Registry.KR.RegistryKey, Strings.Registry.KR.Key32Path, Strings.Registry.KR.FolderPath);
                }

                return value;
            }
        }

        private static string GetClosersJPPathFromRegistry()
        {
            return String.Empty;
        }

        private void InitRegionsConfigData()
        {
            var doc = new XmlDocument();
            doc.Load(Urls.TranslationHome + Strings.IniName.LanguagePack);

            XmlElement configRoot = doc.DocumentElement;
            XmlElement xmlRegions = configRoot[Strings.Xml.Regions];
            int regionCount = xmlRegions.ChildNodes.Count;
            Region[] regions = new Region[regionCount];

            for (int i = 0; i < regionCount; i++)
            {
                XmlNode regionNode = xmlRegions.ChildNodes[i];

                string regionId = regionNode.Name;
                string regionName = StringLoader.GetText(regionNode.Attributes[Strings.Xml.Attributes.Name].Value);
                XmlElement xmlLanguages = regionNode[Strings.Xml.Languages];
                int languageCount = xmlLanguages.ChildNodes.Count;
                Language[] regionLanguages = new Language[languageCount];

                for (int j = 0; j < languageCount; j++)
                {
                    XmlNode languageNode = xmlLanguages.ChildNodes[j];

                    string languageId = languageNode.Name;
                    string languageName = languageNode.Attributes[Strings.Xml.Attributes.Name].Value;
                    string languageDateString = languageNode[Strings.Xml.Value].InnerText;
                    DateTime languageDate = Methods.ParseDate(languageDateString);

                    regionLanguages[j] = new Language(languageId, languageName, languageDate, regionId);
                }

                regions[i] = new Region(regionId, regionName, regionLanguages);
            }

            this.ComboBoxRegions.DataSource = regions.Length > 0 ? regions : null;

            if (this.ComboBoxRegions.DataSource != null)
            {
                if (String.IsNullOrEmpty(UserSettings.RegionId))
                {
                    UserSettings.RegionId = (this.ComboBoxRegions.SelectedItem as Region).Id;
                }
                else
                {
                    int index = this.ComboBoxRegions.Items.IndexOf(new Region(UserSettings.RegionId));
                    this.ComboBoxRegions.SelectedIndex = index == -1 ? 0 : index;
                }

                this.ComboBoxRegions_SelectionChangeCommitted(this, EventArgs.Empty);
            }
        }

        private static string GetSHA256(string filename)
        {
            using (var sha256 = SHA256.Create())
            using (FileStream fs = File.OpenRead(filename))
            {
                return BitConverter.ToString(sha256.ComputeHash(fs)).Replace("-", "");
            }
        }

        private string UploadToPasteBin(string title, string text, PasteBinExpiration expiration, bool isPrivate, string format)
        {
            var client = new PasteBinClient(Strings.PasteBin.DevKey);

            try
            {
                client.Login(Strings.PasteBin.Username, Strings.PasteBin.Password);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            var entry = new PasteBinEntry
            {
                Title = title,
                Text = text,
                Expiration = expiration,
                Private = isPrivate,
                Format = format
            };

            try
            {
                return client.Paste(entry);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                MsgBox.Error(StringLoader.GetText("exception_log_file_failed"));
            }
            finally
            {
                client.Logout();
            }

            return null;
        }

        private static byte[] TrimArrayIfNecessary(byte[] array)
        {
            int limit = 512000 / 2;

            if (array.Length > limit)
            {
                byte[] trimmedArray = new byte[limit];
                Array.Copy(array, array.Length - limit, trimmedArray, 0, limit);

                return trimmedArray;
            }

            return array;
        }

        private void ResetNotifier(object sender, EventArgs e)
        {
            this.LabelNotifier.Text = StringLoader.GetText("form_label_notifier_label_idle");
        }

        internal IEnumerable<string> GetTranslationFolders()
        {
            return this.ComboBoxRegions.Items.Cast<Region>().Select(s => s.ToString());
        }
    }
}

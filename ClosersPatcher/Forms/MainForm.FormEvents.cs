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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ClosersPatcher.Forms
{
    internal partial class MainForm
    {
        private void MainForm_Load(object sender, EventArgs e)
        {
            Language[] languages = GetAvailableLanguages();
            this.ComboBoxLanguages.DataSource = languages.Length > 0 ? languages : null;

            var gamePath = UserSettings.GamePath;
            if (String.IsNullOrEmpty(gamePath) || !Methods.IsClosersPath(gamePath))
            {
                UserSettings.GamePath = GetClosersPathFromRegistry();
            }

            if (this.ComboBoxLanguages.DataSource != null)
            {
                Logger.Info($"Loading languages: {String.Join(" ", languages.Select(l => l.ToString()))}");

                if (String.IsNullOrEmpty(UserSettings.LanguageName))
                {
                    UserSettings.LanguageName = (this.ComboBoxLanguages.SelectedItem as Language).Name;
                }
                else
                {
                    int index = this.ComboBoxLanguages.Items.IndexOf(new Language(UserSettings.LanguageName));
                    this.ComboBoxLanguages.SelectedIndex = index == -1 ? 0 : index;
                }
            }

            Directory.CreateDirectory(Strings.FolderName.Backup);
        }

        private void ForceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Language language = this.ComboBoxLanguages.SelectedItem as Language;

            ResetTranslation(language);

            this.CurrentState = State.Download;
            this.Downloader.Run(this.ComboBoxLanguages.SelectedItem as Language);
        }

        private void OriginalFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.CurrentState = State.Download;
            this.Downloader.Run(null);
        }

        private void ComboBoxLanguages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ComboBoxLanguages.SelectedItem is Language language && Methods.HasNewTranslations(language))
            {
                this.LabelNewTranslations.Text = StringLoader.GetText("form_label_new_translation", language.Name, Methods.DateToString(language.LastUpdate));
            }
            else
            {
                this.LabelNewTranslations.Text = String.Empty;
            }
        }

        private void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm();
            settingsForm.ShowDialog(this);
        }

        private void RefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Language language = this.ComboBoxLanguages.SelectedItem as Language;
            Language[] languages = GetAvailableLanguages();
            this.ComboBoxLanguages.DataSource = languages.Length > 0 ? languages : null;

            if (language != null && this.ComboBoxLanguages.DataSource != null)
            {
                int index = this.ComboBoxLanguages.Items.IndexOf(language);
                this.ComboBoxLanguages.SelectedIndex = index == -1 ? 0 : index;
            }
        }

        private void OpenClosersWebpageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Urls.ClosersHome);
        }

        private void UploadLogToPastebinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!File.Exists(Strings.FileName.Log))
            {
                MsgBox.Error(StringLoader.GetText("exception_log_not_exist"));

                return;
            }
#if DEBUG
            Process.Start(Strings.FileName.Log);
#else
            string logTitle = $"{AssemblyAccessor.Version} ({GetSHA256(Application.ExecutablePath).Substring(0, 12)}) at {Methods.DateToString(DateTime.UtcNow)}";
            byte[] logBytes = File.ReadAllBytes(Strings.FileName.Log);
            logBytes = TrimArrayIfNecessary(logBytes);
            string logText = BitConverter.ToString(logBytes).Replace("-", "");
            var pasteUrl = UploadToPasteBin(logTitle, logText, PasteBinExpiration.OneHour, true, "text");

            if (!String.IsNullOrEmpty(pasteUrl))
            {
                Clipboard.SetText(pasteUrl);
                MsgBox.Success(StringLoader.GetText("success_log_file_upload", pasteUrl));
            }
            else
            {
                MsgBox.Error(StringLoader.GetText("exception_log_file_failed"));
            }
#endif
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.ShowDialog(this);
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.NotifyIcon.Visible = true;
                this.NotifyIcon.ShowBalloonTip(500);

                this.ShowInTaskbar = false;
                this.Hide();
            }
        }

        private void NotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            this.RestoreFromTray();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason.In(CloseReason.ApplicationExitCall, CloseReason.WindowsShutDown))
            {
                Logger.Info($"{this.Text} closing abnormally. Reason=[{e.CloseReason.ToString()}]");
            }
            else if (this.CurrentState != State.Idle)
            {
                MsgBox.Error(StringLoader.GetText("exception_cannot_close", AssemblyAccessor.Title));

                e.Cancel = true;
            }
            else
            {
                Logger.Info($"{this.Text} closing. Reason=[{e.CloseReason.ToString()}]");
                UserSettings.LanguageName = this.ComboBoxLanguages.SelectedIndex == -1 ? null : (this.ComboBoxLanguages.SelectedItem as Language).Name;
            }
        }

        private void ButtonDownload_Click(object sender, EventArgs e)
        {
            switch (this.CurrentState)
            {
                case State.Idle:
                    this.CurrentState = State.Download;
                    this.Downloader.Run(this.ComboBoxLanguages.SelectedItem as Language);

                    break;
                case State.Download:
                    this.ButtonDownload.Text = StringLoader.GetText("button_cancelling");
                    this.Downloader.Cancel();

                    break;
            }
        }

        private void ButtonApplyPatch_Click(object sender, EventArgs e)
        {
            switch (this.CurrentState)
            {
                case State.Idle:
                    this.CurrentState = State.ApplyPatch;
                    this.PatchApplier.Run(this.ComboBoxLanguages.SelectedItem as Language);

                    break;
            }
        }

        private void ButtonRemovePatch_Click(object sender, EventArgs e)
        {
            switch (this.CurrentState)
            {
                case State.Idle:
                    this.CurrentState = State.RemovePatch;
                    this.PatchRemover.Run(this.ComboBoxLanguages.SelectedItem as Language);

                    break;
            }
        }

        private void SpeakerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (UserSettings.HasSound)
            {
                UserSettings.HasSound = false;
                this.SpeakerToolStripMenuItem.Image = Properties.Resources.speaker_off;
            }
            else
            {
                UserSettings.HasSound = true;
                this.SpeakerToolStripMenuItem.Image = Properties.Resources.speaker_on;
            }
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

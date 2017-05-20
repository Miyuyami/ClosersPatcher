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
using System.Windows.Forms;

namespace ClosersPatcher.Forms
{
    internal partial class MainForm
    {
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.InitRegionsConfigData();
        }

        private void ForceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var language = this.ComboBoxLanguages.SelectedItem as Language;

            ResetTranslation(language);

            this.CurrentState = State.Download;
            this.Downloader.Run(this.ComboBoxLanguages.SelectedItem as Language);
        }

        private void OriginalFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.CurrentState = State.Download;
            this.Downloader.Run(this.ComboBoxRegions.SelectedItem as Region);
        }

        private void ComboBoxLanguages_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (this.ComboBoxLanguages.SelectedItem is Language language)
            {
                Logger.Info($"Selected language '{language}'");
                UserSettings.LanguageId = this.ComboBoxLanguages.SelectedIndex == -1 ? null : (this.ComboBoxLanguages.SelectedItem as Language).Id;

                if (Methods.HasNewTranslations(language))
                {
                    this.LabelNewTranslations.Text = StringLoader.GetText("form_label_new_translation", language, Methods.DateToString(language.LastUpdate));
                }
                else
                {
                    this.LabelNewTranslations.Text = String.Empty;
                }
            }
        }

        private void ComboBoxRegions_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (this.ComboBoxRegions.SelectedItem is Region region)
            {
                Logger.Info($"Selected region '{region}'");
                UserSettings.RegionId = this.ComboBoxRegions.SelectedIndex == -1 ? null : (this.ComboBoxRegions.SelectedItem as Region).Id;

                Language[] languages = region.AppliedLanguages;

                this.ComboBoxLanguages.DataSource = languages.Length > 0 ? languages : null;

                if (this.ComboBoxLanguages.DataSource != null)
                {
                    if (String.IsNullOrEmpty(UserSettings.LanguageId))
                    {
                        UserSettings.LanguageId = (this.ComboBoxLanguages.SelectedItem as Language).Id;
                    }
                    else
                    {
                        int index = this.ComboBoxLanguages.Items.IndexOf(new Language(UserSettings.LanguageId));
                        this.ComboBoxLanguages.SelectedIndex = index == -1 ? 0 : index;
                    }

                    this.ComboBoxLanguages_SelectionChangeCommitted(sender, e);
                }

                switch (region.Id)
                {
                    case "kr":
                        UserSettings.GamePath = GetClosersKRPathFromRegistry();

                        break;
                    case "jp":
                        UserSettings.GamePath = GetClosersJPPathFromRegistry();

                        break;
                    default:
                        UserSettings.GamePath = String.Empty;

                        break;
                }

                if (UserSettings.GamePath == String.Empty)
                {
                    this.CurrentState = State.RegionNotInstalled;
                    MsgBox.Error(StringLoader.GetText("exception_game_install_not_found", region.ToString()));
                }
                else
                {
                    this.CurrentState = State.Idle;
                }
            }
        }

        private void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var settingsForm = new SettingsForm();
            settingsForm.ShowDialog(this);
        }

        private void RefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.InitRegionsConfigData();
        }

        private void OpenClosersWebpageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch ((this.ComboBoxRegions.SelectedItem as Region).Id)
            {
                case "kr":
                    Process.Start(Uris.ClosersKRHome);

                    break;
                case "jp":
                    Process.Start(Uris.ClosersJPHome);

                    break;
                default:
                    Process.Start("https://www.google.com/");

                    break;
            }
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
            string pasteUrl = UploadToPasteBin(logTitle, logText, PasteBinExpiration.OneHour, true, "text");

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
            var aboutBox = new AboutBox();
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
                this.CurrentState = State.Idle;
                this.Downloader.Cancel();
                this.PatchApplier.Cancel();
                this.PatchRemover.Cancel();
            }
            else if (!this.CurrentState.In(State.Idle, State.RegionNotInstalled))
            {
                MsgBox.Error(StringLoader.GetText("exception_cannot_close", AssemblyAccessor.Title));

                e.Cancel = true;
            }
            else
            {
                Logger.Info($"{this.Text} closing. Reason=[{e.CloseReason.ToString()}]");
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

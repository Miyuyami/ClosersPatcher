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

using ClosersPatcher.Downloading;
using ClosersPatcher.Helpers;
using ClosersPatcher.Helpers.GlobalVariables;
using ClosersPatcher.Patching;
using MadMilkman.Ini;
using System;
using System.ComponentModel;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace ClosersPatcher.Forms
{
    internal partial class MainForm : Form
    {
        internal enum State
        {
            Idle = 0,
            Download,
            ApplyPatch,
            RemovePatch,
            RegionNotInstalled
        }

        private State _state;
        private readonly Downloader Downloader;
        private readonly PatchApplier PatchApplier;
        private readonly PatchRemover PatchRemover;

        internal State CurrentState
        {
            get
            {
                return this._state;
            }
            private set
            {
                if (this._state != value)
                {
                    switch (value)
                    {
                        case State.Idle:
                            this.ComboBoxLanguages.Enabled = true;
                            this.ComboBoxRegions.Enabled = true;
                            this.ButtonDownload.Enabled = true;
                            this.ButtonDownload.Text = StringLoader.GetText("button_download_translation");
                            this.ButtonApplyPatch.Enabled = true;
                            this.ButtonApplyPatch.Text = StringLoader.GetText("button_apply_patch");
                            this.ButtonRemovePatch.Enabled = true;
                            this.ButtonRemovePatch.Text = StringLoader.GetText("button_remove_patch");
                            this.OriginalFilesToolStripMenuItem.Enabled = true;
                            this.RefreshToolStripMenuItem.Enabled = true;
                            this.ToolStripStatusLabel.Text = StringLoader.GetText("form_status_idle");
                            this.ToolStripProgressBar.Value = this.ToolStripProgressBar.Minimum;
                            this.ToolStripProgressBar.Style = ProgressBarStyle.Blocks;

                            break;
                        case State.Download:
                            this.ComboBoxLanguages.Enabled = false;
                            this.ComboBoxRegions.Enabled = false;
                            this.ButtonDownload.Enabled = true;
                            this.ButtonDownload.Text = StringLoader.GetText("button_cancel");
                            this.ButtonApplyPatch.Enabled = false;
                            this.ButtonApplyPatch.Text = StringLoader.GetText("button_apply_patch");
                            this.ButtonRemovePatch.Enabled = false;
                            this.ButtonRemovePatch.Text = StringLoader.GetText("button_remove_patch");
                            this.OriginalFilesToolStripMenuItem.Enabled = false;
                            this.RefreshToolStripMenuItem.Enabled = false;
                            this.ToolStripStatusLabel.Text = StringLoader.GetText("form_status_download");
                            this.ToolStripProgressBar.Value = this.ToolStripProgressBar.Minimum;
                            this.ToolStripProgressBar.Style = ProgressBarStyle.Blocks;

                            break;
                        case State.ApplyPatch:
                            this.ComboBoxLanguages.Enabled = false;
                            this.ComboBoxRegions.Enabled = false;
                            this.ButtonDownload.Enabled = false;
                            this.ButtonDownload.Text = StringLoader.GetText("button_download_translation");
                            this.ButtonApplyPatch.Enabled = true;
                            this.ButtonApplyPatch.Text = StringLoader.GetText("button_cancel");
                            this.ButtonRemovePatch.Enabled = false;
                            this.ButtonRemovePatch.Text = StringLoader.GetText("button_remove_patch");
                            this.OriginalFilesToolStripMenuItem.Enabled = false;
                            this.RefreshToolStripMenuItem.Enabled = false;
                            this.ToolStripStatusLabel.Text = StringLoader.GetText("form_status_apply_patch");
                            this.ToolStripProgressBar.Value = this.ToolStripProgressBar.Minimum;
                            this.ToolStripProgressBar.Style = ProgressBarStyle.Marquee;

                            break;
                        case State.RemovePatch:
                            this.ComboBoxLanguages.Enabled = false;
                            this.ComboBoxRegions.Enabled = false;
                            this.ButtonDownload.Enabled = false;
                            this.ButtonDownload.Text = StringLoader.GetText("button_download_translation");
                            this.ButtonApplyPatch.Enabled = false;
                            this.ButtonApplyPatch.Text = StringLoader.GetText("button_apply_patch");
                            this.ButtonRemovePatch.Enabled = true;
                            this.ButtonRemovePatch.Text = StringLoader.GetText("button_cancel");
                            this.OriginalFilesToolStripMenuItem.Enabled = false;
                            this.RefreshToolStripMenuItem.Enabled = false;
                            this.ToolStripStatusLabel.Text = StringLoader.GetText("form_status_remove_patch");
                            this.ToolStripProgressBar.Value = this.ToolStripProgressBar.Minimum;
                            this.ToolStripProgressBar.Style = ProgressBarStyle.Marquee;

                            break;
                        case State.RegionNotInstalled:
                            this.ComboBoxLanguages.Enabled = false;
                            this.ComboBoxRegions.Enabled = true;
                            this.ButtonDownload.Enabled = false;
                            this.ButtonDownload.Text = StringLoader.GetText("button_download_translation");
                            this.ButtonApplyPatch.Enabled = false;
                            this.ButtonApplyPatch.Text = StringLoader.GetText("button_apply_patch");
                            this.ButtonRemovePatch.Enabled = false;
                            this.ButtonRemovePatch.Text = StringLoader.GetText("button_remove_patch");
                            this.OriginalFilesToolStripMenuItem.Enabled = false;
                            this.RefreshToolStripMenuItem.Enabled = false;
                            this.ToolStripStatusLabel.Text = StringLoader.GetText("form_status_idle");
                            this.ToolStripProgressBar.Value = this.ToolStripProgressBar.Minimum;
                            this.ToolStripProgressBar.Style = ProgressBarStyle.Blocks;

                            break;
                    }

                    Logger.Info($"State=[{value}]");
                    this.ComboBoxLanguages_SelectionChangeCommitted(this, EventArgs.Empty);
                    this._state = value;
                }
            }
        }

        internal MainForm()
        {
            this.Downloader = new Downloader();
            this.Downloader.DownloaderProgressChanged += this.Downloader_DownloaderProgressChanged;
            this.Downloader.DownloaderCompleted += this.Downloader_DownloaderCompleted;

            this.PatchApplier = new PatchApplier();
            this.PatchApplier.PatchApplierCompleted += this.PatchApplier_PatchApplierCompleted;

            this.PatchRemover = new PatchRemover();
            this.PatchRemover.PatchRemoverCompleted += this.PatchRemover_PatchRemoverCompleted;

            this.InitializeComponent();
            this.InitializeTextComponent();
            this.InitializeComponentAdditionalEvents();
            Logger.Info($"[{this.Text}] starting in UI Language=[{UserSettings.UILanguageCode}]");
        }

        private void InitializeTextComponent()
        {
            this.MenuToolStripMenuItem.Text = StringLoader.GetText("form_menu");
            this.OriginalFilesToolStripMenuItem.Text = StringLoader.GetText("form_original_files");
            this.OpenClosersWebpageToolStripMenuItem.Text = StringLoader.GetText("form_open_closers_webpage");
            this.UploadLogToPastebinToolStripMenuItem.Text = StringLoader.GetText("form_upload_log");
            this.ButtonExit.Text = StringLoader.GetText("button_exit");
            this.SettingsToolStripMenuItem.Text = StringLoader.GetText("form_settings");
            this.RefreshToolStripMenuItem.Text = StringLoader.GetText("form_refresh");
            this.AboutToolStripMenuItem.Text = StringLoader.GetText("form_about");
            this.LabelRegionPick.Text = StringLoader.GetText("form_label_region_pick");
            this.LabelLanguagePick.Text = StringLoader.GetText("form_label_language_pick");
            this.ButtonDownload.Text = StringLoader.GetText("button_download_translation");
            this.ButtonApplyPatch.Text = StringLoader.GetText("button_apply_patch");
            this.ButtonRemovePatch.Text = StringLoader.GetText("button_remove_patch");
            this.LabelNotifier.Text = StringLoader.GetText("form_label_notifier_label_idle");
            this.ToolStripStatusLabel.Text = StringLoader.GetText("form_status_idle");
            this.NotifyIcon.BalloonTipText = StringLoader.GetText("notify_balloon_text");
            this.NotifyIcon.BalloonTipTitle = StringLoader.GetText("notify_balloon_title");
            this.NotifyIcon.Text = StringLoader.GetText("notify_text");
            this.SpeakerToolStripMenuItem.Image = UserSettings.HasSound ? Properties.Resources.speaker_on : Properties.Resources.speaker_off;
            this.Text = AssemblyAccessor.Title + " " + AssemblyAccessor.Version;
        }

        private void InitializeComponentAdditionalEvents()
        {
            this.OriginalFilesToolStripMenuItem.Click += this.ResetNotifier;
            this.OpenClosersWebpageToolStripMenuItem.Click += this.ResetNotifier;
            this.UploadLogToPastebinToolStripMenuItem.Click += this.ResetNotifier;
            this.SettingsToolStripMenuItem.Click += this.ResetNotifier;
            this.RefreshToolStripMenuItem.Click += this.ResetNotifier;
            this.ButtonDownload.Click += this.ResetNotifier;
            this.ButtonApplyPatch.Click += this.ResetNotifier;
            this.ButtonRemovePatch.Click += this.ResetNotifier;
            this.ButtonDownload.Click += this.ResetNotifier;
            this.ComboBoxLanguages.SelectionChangeCommitted += this.ResetNotifier;
            this.ComboBoxRegions.SelectionChangeCommitted += this.ResetNotifier;
        }

        private void Downloader_DownloaderProgressChanged(object sender, DownloaderProgressChangedEventArgs e)
        {
            if (this.CurrentState == State.Download)
            {
                this.ToolStripStatusLabel.Text = $"{StringLoader.GetText("form_status_download")} {e.FileName} ({e.FileNumber}/{e.FileCount})";
                this.ToolStripProgressBar.Value = e.Progress;
            }
        }

        private void Downloader_DownloaderCompleted(object sender, DownloaderCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                Logger.Debug($"{sender.ToString()} cancelled");
            }
            else if (e.Error != null)
            {
                Logger.Error(e.Error);
                MsgBox.Error(Logger.ExeptionParser(e.Error));
            }
            else
            {
                Logger.Debug($"{sender.ToString()} successfuly completed");

                if (e.IsDownloadingInClientFolder)
                {
                    Methods.DeleteBackups(e.Language);
                    this.LabelNotifier.Text = StringLoader.GetText("form_label_notifier_label_original_files_success");
                }
                else
                {
                    string clientIniPath = Path.Combine(UserSettings.GamePath, Strings.IniName.ClientVer);
                    if (!Methods.LoadVerIni(out IniFile clientIni, clientIniPath))
                    {
                        throw new Exception(StringLoader.GetText("exception_generic_read_error", clientIniPath));
                    }
                    IniSection clientVerSection = clientIni.Sections[Strings.IniName.Ver.Section];

                    string translationIniPath = Path.Combine(e.Language.Path, Strings.IniName.Translation);
                    var translationIni = new IniFile();

                    IniKey translationDateKey = new IniKey(translationIni, Strings.IniName.Patcher.KeyDate, Methods.DateToString(e.Language.LastUpdate));
                    IniKey translationRegionKey = new IniKey(translationIni, Strings.IniName.Patcher.KeyRegion, e.Language.ApplyingRegionId);
                    IniSection translationPatcherSection = new IniSection(translationIni, Strings.IniName.Patcher.Section, translationDateKey, translationRegionKey);

                    translationIni.Sections.Add(translationPatcherSection);
                    translationIni.Sections.Add(clientVerSection.Copy(translationIni));
                    translationIni.Save(translationIniPath);
                }

                if (UserSettings.HasSound)
                {
                    using (SoundPlayer player = new SoundPlayer(Properties.Resources.notification_download_completed))
                    {
                        player.Play();
                    }
                }
            }

            GC.Collect();
            this.CurrentState = State.Idle;
        }

        private void PatchApplier_PatchApplierCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                Logger.Debug($"{sender.ToString()} cancelled.");
            }
            else if (e.Error != null)
            {
                Logger.Error(e.Error);
                MsgBox.Error(e.Error.Message);
            }
            else if (e.Result != null && Convert.ToBoolean(e.Result))
            {
                MsgBox.Notice(StringLoader.GetText("notice_outdated_translation"));
                ForceToolStripMenuItem_Click(sender, e);

                return;
            }
            else
            {
                this.LabelNotifier.Text = StringLoader.GetText("form_label_notifier_label_apply_patch_success");
            }

            this.CurrentState = State.Idle;
        }

        private void PatchRemover_PatchRemoverCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                Logger.Debug($"{sender.ToString()} cancelled.");
            }
            else if (e.Error != null)
            {
                Logger.Error(e.Error);
                MsgBox.Error(e.Error.Message);
            }
            else
            {
                this.LabelNotifier.Text = StringLoader.GetText("form_label_notifier_label_remove_patch_success");
            }

            this.CurrentState = State.Idle;
        }
    }
}

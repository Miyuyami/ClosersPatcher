/*
 * This file is part of Soulworker Patcher.
 * Copyright (C) 2016-2017 Miyu, Dramiel Leayal
 * 
 * Soulworker Patcher is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * Soulworker Patcher is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with Soulworker Patcher. If not, see <http://www.gnu.org/licenses/>.
 */

using ClosersPatcher.Downloading;
using ClosersPatcher.General;
using ClosersPatcher.Helpers;
using ClosersPatcher.Helpers.GlobalVariables;
using ClosersPatcher.Launching;
using MadMilkman.Ini;
using System;
using System.ComponentModel;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace ClosersPatcher.Forms
{
    public partial class MainForm : Form
    {
        public enum State
        {
            Idle = 0,
            Download,
            Prepare,
            WaitClient,
            WaitClose
        }

        private State _state;
        private readonly Downloader Downloader;
        private readonly GameStarter GameStarter;


        public State CurrentState
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
                            this.ButtonDownload.Enabled = true;
                            this.ButtonDownload.Text = StringLoader.GetText("button_download_translation");
                            this.ButtonPlay.Enabled = true;
                            this.ButtonPlay.Text = StringLoader.GetText("button_play");
                            this.ForceStripMenuItem.Enabled = true;
                            this.RefreshToolStripMenuItem.Enabled = true;
                            this.ToolStripStatusLabel.Text = StringLoader.GetText("form_status_idle");
                            this.ToolStripProgressBar.Value = this.ToolStripProgressBar.Minimum;
                            this.ToolStripProgressBar.Style = ProgressBarStyle.Blocks;
                            break;
                        case State.Download:
                            this.ComboBoxLanguages.Enabled = false;
                            this.ButtonDownload.Enabled = true;
                            this.ButtonDownload.Text = StringLoader.GetText("button_cancel");
                            this.ButtonPlay.Enabled = false;
                            this.ButtonPlay.Text = StringLoader.GetText("button_play");
                            this.ForceStripMenuItem.Enabled = false;
                            this.RefreshToolStripMenuItem.Enabled = false;
                            this.ToolStripStatusLabel.Text = StringLoader.GetText("form_status_download");
                            this.ToolStripProgressBar.Value = this.ToolStripProgressBar.Minimum;
                            this.ToolStripProgressBar.Style = ProgressBarStyle.Blocks;
                            break;
                        case State.Prepare:
                            this.ComboBoxLanguages.Enabled = false;
                            this.ButtonDownload.Enabled = false;
                            this.ButtonDownload.Text = StringLoader.GetText("button_download_translation");
                            this.ButtonPlay.Enabled = false;
                            this.ButtonPlay.Text = StringLoader.GetText("button_play");
                            this.ForceStripMenuItem.Enabled = false;
                            this.RefreshToolStripMenuItem.Enabled = false;
                            this.ToolStripStatusLabel.Text = StringLoader.GetText("form_status_prepare");
                            this.ToolStripProgressBar.Value = this.ToolStripProgressBar.Minimum;
                            this.ToolStripProgressBar.Style = ProgressBarStyle.Marquee;
                            break;
                        case State.WaitClient:
                            this.ComboBoxLanguages.Enabled = false;
                            this.ButtonDownload.Enabled = false;
                            this.ButtonDownload.Text = StringLoader.GetText("button_download_translation");
                            this.ButtonPlay.Enabled = true;
                            this.ButtonPlay.Text = StringLoader.GetText("button_cancel");
                            this.ForceStripMenuItem.Enabled = false;
                            this.RefreshToolStripMenuItem.Enabled = false;
                            this.ToolStripStatusLabel.Text = StringLoader.GetText("form_status_wait_client");
                            this.ToolStripProgressBar.Value = this.ToolStripProgressBar.Minimum;
                            this.ToolStripProgressBar.Style = ProgressBarStyle.Blocks;
                            break;
                        case State.WaitClose:
                            this.ComboBoxLanguages.Enabled = false;
                            this.ButtonDownload.Enabled = false;
                            this.ButtonDownload.Text = StringLoader.GetText("button_download_translation");
                            this.ButtonPlay.Enabled = false;
                            this.ButtonPlay.Text = StringLoader.GetText("button_play");
                            this.ForceStripMenuItem.Enabled = false;
                            this.RefreshToolStripMenuItem.Enabled = false;
                            this.ToolStripStatusLabel.Text = StringLoader.GetText("form_status_wait_close");
                            this.ToolStripProgressBar.Value = this.ToolStripProgressBar.Minimum;
                            this.ToolStripProgressBar.Style = ProgressBarStyle.Marquee;
                            this.WindowState = FormWindowState.Minimized;
                            break;
                    }

                    Logger.Info($"State=[{value}]");
                    this.ComboBoxLanguages_SelectedIndexChanged(this, null);
                    this._state = value;
                }
            }
        }
        
        public MainForm()
        {
            this.Downloader = new Downloader();
            this.Downloader.DownloaderProgressChanged += new DownloaderProgressChangedEventHandler(this.Downloader_DownloaderProgressChanged);
            this.Downloader.DownloaderCompleted += new DownloaderCompletedEventHandler(this.Downloader_DownloaderCompleted);
            this.GameStarter = new GameStarter();
            this.GameStarter.GameStarterProgressChanged += this.GameStarter_GameStarterProgressChanged;
            this.GameStarter.GameStarterCompleted += this.GameStarter_GameStarterCompleted;
            InitializeComponent();
            InitializeTextComponent();
            Logger.Info($"[{this.Text}]");// starting in UI Language=[{UserSettings.UILanguageCode}]");
        }

        private void InitializeTextComponent()
        {
            this.MenuToolStripMenuItem.Text = StringLoader.GetText("form_menu");
            this.SettingsToolStripMenuItem.Text = StringLoader.GetText("form_settings");
            this.RefreshToolStripMenuItem.Text = StringLoader.GetText("form_refresh");
            this.AboutToolStripMenuItem.Text = StringLoader.GetText("form_about");
            this.ForceStripMenuItem.Text = StringLoader.GetText("form_force_patch");
            this.OpenClosersWebpageToolStripMenuItem.Text = StringLoader.GetText("form_open_closers_webpage");
            this.UploadLogToPastebinToolStripMenuItem.Text = StringLoader.GetText("form_upload_log");
            this.ButtonDownload.Text = StringLoader.GetText("button_download_translation");
            this.ButtonPlay.Text = StringLoader.GetText("button_play");
            this.ButtonExit.Text = StringLoader.GetText("button_exit");
            this.NotifyIcon.BalloonTipText = StringLoader.GetText("notify_balloon_text");
            this.NotifyIcon.BalloonTipTitle = StringLoader.GetText("notify_balloon_title");
            this.NotifyIcon.Text = StringLoader.GetText("notify_text");
            this.SpeakerToolStripMenuItem.Image = UserSettings.HasSound ? Properties.Resources.speaker_on : Properties.Resources.speaker_off;
            this.Text = AssemblyAccessor.Title + " " + AssemblyAccessor.Version;
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

                IniFile ini = new IniFile(new IniOptions
                {
                    KeyDuplicate = IniDuplication.Ignored,
                    SectionDuplicate = IniDuplication.Ignored
                });
                ini.Load(Path.Combine(UserSettings.GamePath, Strings.IniName.ClientVer));
                string clientMVer = ini.Sections[Strings.IniName.Ver.Section].Keys[Strings.IniName.Ver.KeyMVer].Value;
                string clientTime = ini.Sections[Strings.IniName.Ver.Section].Keys[Strings.IniName.Ver.KeyTime].Value;

                string iniPath = Path.Combine(e.Language.Name, Strings.IniName.Translation);
                if (!File.Exists(iniPath))
                {
                    File.Create(iniPath).Dispose();
                }

                ini.Sections.Clear();
                ini.Load(iniPath);
                ini.Sections.Add(Strings.IniName.Patcher.Section);
                ini.Sections[Strings.IniName.Patcher.Section].Keys.Add(Strings.IniName.Pack.KeyDate);
                ini.Sections[Strings.IniName.Patcher.Section].Keys[Strings.IniName.Pack.KeyDate].Value = Methods.DateToString(e.Language.LastUpdate);
                ini.Sections[Strings.IniName.Patcher.Section].Keys.Add(Strings.IniName.Ver.KeyMVer);
                ini.Sections[Strings.IniName.Patcher.Section].Keys[Strings.IniName.Ver.KeyMVer].Value = clientMVer;
                ini.Sections[Strings.IniName.Patcher.Section].Keys.Add(Strings.IniName.Ver.KeyTime);
                ini.Sections[Strings.IniName.Patcher.Section].Keys[Strings.IniName.Ver.KeyTime].Value = clientTime;
                ini.Save(iniPath);

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

        private void GameStarter_GameStarterProgressChanged(object sender, GameStarterProgressChangedEventArgs e)
        {
            this.CurrentState = e.State;
        }

        private void GameStarter_GameStarterCompleted(object sender, RunWorkerCompletedEventArgs e)
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
                ForceStripMenuItem_Click(sender, e);

                return;
            }
            else
            {
                Logger.Debug($"{sender.ToString()} successfuly completed");
                this.RestoreFromTray();
            }

            try
            {
                RestoreBackup(this.ComboBoxLanguages.SelectedItem as Language);
            }
            finally
            {
                this.CurrentState = State.Idle;
            }
        }
    }
}

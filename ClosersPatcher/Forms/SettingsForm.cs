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
using System.Windows.Forms;

namespace ClosersPatcher.Forms
{
    internal partial class SettingsForm : Form
    {
        private bool PendingRestart;
        //private string GameClientDirectory;
        private string PatcherWorkingDirectory;
        private string UILanguage;

        internal SettingsForm()
        {
            InitializeComponent();
            InitializeTextComponent();
        }

        private void InitializeTextComponent()
        {
            this.Text = StringLoader.GetText("form_settings");
            this.ButtonOk.Text = StringLoader.GetText("button_ok");
            this.ButtonCancel.Text = StringLoader.GetText("button_cancel");
            this.ButtonApply.Text = StringLoader.GetText("button_apply");
            /*this.TabPageGame.Text = StringLoader.GetText("tab_game");
            this.GroupBoxGameDirectory.Text = StringLoader.GetText("box_game_dir");
            this.ButtonGameChangeDirectory.Text = StringLoader.GetText("button_change");*/
            this.ButtonPatcherChangeDirectory.Text = StringLoader.GetText("button_change");
            this.TabPagePatcher.Text = StringLoader.GetText("tab_patcher");
            this.GroupBoxPatcherDirectory.Text = StringLoader.GetText("box_patcher_dir");
            this.GroupBoxUILanguagePicker.Text = StringLoader.GetText("box_language");
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            this.PendingRestart = false;
            //this.TextBoxGameDirectory.Text = this.GameClientDirectory = UserSettings.GamePath;
            this.TextBoxPatcherDirectory.Text = this.PatcherWorkingDirectory = UserSettings.PatcherPath;

            var def = new ResxLanguage(StringLoader.GetText("match_windows"), "default");
            var en = new ResxLanguage("English", "en");
            this.ComboBoxUILanguage.DataSource = new ResxLanguage[] { def, en };
            string savedCode = this.UILanguage = UserSettings.UILanguageCode;
            if (en.Code == savedCode)
            {
                this.ComboBoxUILanguage.SelectedItem = en;
            }
            else
            {
                this.ComboBoxUILanguage.SelectedItem = def;
            }

            if ((this.Owner as MainForm).CurrentState.In(MainForm.State.Idle, MainForm.State.RegionNotInstalled))
            {
                //this.TextBoxGameDirectory.TextChanged += this.EnableApplyButton;
                this.TextBoxPatcherDirectory.TextChanged += this.EnableApplyButton;
                this.ComboBoxUILanguage.SelectedIndexChanged += this.EnableApplyButton;
            }
            else
            {
                //this.ButtonGameChangeDirectory.Enabled = false;
                this.ButtonPatcherChangeDirectory.Enabled = false;
                this.ComboBoxUILanguage.Enabled = false;
            }
        }

        private void EnableApplyButton(object sender, EventArgs e)
        {
            this.ButtonApply.Enabled = true;
        }

        /*private void ButtonChangeDirectory_Click(object sender, EventArgs e)
        {
            using (var folderDialog = new FolderBrowserDialog
            {
                ShowNewFolderButton = false,
                Description = StringLoader.GetText("dialog_folder_change_game_dir")
            })
            {
                DialogResult result = folderDialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    if (Methods.IsClosersPath(folderDialog.SelectedPath))
                    {
                        this.TextBoxGameDirectory.Text = this.GameClientDirectory = folderDialog.SelectedPath;
                    }
                    else
                    {
                        MsgBox.Error(StringLoader.GetText("exception_folder_not_game_folder"));
                    }
                }
            }
        }*/

        private void ButtonPatcherChangeDirectory_Click(object sender, EventArgs e)
        {
            using (var folderDialog = new FolderBrowserDialog
            {
                Description = StringLoader.GetText("dialog_folder_change_patcher_dir")
            })
            {
                DialogResult result = folderDialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    this.TextBoxPatcherDirectory.Text = this.PatcherWorkingDirectory = folderDialog.SelectedPath;
                }
            }
        }

        private void ComboBoxUILanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.UILanguage = (this.ComboBoxUILanguage.SelectedItem as ResxLanguage).Code;
        }

        private void ButtonOk_Click(object sender, EventArgs e)
        {
            if (this.ButtonApply.Enabled)
            {
                this.ApplyChanges();
            }

            this.DialogResult = DialogResult.OK;
        }

        private void ButtonApply_Click(object sender, EventArgs e)
        {
            this.ApplyChanges();
        }

        private void ApplyChanges()
        {
            /*if (UserSettings.GamePath != this.GameClientDirectory)
                UserSettings.GamePath = this.GameClientDirectory;*/

            if (UserSettings.PatcherPath != this.PatcherWorkingDirectory)
            {
                try
                {
                    MoveOldPatcherFolder(UserSettings.PatcherPath, this.PatcherWorkingDirectory, (this.Owner as MainForm).GetTranslationFolders());
                }
                catch (IOException ex)
                {
                    Logger.Error(ex);
                    MsgBox.Error(Logger.ExeptionParser(ex));
                }

                UserSettings.PatcherPath = this.PatcherWorkingDirectory;
            }

            if (UserSettings.UILanguageCode != this.UILanguage)
            {
                UserSettings.UILanguageCode = this.UILanguage;
                this.PendingRestart = true;
            }

            this.ButtonApply.Enabled = false;

            if (this.PendingRestart)
                MsgBox.Notice(StringLoader.GetText("notice_pending_restart"));
        }

        private static void MoveOldPatcherFolder(string oldPath, string newPath, IEnumerable<string> translationFolders)
        {
            string[] movingFolders = translationFolders.Where(s => Directory.Exists(s)).ToArray();
            string logFilePath = Path.Combine(oldPath, Strings.FileName.Log);

            foreach (var folder in movingFolders)
            {
                MoveDirectory(Path.Combine(oldPath, folder), newPath);
            }

            MoveFile(logFilePath, newPath, false);
        }

        private static bool MoveDirectory(string directory, string newPath)
        {
            if (Directory.Exists(directory))
            {
                string destination = Path.Combine(newPath, Path.GetFileName(directory));
                Directory.CreateDirectory(destination);

                foreach (var dirPath in Directory.GetDirectories(directory, "*", SearchOption.AllDirectories))
                    Directory.CreateDirectory(dirPath.Replace(directory, destination));

                foreach (var filePath in Directory.GetFiles(directory, "*", SearchOption.AllDirectories))
                    MoveFile(filePath, filePath.Replace(directory, destination), true);

                Directory.Delete(directory, true);
                return true;
            }

            return false;
        }

        private static bool MoveFile(string file, string newPath, bool newPathHasFileName)
        {
            if (File.Exists(file))
            {
                string newFilePath = "";
                if (newPathHasFileName)
                {
                    newFilePath = newPath;
                }
                else
                {
                    newFilePath = Path.Combine(newPath, Path.GetFileName(file));
                }

                File.Delete(newFilePath);
                File.Move(file, newFilePath);

                return true;
            }

            return false;
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

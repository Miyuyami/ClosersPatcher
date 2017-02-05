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

namespace ClosersPatcher.Forms
{
    internal partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.ButtonApply = new System.Windows.Forms.Button();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.ButtonOk = new System.Windows.Forms.Button();
            this.TabControl = new System.Windows.Forms.TabControl();
            this.TabPagePatcher = new System.Windows.Forms.TabPage();
            this.GroupBoxUILanguagePicker = new System.Windows.Forms.GroupBox();
            this.ComboBoxUILanguage = new System.Windows.Forms.ComboBox();
            this.GroupBoxPatcherDirectory = new System.Windows.Forms.GroupBox();
            this.ButtonPatcherChangeDirectory = new System.Windows.Forms.Button();
            this.TextBoxPatcherDirectory = new System.Windows.Forms.TextBox();
            this.TableLayoutPanel.SuspendLayout();
            this.TabControl.SuspendLayout();
            this.TabPagePatcher.SuspendLayout();
            this.GroupBoxUILanguagePicker.SuspendLayout();
            this.GroupBoxPatcherDirectory.SuspendLayout();
            this.SuspendLayout();
            // 
            // TableLayoutPanel
            // 
            this.TableLayoutPanel.ColumnCount = 4;
            this.TableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            this.TableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            this.TableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            this.TableLayoutPanel.Controls.Add(this.ButtonApply, 3, 1);
            this.TableLayoutPanel.Controls.Add(this.ButtonCancel, 2, 1);
            this.TableLayoutPanel.Controls.Add(this.ButtonOk, 1, 1);
            this.TableLayoutPanel.Controls.Add(this.TabControl, 0, 0);
            this.TableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.TableLayoutPanel.Name = "TableLayoutPanel";
            this.TableLayoutPanel.RowCount = 2;
            this.TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.TableLayoutPanel.Size = new System.Drawing.Size(320, 252);
            this.TableLayoutPanel.TabIndex = 0;
            // 
            // ButtonApply
            // 
            this.ButtonApply.Enabled = false;
            this.ButtonApply.Location = new System.Drawing.Point(242, 224);
            this.ButtonApply.Name = "ButtonApply";
            this.ButtonApply.Size = new System.Drawing.Size(75, 23);
            this.ButtonApply.TabIndex = 3;
            this.ButtonApply.UseVisualStyleBackColor = true;
            this.ButtonApply.Click += new System.EventHandler(this.ButtonApply_Click);
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.Location = new System.Drawing.Point(161, 224);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(75, 23);
            this.ButtonCancel.TabIndex = 2;
            this.ButtonCancel.UseVisualStyleBackColor = true;
            this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // ButtonOk
            // 
            this.ButtonOk.Location = new System.Drawing.Point(80, 224);
            this.ButtonOk.Name = "ButtonOk";
            this.ButtonOk.Size = new System.Drawing.Size(75, 23);
            this.ButtonOk.TabIndex = 1;
            this.ButtonOk.UseVisualStyleBackColor = true;
            this.ButtonOk.Click += new System.EventHandler(this.ButtonOk_Click);
            // 
            // TabControl
            // 
            this.TableLayoutPanel.SetColumnSpan(this.TabControl, 4);
            this.TabControl.Controls.Add(this.TabPagePatcher);
            this.TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControl.Location = new System.Drawing.Point(3, 3);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(314, 215);
            this.TabControl.TabIndex = 0;
            // 
            // TabPagePatcher
            // 
            this.TabPagePatcher.Controls.Add(this.GroupBoxUILanguagePicker);
            this.TabPagePatcher.Controls.Add(this.GroupBoxPatcherDirectory);
            this.TabPagePatcher.Location = new System.Drawing.Point(4, 22);
            this.TabPagePatcher.Name = "TabPagePatcher";
            this.TabPagePatcher.Padding = new System.Windows.Forms.Padding(3);
            this.TabPagePatcher.Size = new System.Drawing.Size(306, 189);
            this.TabPagePatcher.TabIndex = 2;
            this.TabPagePatcher.UseVisualStyleBackColor = true;
            // 
            // GroupBoxUILanguagePicker
            // 
            this.GroupBoxUILanguagePicker.AutoSize = true;
            this.GroupBoxUILanguagePicker.Controls.Add(this.ComboBoxUILanguage);
            this.GroupBoxUILanguagePicker.Dock = System.Windows.Forms.DockStyle.Top;
            this.GroupBoxUILanguagePicker.Location = new System.Drawing.Point(3, 64);
            this.GroupBoxUILanguagePicker.Name = "GroupBoxUILanguagePicker";
            this.GroupBoxUILanguagePicker.Size = new System.Drawing.Size(300, 59);
            this.GroupBoxUILanguagePicker.TabIndex = 4;
            this.GroupBoxUILanguagePicker.TabStop = false;
            // 
            // ComboBoxUILanguage
            // 
            this.ComboBoxUILanguage.FormattingEnabled = true;
            this.ComboBoxUILanguage.Location = new System.Drawing.Point(6, 19);
            this.ComboBoxUILanguage.Name = "ComboBoxUILanguage";
            this.ComboBoxUILanguage.Size = new System.Drawing.Size(121, 21);
            this.ComboBoxUILanguage.TabIndex = 0;
            this.ComboBoxUILanguage.SelectedIndexChanged += new System.EventHandler(this.ComboBoxUILanguage_SelectedIndexChanged);
            // 
            // GroupBoxPatcherDirectory
            // 
            this.GroupBoxPatcherDirectory.AutoSize = true;
            this.GroupBoxPatcherDirectory.Controls.Add(this.ButtonPatcherChangeDirectory);
            this.GroupBoxPatcherDirectory.Controls.Add(this.TextBoxPatcherDirectory);
            this.GroupBoxPatcherDirectory.Dock = System.Windows.Forms.DockStyle.Top;
            this.GroupBoxPatcherDirectory.Location = new System.Drawing.Point(3, 3);
            this.GroupBoxPatcherDirectory.Name = "GroupBoxPatcherDirectory";
            this.GroupBoxPatcherDirectory.Size = new System.Drawing.Size(300, 61);
            this.GroupBoxPatcherDirectory.TabIndex = 2;
            this.GroupBoxPatcherDirectory.TabStop = false;
            // 
            // ButtonPatcherChangeDirectory
            // 
            this.ButtonPatcherChangeDirectory.Location = new System.Drawing.Point(219, 19);
            this.ButtonPatcherChangeDirectory.Name = "ButtonPatcherChangeDirectory";
            this.ButtonPatcherChangeDirectory.Size = new System.Drawing.Size(75, 23);
            this.ButtonPatcherChangeDirectory.TabIndex = 1;
            this.ButtonPatcherChangeDirectory.UseVisualStyleBackColor = true;
            this.ButtonPatcherChangeDirectory.Click += new System.EventHandler(this.ButtonPatcherChangeDirectory_Click);
            // 
            // TextBoxPatcherDirectory
            // 
            this.TextBoxPatcherDirectory.Location = new System.Drawing.Point(6, 21);
            this.TextBoxPatcherDirectory.Name = "TextBoxPatcherDirectory";
            this.TextBoxPatcherDirectory.ReadOnly = true;
            this.TextBoxPatcherDirectory.Size = new System.Drawing.Size(207, 20);
            this.TextBoxPatcherDirectory.TabIndex = 0;
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.ButtonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 252);
            this.Controls.Add(this.TableLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.TableLayoutPanel.ResumeLayout(false);
            this.TabControl.ResumeLayout(false);
            this.TabPagePatcher.ResumeLayout(false);
            this.TabPagePatcher.PerformLayout();
            this.GroupBoxUILanguagePicker.ResumeLayout(false);
            this.GroupBoxPatcherDirectory.ResumeLayout(false);
            this.GroupBoxPatcherDirectory.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel TableLayoutPanel;
        private System.Windows.Forms.Button ButtonApply;
        private System.Windows.Forms.Button ButtonCancel;
        private System.Windows.Forms.Button ButtonOk;
        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage TabPagePatcher;
        private System.Windows.Forms.GroupBox GroupBoxUILanguagePicker;
        private System.Windows.Forms.ComboBox ComboBoxUILanguage;
        private System.Windows.Forms.GroupBox GroupBoxPatcherDirectory;
        private System.Windows.Forms.Button ButtonPatcherChangeDirectory;
        private System.Windows.Forms.TextBox TextBoxPatcherDirectory;
    }
}
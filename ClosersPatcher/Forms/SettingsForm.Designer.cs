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
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.buttonApply = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.tabPagePatcher = new System.Windows.Forms.TabPage();
            this.groupBoxPatcherDirectory = new System.Windows.Forms.GroupBox();
            this.textBoxPatcherDirectory = new System.Windows.Forms.TextBox();
            this.buttonPatcherChangeDirectory = new System.Windows.Forms.Button();
            this.groupBoxUILanguagePicker = new System.Windows.Forms.GroupBox();
            this.comboBoxUILanguage = new System.Windows.Forms.ComboBox();
            this.tabPageGame = new System.Windows.Forms.TabPage();
            this.groupBoxGameDirectory = new System.Windows.Forms.GroupBox();
            this.textBoxGameDirectory = new System.Windows.Forms.TextBox();
            this.buttonGameChangeDirectory = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tableLayoutPanel.SuspendLayout();
            this.tabPagePatcher.SuspendLayout();
            this.groupBoxPatcherDirectory.SuspendLayout();
            this.groupBoxUILanguagePicker.SuspendLayout();
            this.tabPageGame.SuspendLayout();
            this.groupBoxGameDirectory.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 4;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            this.tableLayoutPanel.Controls.Add(this.buttonApply, 3, 1);
            this.tableLayoutPanel.Controls.Add(this.buttonCancel, 2, 1);
            this.tableLayoutPanel.Controls.Add(this.buttonOk, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.tabControl, 0, 0);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 2;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(320, 252);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // buttonApply
            // 
            this.buttonApply.Enabled = false;
            this.buttonApply.Location = new System.Drawing.Point(242, 224);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(75, 23);
            this.buttonApply.TabIndex = 3;
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.ButtonApply_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(161, 224);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(80, 224);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 1;
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.ButtonOk_Click);
            // 
            // tabPagePatcher
            // 
            this.tabPagePatcher.Controls.Add(this.groupBoxUILanguagePicker);
            this.tabPagePatcher.Controls.Add(this.groupBoxPatcherDirectory);
            this.tabPagePatcher.Location = new System.Drawing.Point(4, 22);
            this.tabPagePatcher.Name = "tabPagePatcher";
            this.tabPagePatcher.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePatcher.Size = new System.Drawing.Size(306, 189);
            this.tabPagePatcher.TabIndex = 2;
            this.tabPagePatcher.UseVisualStyleBackColor = true;
            // 
            // groupBoxPatcherDirectory
            // 
            this.groupBoxPatcherDirectory.AutoSize = true;
            this.groupBoxPatcherDirectory.Controls.Add(this.buttonPatcherChangeDirectory);
            this.groupBoxPatcherDirectory.Controls.Add(this.textBoxPatcherDirectory);
            this.groupBoxPatcherDirectory.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxPatcherDirectory.Location = new System.Drawing.Point(3, 3);
            this.groupBoxPatcherDirectory.Name = "groupBoxPatcherDirectory";
            this.groupBoxPatcherDirectory.Size = new System.Drawing.Size(300, 61);
            this.groupBoxPatcherDirectory.TabIndex = 2;
            this.groupBoxPatcherDirectory.TabStop = false;
            // 
            // textBoxPatcherDirectory
            // 
            this.textBoxPatcherDirectory.Location = new System.Drawing.Point(6, 21);
            this.textBoxPatcherDirectory.Name = "textBoxPatcherDirectory";
            this.textBoxPatcherDirectory.ReadOnly = true;
            this.textBoxPatcherDirectory.Size = new System.Drawing.Size(207, 20);
            this.textBoxPatcherDirectory.TabIndex = 0;
            // 
            // buttonPatcherChangeDirectory
            // 
            this.buttonPatcherChangeDirectory.Location = new System.Drawing.Point(219, 19);
            this.buttonPatcherChangeDirectory.Name = "buttonPatcherChangeDirectory";
            this.buttonPatcherChangeDirectory.Size = new System.Drawing.Size(75, 23);
            this.buttonPatcherChangeDirectory.TabIndex = 1;
            this.buttonPatcherChangeDirectory.UseVisualStyleBackColor = true;
            this.buttonPatcherChangeDirectory.Click += new System.EventHandler(this.ButtonPatcherChangeDirectory_Click);
            // 
            // groupBoxUILanguagePicker
            // 
            this.groupBoxUILanguagePicker.AutoSize = true;
            this.groupBoxUILanguagePicker.Controls.Add(this.comboBoxUILanguage);
            this.groupBoxUILanguagePicker.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxUILanguagePicker.Location = new System.Drawing.Point(3, 64);
            this.groupBoxUILanguagePicker.Name = "groupBoxUILanguagePicker";
            this.groupBoxUILanguagePicker.Size = new System.Drawing.Size(300, 59);
            this.groupBoxUILanguagePicker.TabIndex = 4;
            this.groupBoxUILanguagePicker.TabStop = false;
            // 
            // comboBoxUILanguage
            // 
            this.comboBoxUILanguage.FormattingEnabled = true;
            this.comboBoxUILanguage.Location = new System.Drawing.Point(6, 19);
            this.comboBoxUILanguage.Name = "comboBoxUILanguage";
            this.comboBoxUILanguage.Size = new System.Drawing.Size(121, 21);
            this.comboBoxUILanguage.TabIndex = 0;
            this.comboBoxUILanguage.SelectedIndexChanged += new System.EventHandler(this.ComboBoxUILanguage_SelectedIndexChanged);
            // 
            // tabPageGame
            // 
            this.tabPageGame.Controls.Add(this.groupBoxGameDirectory);
            this.tabPageGame.Location = new System.Drawing.Point(4, 22);
            this.tabPageGame.Name = "tabPageGame";
            this.tabPageGame.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGame.Size = new System.Drawing.Size(306, 189);
            this.tabPageGame.TabIndex = 0;
            this.tabPageGame.UseVisualStyleBackColor = true;
            // 
            // groupBoxGameDirectory
            // 
            this.groupBoxGameDirectory.AutoSize = true;
            this.groupBoxGameDirectory.Controls.Add(this.buttonGameChangeDirectory);
            this.groupBoxGameDirectory.Controls.Add(this.textBoxGameDirectory);
            this.groupBoxGameDirectory.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxGameDirectory.Location = new System.Drawing.Point(3, 3);
            this.groupBoxGameDirectory.Name = "groupBoxGameDirectory";
            this.groupBoxGameDirectory.Size = new System.Drawing.Size(300, 61);
            this.groupBoxGameDirectory.TabIndex = 0;
            this.groupBoxGameDirectory.TabStop = false;
            // 
            // textBoxGameDirectory
            // 
            this.textBoxGameDirectory.Location = new System.Drawing.Point(6, 21);
            this.textBoxGameDirectory.Name = "textBoxGameDirectory";
            this.textBoxGameDirectory.ReadOnly = true;
            this.textBoxGameDirectory.Size = new System.Drawing.Size(207, 20);
            this.textBoxGameDirectory.TabIndex = 0;
            // 
            // buttonGameChangeDirectory
            // 
            this.buttonGameChangeDirectory.Location = new System.Drawing.Point(219, 19);
            this.buttonGameChangeDirectory.Name = "buttonGameChangeDirectory";
            this.buttonGameChangeDirectory.Size = new System.Drawing.Size(75, 23);
            this.buttonGameChangeDirectory.TabIndex = 1;
            this.buttonGameChangeDirectory.UseVisualStyleBackColor = true;
            this.buttonGameChangeDirectory.Click += new System.EventHandler(this.ButtonChangeDirectory_Click);
            // 
            // tabControl
            // 
            this.tableLayoutPanel.SetColumnSpan(this.tabControl, 4);
            this.tabControl.Controls.Add(this.tabPageGame);
            this.tabControl.Controls.Add(this.tabPagePatcher);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(3, 3);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(314, 215);
            this.tabControl.TabIndex = 0;
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 252);
            this.Controls.Add(this.tableLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tabPagePatcher.ResumeLayout(false);
            this.tabPagePatcher.PerformLayout();
            this.groupBoxPatcherDirectory.ResumeLayout(false);
            this.groupBoxPatcherDirectory.PerformLayout();
            this.groupBoxUILanguagePicker.ResumeLayout(false);
            this.tabPageGame.ResumeLayout(false);
            this.tabPageGame.PerformLayout();
            this.groupBoxGameDirectory.ResumeLayout(false);
            this.groupBoxGameDirectory.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageGame;
        private System.Windows.Forms.GroupBox groupBoxGameDirectory;
        private System.Windows.Forms.Button buttonGameChangeDirectory;
        private System.Windows.Forms.TextBox textBoxGameDirectory;
        private System.Windows.Forms.TabPage tabPagePatcher;
        private System.Windows.Forms.GroupBox groupBoxUILanguagePicker;
        private System.Windows.Forms.ComboBox comboBoxUILanguage;
        private System.Windows.Forms.GroupBox groupBoxPatcherDirectory;
        private System.Windows.Forms.Button buttonPatcherChangeDirectory;
        private System.Windows.Forms.TextBox textBoxPatcherDirectory;
    }
}
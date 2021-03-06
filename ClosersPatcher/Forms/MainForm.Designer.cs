﻿namespace ClosersPatcher.Forms
{
    internal partial class MainForm
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
            if (disposing)
            {
                if (this.components != null)
                {
                    this.components.Dispose();
                }
            }
            
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.NotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.StatusStrip = new System.Windows.Forms.StatusStrip();
            this.ToolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.ToolStripSeparator = new System.Windows.Forms.ToolStripStatusLabel();
            this.ToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.ButtonApplyPatch = new System.Windows.Forms.Button();
            this.ButtonRemovePatch = new System.Windows.Forms.Button();
            this.TableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.ButtonDownload = new System.Windows.Forms.Button();
            this.PictureBox = new System.Windows.Forms.PictureBox();
            this.ButtonExit = new System.Windows.Forms.Button();
            this.LabelNewTranslations = new System.Windows.Forms.Label();
            this.ComboBoxLanguages = new System.Windows.Forms.ComboBox();
            this.LabelLanguagePick = new System.Windows.Forms.Label();
            this.LabelRegionPick = new System.Windows.Forms.Label();
            this.ComboBoxRegions = new System.Windows.Forms.ComboBox();
            this.LabelNotifier = new System.Windows.Forms.Label();
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.MenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OriginalFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenClosersWebpageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UploadLogToPastebinToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RefreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SpeakerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StatusStrip.SuspendLayout();
            this.TableLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).BeginInit();
            this.MenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // NotifyIcon
            // 
            this.NotifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.NotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("NotifyIcon.Icon")));
            this.NotifyIcon.DoubleClick += new System.EventHandler(this.NotifyIcon_DoubleClick);
            // 
            // StatusStrip
            // 
            this.StatusStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.StatusStrip.GripMargin = new System.Windows.Forms.Padding(0);
            this.StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripProgressBar,
            this.ToolStripSeparator,
            this.ToolStripStatusLabel});
            this.StatusStrip.Location = new System.Drawing.Point(0, 324);
            this.StatusStrip.Name = "StatusStrip";
            this.StatusStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.StatusStrip.Size = new System.Drawing.Size(364, 22);
            this.StatusStrip.SizingGrip = false;
            this.StatusStrip.TabIndex = 2;
            // 
            // ToolStripProgressBar
            // 
            this.ToolStripProgressBar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.ToolStripProgressBar.MarqueeAnimationSpeed = 40;
            this.ToolStripProgressBar.Maximum = 2147483647;
            this.ToolStripProgressBar.Name = "ToolStripProgressBar";
            this.ToolStripProgressBar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ToolStripProgressBar.Size = new System.Drawing.Size(160, 16);
            // 
            // ToolStripSeparator
            // 
            this.ToolStripSeparator.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.ToolStripSeparator.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.ToolStripSeparator.Margin = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.ToolStripSeparator.Name = "ToolStripSeparator";
            this.ToolStripSeparator.Size = new System.Drawing.Size(4, 22);
            // 
            // ToolStripStatusLabel
            // 
            this.ToolStripStatusLabel.Name = "ToolStripStatusLabel";
            this.ToolStripStatusLabel.Overflow = System.Windows.Forms.ToolStripItemOverflow.Always;
            this.ToolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // ButtonApplyPatch
            // 
            this.ButtonApplyPatch.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.TableLayout.SetColumnSpan(this.ButtonApplyPatch, 2);
            this.ButtonApplyPatch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonApplyPatch.Location = new System.Drawing.Point(58, 205);
            this.ButtonApplyPatch.Name = "ButtonApplyPatch";
            this.ButtonApplyPatch.Size = new System.Drawing.Size(120, 26);
            this.ButtonApplyPatch.TabIndex = 6;
            this.ButtonApplyPatch.UseVisualStyleBackColor = true;
            this.ButtonApplyPatch.Click += new System.EventHandler(this.ButtonApplyPatch_Click);
            // 
            // ButtonRemovePatch
            // 
            this.ButtonRemovePatch.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.TableLayout.SetColumnSpan(this.ButtonRemovePatch, 2);
            this.ButtonRemovePatch.Location = new System.Drawing.Point(184, 205);
            this.ButtonRemovePatch.Name = "ButtonRemovePatch";
            this.ButtonRemovePatch.Size = new System.Drawing.Size(120, 26);
            this.ButtonRemovePatch.TabIndex = 7;
            this.ButtonRemovePatch.UseVisualStyleBackColor = true;
            this.ButtonRemovePatch.Click += new System.EventHandler(this.ButtonRemovePatch_Click);
            // 
            // TableLayout
            // 
            this.TableLayout.BackgroundImage = global::ClosersPatcher.Properties.Resources.fadegray;
            this.TableLayout.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.TableLayout.ColumnCount = 4;
            this.TableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.TableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32F));
            this.TableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.TableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32F));
            this.TableLayout.Controls.Add(this.ButtonApplyPatch, 0, 4);
            this.TableLayout.Controls.Add(this.ButtonRemovePatch, 2, 4);
            this.TableLayout.Controls.Add(this.ButtonDownload, 0, 3);
            this.TableLayout.Controls.Add(this.PictureBox, 0, 0);
            this.TableLayout.Controls.Add(this.ButtonExit, 0, 6);
            this.TableLayout.Controls.Add(this.LabelNewTranslations, 0, 2);
            this.TableLayout.Controls.Add(this.ComboBoxLanguages, 3, 1);
            this.TableLayout.Controls.Add(this.LabelLanguagePick, 2, 1);
            this.TableLayout.Controls.Add(this.LabelRegionPick, 0, 1);
            this.TableLayout.Controls.Add(this.ComboBoxRegions, 1, 1);
            this.TableLayout.Controls.Add(this.LabelNotifier, 0, 5);
            this.TableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TableLayout.Location = new System.Drawing.Point(0, 24);
            this.TableLayout.Name = "TableLayout";
            this.TableLayout.Padding = new System.Windows.Forms.Padding(4);
            this.TableLayout.RowCount = 7;
            this.TableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 122F));
            this.TableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.TableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.TableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.TableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 22F));
            this.TableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.TableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 24F));
            this.TableLayout.Size = new System.Drawing.Size(364, 300);
            this.TableLayout.TabIndex = 1;
            // 
            // ButtonDownload
            // 
            this.ButtonDownload.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.TableLayout.SetColumnSpan(this.ButtonDownload, 4);
            this.ButtonDownload.Location = new System.Drawing.Point(112, 171);
            this.ButtonDownload.Name = "ButtonDownload";
            this.ButtonDownload.Size = new System.Drawing.Size(140, 24);
            this.ButtonDownload.TabIndex = 5;
            this.ButtonDownload.UseVisualStyleBackColor = true;
            this.ButtonDownload.Click += new System.EventHandler(this.ButtonDownload_Click);
            // 
            // PictureBox
            // 
            this.PictureBox.BackgroundImage = global::ClosersPatcher.Properties.Resources.fadegray;
            this.TableLayout.SetColumnSpan(this.PictureBox, 4);
            this.PictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PictureBox.Image = global::ClosersPatcher.Properties.Resources.logo;
            this.PictureBox.Location = new System.Drawing.Point(7, 7);
            this.PictureBox.Name = "PictureBox";
            this.PictureBox.Size = new System.Drawing.Size(350, 116);
            this.PictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBox.TabIndex = 7;
            this.PictureBox.TabStop = false;
            // 
            // ButtonExit
            // 
            this.ButtonExit.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.TableLayout.SetColumnSpan(this.ButtonExit, 4);
            this.ButtonExit.Location = new System.Drawing.Point(132, 265);
            this.ButtonExit.Name = "ButtonExit";
            this.ButtonExit.Size = new System.Drawing.Size(100, 28);
            this.ButtonExit.TabIndex = 9;
            this.ButtonExit.UseVisualStyleBackColor = true;
            this.ButtonExit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // LabelNewTranslations
            // 
            this.LabelNewTranslations.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LabelNewTranslations.AutoSize = true;
            this.LabelNewTranslations.BackColor = System.Drawing.Color.Transparent;
            this.TableLayout.SetColumnSpan(this.LabelNewTranslations, 4);
            this.LabelNewTranslations.ForeColor = System.Drawing.Color.Red;
            this.LabelNewTranslations.Location = new System.Drawing.Point(182, 151);
            this.LabelNewTranslations.Name = "LabelNewTranslations";
            this.LabelNewTranslations.Size = new System.Drawing.Size(0, 13);
            this.LabelNewTranslations.TabIndex = 4;
            // 
            // ComboBoxLanguages
            // 
            this.ComboBoxLanguages.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ComboBoxLanguages.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxLanguages.FormattingEnabled = true;
            this.ComboBoxLanguages.Location = new System.Drawing.Point(248, 129);
            this.ComboBoxLanguages.Name = "ComboBoxLanguages";
            this.ComboBoxLanguages.Size = new System.Drawing.Size(80, 21);
            this.ComboBoxLanguages.TabIndex = 3;
            this.ComboBoxLanguages.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxLanguages_SelectionChangeCommitted);
            // 
            // LabelLanguagePick
            // 
            this.LabelLanguagePick.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.LabelLanguagePick.AutoSize = true;
            this.LabelLanguagePick.BackColor = System.Drawing.Color.Transparent;
            this.LabelLanguagePick.Location = new System.Drawing.Point(242, 131);
            this.LabelLanguagePick.Name = "LabelLanguagePick";
            this.LabelLanguagePick.Size = new System.Drawing.Size(0, 13);
            this.LabelLanguagePick.TabIndex = 2;
            this.LabelLanguagePick.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LabelRegionPick
            // 
            this.LabelRegionPick.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.LabelRegionPick.AutoSize = true;
            this.LabelRegionPick.BackColor = System.Drawing.Color.Transparent;
            this.LabelRegionPick.Location = new System.Drawing.Point(65, 131);
            this.LabelRegionPick.Name = "LabelRegionPick";
            this.LabelRegionPick.Size = new System.Drawing.Size(0, 13);
            this.LabelRegionPick.TabIndex = 0;
            this.LabelRegionPick.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ComboBoxRegions
            // 
            this.ComboBoxRegions.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ComboBoxRegions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxRegions.FormattingEnabled = true;
            this.ComboBoxRegions.Location = new System.Drawing.Point(71, 129);
            this.ComboBoxRegions.Name = "ComboBoxRegions";
            this.ComboBoxRegions.Size = new System.Drawing.Size(80, 21);
            this.ComboBoxRegions.TabIndex = 1;
            this.ComboBoxRegions.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxRegions_SelectionChangeCommitted);
            // 
            // LabelNotifier
            // 
            this.LabelNotifier.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LabelNotifier.AutoSize = true;
            this.LabelNotifier.BackColor = System.Drawing.Color.Transparent;
            this.TableLayout.SetColumnSpan(this.LabelNotifier, 4);
            this.LabelNotifier.ForeColor = System.Drawing.Color.Green;
            this.LabelNotifier.Location = new System.Drawing.Point(182, 239);
            this.LabelNotifier.Name = "LabelNotifier";
            this.LabelNotifier.Size = new System.Drawing.Size(0, 13);
            this.LabelNotifier.TabIndex = 8;
            // 
            // MenuStrip
            // 
            this.MenuStrip.BackgroundImage = global::ClosersPatcher.Properties.Resources.fadegray;
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuToolStripMenuItem,
            this.SettingsToolStripMenuItem,
            this.RefreshToolStripMenuItem,
            this.AboutToolStripMenuItem,
            this.SpeakerToolStripMenuItem});
            this.MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Size = new System.Drawing.Size(364, 24);
            this.MenuStrip.TabIndex = 0;
            // 
            // MenuToolStripMenuItem
            // 
            this.MenuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OriginalFilesToolStripMenuItem,
            this.OpenClosersWebpageToolStripMenuItem,
            this.UploadLogToPastebinToolStripMenuItem});
            this.MenuToolStripMenuItem.Name = "MenuToolStripMenuItem";
            this.MenuToolStripMenuItem.Size = new System.Drawing.Size(12, 20);
            // 
            // OriginalFilesToolStripMenuItem
            // 
            this.OriginalFilesToolStripMenuItem.Name = "OriginalFilesToolStripMenuItem";
            this.OriginalFilesToolStripMenuItem.Size = new System.Drawing.Size(67, 22);
            this.OriginalFilesToolStripMenuItem.Click += new System.EventHandler(this.OriginalFilesToolStripMenuItem_Click);
            // 
            // OpenClosersWebpageToolStripMenuItem
            // 
            this.OpenClosersWebpageToolStripMenuItem.Name = "OpenClosersWebpageToolStripMenuItem";
            this.OpenClosersWebpageToolStripMenuItem.Size = new System.Drawing.Size(67, 22);
            this.OpenClosersWebpageToolStripMenuItem.Click += new System.EventHandler(this.OpenClosersWebpageToolStripMenuItem_Click);
            // 
            // UploadLogToPastebinToolStripMenuItem
            // 
            this.UploadLogToPastebinToolStripMenuItem.Name = "UploadLogToPastebinToolStripMenuItem";
            this.UploadLogToPastebinToolStripMenuItem.Size = new System.Drawing.Size(67, 22);
            this.UploadLogToPastebinToolStripMenuItem.Click += new System.EventHandler(this.UploadLogToPastebinToolStripMenuItem_Click);
            // 
            // SettingsToolStripMenuItem
            // 
            this.SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem";
            this.SettingsToolStripMenuItem.Size = new System.Drawing.Size(12, 20);
            this.SettingsToolStripMenuItem.Click += new System.EventHandler(this.SettingsToolStripMenuItem_Click);
            // 
            // RefreshToolStripMenuItem
            // 
            this.RefreshToolStripMenuItem.Name = "RefreshToolStripMenuItem";
            this.RefreshToolStripMenuItem.Size = new System.Drawing.Size(12, 20);
            this.RefreshToolStripMenuItem.Click += new System.EventHandler(this.RefreshToolStripMenuItem_Click);
            // 
            // AboutToolStripMenuItem
            // 
            this.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem";
            this.AboutToolStripMenuItem.Size = new System.Drawing.Size(12, 20);
            this.AboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // SpeakerToolStripMenuItem
            // 
            this.SpeakerToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.SpeakerToolStripMenuItem.Name = "SpeakerToolStripMenuItem";
            this.SpeakerToolStripMenuItem.Size = new System.Drawing.Size(12, 20);
            this.SpeakerToolStripMenuItem.Click += new System.EventHandler(this.SpeakerToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AcceptButton = this.ButtonApplyPatch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(364, 346);
            this.Controls.Add(this.TableLayout);
            this.Controls.Add(this.StatusStrip);
            this.Controls.Add(this.MenuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MenuStrip;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.StatusStrip.ResumeLayout(false);
            this.StatusStrip.PerformLayout();
            this.TableLayout.ResumeLayout(false);
            this.TableLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).EndInit();
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.TableLayoutPanel TableLayout;
        private System.Windows.Forms.Button ButtonApplyPatch;
        private System.Windows.Forms.Button ButtonRemovePatch;
        private System.Windows.Forms.Button ButtonDownload;
        private System.Windows.Forms.ComboBox ComboBoxLanguages;
        private System.Windows.Forms.NotifyIcon NotifyIcon;
        private System.Windows.Forms.MenuStrip MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem AboutToolStripMenuItem;
        private System.Windows.Forms.StatusStrip StatusStrip;
        private System.Windows.Forms.Button ButtonExit;
        private System.Windows.Forms.PictureBox PictureBox;
        private System.Windows.Forms.ToolStripProgressBar ToolStripProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel ToolStripSeparator;
        private System.Windows.Forms.ToolStripStatusLabel ToolStripStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem MenuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OriginalFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenClosersWebpageToolStripMenuItem;
        private System.Windows.Forms.Label LabelNewTranslations;
        private System.Windows.Forms.ToolStripMenuItem RefreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem UploadLogToPastebinToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SpeakerToolStripMenuItem;
        private System.Windows.Forms.Label LabelLanguagePick;
        private System.Windows.Forms.Label LabelRegionPick;
        private System.Windows.Forms.ComboBox ComboBoxRegions;
        private System.Windows.Forms.Label LabelNotifier;
    }
}


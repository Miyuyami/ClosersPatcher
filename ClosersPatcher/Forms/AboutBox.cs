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

using ClosersPatcher.Helpers;
using ClosersPatcher.Helpers.GlobalVariables;
using System;
using System.Windows.Forms;

namespace ClosersPatcher.Forms
{
    partial class AboutBox : Form
    {
        private int ImagesCount = 31;

        public AboutBox()
        {
            InitializeComponent();
            InitializeTextComponent();
        }

        private void InitializeTextComponent()
        {
            this.buttonOk.Text = StringLoader.GetText("button_ok");
            this.Text = $"About {AssemblyAccessor.Title}";
            this.labelProductName.Text = AssemblyAccessor.Product;
            this.labelVersion.Text = $"Version {AssemblyAccessor.Version}";
            this.textBoxDescription.Text = StringLoader.GetText("patcher_description");
            this.linkLabelWebsite.Links.Add(0, this.linkLabelWebsite.Text.Length, Urls.ClosersWebsite);
            this.logoPictureBox.ImageLocation = $"https://raw.githubusercontent.com/Miyuyami/ClosersPatcher/master/Images/{(new Random()).Next(ImagesCount) + 1}.png";
        }

        private void LinkLabelWebsite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.linkLabelWebsite.LinkVisited = true;
            System.Diagnostics.Process.Start(Urls.ClosersWebsite);
        }
    }
}

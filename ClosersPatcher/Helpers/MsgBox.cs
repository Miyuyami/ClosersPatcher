﻿/*
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

using System.Windows.Forms;

namespace ClosersPatcher.Helpers
{
    internal static class MsgBox
    {
        internal static void Error(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);
        }

        internal static DialogResult ErrorRetry(string message)
        {
            return MessageBox.Show(message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);
        }

        internal static DialogResult Question(string message)
        {
            return MessageBox.Show(message, "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);
        }

        internal static void Success(string message)
        {
            MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);
        }

        internal static void Default(string message, string title, MessageBoxButtons button, MessageBoxIcon icon)
        {
            MessageBox.Show(message, title, button, icon, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);
        }

        internal static void Notice(string message)
        {
            MessageBox.Show(message, "Notice", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);
        }
    }
}

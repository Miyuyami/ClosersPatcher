/*
 * This file is part of Closers Patcher.
 * Copyright (C) 2017 Miyu
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
using System.ComponentModel;
using System.IO;
using System.Threading;

namespace ClosersPatcher.Patching
{
    internal delegate void PatchRemoverCompletedEventHandler(object sender, RunWorkerCompletedEventArgs e);

    internal class PatchRemover
    {
        private readonly BackgroundWorker Worker;
        private Language Language;

        internal PatchRemover()
        {
            this.Worker = new BackgroundWorker
            {
                WorkerSupportsCancellation = true
            };
            this.Worker.DoWork += this.Worker_DoWork;
            this.Worker.RunWorkerCompleted += this.Worker_RunWorkerCompleted;
        }

        internal event PatchRemoverCompletedEventHandler PatchRemoverCompleted;

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Logger.Debug(Methods.MethodFullName("PatchRemover", Thread.CurrentThread.ManagedThreadId, this.Language));

            Methods.CheckRunningPrograms();

            ClosersFileManager.LoadFileConfiguration(this.Language);

            if (Methods.IsTranslationOutdated(this.Language))
            {
                Methods.DeleteBackups(this.Language);
            }

            if (!Methods.BackupExists(this.Language))
            {
                throw new Exception(StringLoader.GetText("exception_no_backup"));
            }

            if (this.Worker.CancellationPending)
            {
                e.Cancel = true;
                return;
            }

            RestoreBackup(this.Language);
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.PatchRemoverCompleted?.Invoke(sender, e);
        }

        private static void RestoreBackup(Language language)
        {
            if (!Directory.Exists(language.BackupPath))
            {
                return;
            }

            string[] filePaths = Directory.GetFiles(language.BackupPath, "*", SearchOption.AllDirectories);

            foreach (string file in filePaths)
            {
                string path = Path.Combine(UserSettings.GamePath, file.Substring(language.BackupPath.Length + 1));
                Logger.Info($"Restoring file original=[{path}] backup=[{file}]");

                try
                {
                    File.Delete(path);
                    File.Move(file, path);
                }
                catch (DirectoryNotFoundException)
                {
                    MsgBox.Error(StringLoader.GetText("exception_cannot_restore_file", Path.GetFullPath(file)));
                    Logger.Error($"Cannot restore file=[{file}]");
                    File.Delete(file);
                }
            }
        }

        internal void Cancel()
        {
            this.Worker.CancelAsync();
        }

        internal void Run(Language language)
        {
            if (this.Worker.IsBusy)
            {
                return;
            }

            this.Language = language;
            this.Worker.RunWorkerAsync();
        }
    }
}

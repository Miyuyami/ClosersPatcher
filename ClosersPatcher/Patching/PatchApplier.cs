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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;

namespace ClosersPatcher.Patching
{
    internal delegate void PatchApplierCompletedEventHandler(object sender, RunWorkerCompletedEventArgs e);

    internal class PatchApplier
    {
        private readonly BackgroundWorker Worker;
        private Language Language;

        internal PatchApplier()
        {
            this.Worker = new BackgroundWorker
            {
                WorkerSupportsCancellation = true
            };
            this.Worker.DoWork += this.Worker_DoWork;
            this.Worker.RunWorkerCompleted += this.Worker_RunWorkerCompleted;
        }

        internal event PatchApplierCompletedEventHandler PatchApplierCompleted;

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Logger.Debug(Methods.MethodFullName("PatchApplier", Thread.CurrentThread.ManagedThreadId.ToString(), this.Language.ToString()));

            Methods.CheckRunningPrograms();

            if (!Methods.IsGameLatestVersion())
            {
                throw new Exception(StringLoader.GetText("exception_not_latest_client"));
            }

            ClosersFileManager.LoadFileConfiguration();

            if (Methods.IsTranslationOutdated(this.Language))
            {
                e.Result = true; // call force patch in completed Event
                return;
            }

            if (Methods.BackupExists())
            {
                throw new Exception(StringLoader.GetText("exception_translation_already_applied"));
            }

            if (IsTranslationMissing(this.Language))
            {
                e.Result = true; // call force patch in completed Event
                return;
            }

            if (this.Worker.CancellationPending)
            {
                e.Cancel = true;
                return;
            }

            BackupAndPlaceFiles(this.Language);
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.PatchApplierCompleted?.Invoke(sender, e);
        }

        private static bool IsTranslationMissing(Language language)
        {
            ReadOnlyCollection<ClosersFile> gameFiles = ClosersFileManager.GetFiles();
            ILookup<Type, ClosersFile> gameFileTypeLookup = gameFiles.ToLookup(f => f.GetType());
            IEnumerable<string> otherGameFiles = gameFileTypeLookup[typeof(ClosersFile)].Select(f => f.Path + Path.GetFileName(f.PathD));
            IEnumerable<string> translationFiles = otherGameFiles.Select(f => Path.Combine(language.Name, f));

            foreach (var path in translationFiles)
            {
                if (!File.Exists(path))
                {
                    return true;
                }
            }

            return false;
        }

        private static void BackupAndPlaceFiles(Language language)
        {
            ReadOnlyCollection<ClosersFile> gameFiles = ClosersFileManager.GetFiles();
            ILookup<Type, ClosersFile> gameFileTypeLookup = gameFiles.ToLookup(f => f.GetType());
            IEnumerable<string> otherGameFiles = gameFileTypeLookup[typeof(ClosersFile)].Select(f => f.Path + Path.GetFileName(f.PathD));
            IEnumerable<string> translationFiles = otherGameFiles;

            foreach (var path in translationFiles)
            {
                string originalFilePath = Path.Combine(UserSettings.GamePath, path);
                string translationFilePath = Path.Combine(language.Name, path);
                string backupFilePath = Path.Combine(Strings.FolderName.Backup, path);

                BackupAndPlaceFile(originalFilePath, translationFilePath, backupFilePath);
            }
        }

        private static void BackupAndPlaceFile(string originalFilePath, string translationFilePath, string backupFilePath)
        {
            string backupFileDirectory = Path.GetDirectoryName(backupFilePath);
            Directory.CreateDirectory(backupFileDirectory);

            Logger.Info($"Swapping file original=[{originalFilePath}] backup=[{backupFilePath}] translation=[{translationFilePath}]");
            File.Move(originalFilePath, backupFilePath);
            File.Move(translationFilePath, originalFilePath);
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

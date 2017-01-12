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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using static ClosersPatcher.Forms.MainForm;

namespace ClosersPatcher.Launching
{
    public delegate void GameStarterProgressChangedEventHandler(object sender, GameStarterProgressChangedEventArgs e);
    public delegate void GameStarterCompletedEventHandler(object sender, RunWorkerCompletedEventArgs e);
    
    class GameStarter
    {
        private readonly BackgroundWorker Worker;
        private Language Language;

        public GameStarter()
        {
            this.Worker = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            this.Worker.DoWork += this.Worker_DoWork;
            this.Worker.ProgressChanged += this.Worker_ProgressChanged;
            this.Worker.RunWorkerCompleted += this.Worker_RunWorkerCompleted;
        }

        public event GameStarterProgressChangedEventHandler GameStarterProgressChanged;
        public event GameStarterCompletedEventHandler GameStarterCompleted;

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            this.Worker.ReportProgress((int)State.Prepare);
            Logger.Debug(Methods.MethodFullName("GameStart", Thread.CurrentThread.ManagedThreadId.ToString(), this.Language.ToString()));

            if (!Methods.IsGameLatestVersion())
            {
                throw new Exception(StringLoader.GetText("exception_not_latest_client"));
            }

            Methods.CheckRunningPrograms();

            ClosersFileManager.LoadFileConfiguration();

            if (IsTranslationOutdatedOrMissing(this.Language))
            {
                e.Result = true; // call force patch in completed event
                return;
            }

            Process clientProcess = null;

            BackupAndPlaceFiles(this.Language);

            this.Worker.ReportProgress((int)State.WaitClient);
            while (true)
            {
                if (this.Worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                if ((clientProcess = GetProcess(Strings.FileName.GameExe)) == null)
                {
                    Thread.Sleep(1000);
                }
                else
                {
                    break;
                }
            }

            this.Worker.ReportProgress((int)State.WaitClose);
            clientProcess.WaitForExit();
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.GameStarterProgressChanged?.Invoke(sender, new GameStarterProgressChangedEventArgs((State)e.ProgressPercentage));
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.GameStarterCompleted?.Invoke(sender, e);
        }

        private static bool IsTranslationOutdatedOrMissing(Language language)
        {
            if (Methods.IsTranslationOutdated(language))
            {
                return true;
            }

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

        private static Process GetProcess(string name)
        {
            Process[] processesByName = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(name));

            if (processesByName.Length > 0)
            {
                return processesByName[0];
            }

            return null;
        }

        public void Cancel()
        {
            this.Worker.CancelAsync();
        }

        public void Run(Language language)
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

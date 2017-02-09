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
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Threading;

namespace ClosersPatcher.Downloading
{
    internal delegate void DownloaderProgressChangedEventHandler(object sender, DownloaderProgressChangedEventArgs e);
    internal delegate void DownloaderCompletedEventHandler(object sender, DownloaderCompletedEventArgs e);

    internal class Downloader
    {
        private BackgroundWorker Worker;
        private WebClient Client;
        private Language Language;
        private bool IsDownloadingInClientFolder = false;

        internal Downloader()
        {
            this.Worker = new BackgroundWorker
            {
                WorkerSupportsCancellation = true
            };
            this.Worker.DoWork += this.Worker_DoWork;
            this.Worker.RunWorkerCompleted += this.Worker_RunWorkerCompleted;
            this.Client = new WebClient();
            this.Client.DownloadProgressChanged += this.Client_DownloadProgressChanged;
            this.Client.DownloadDataCompleted += this.Client_DownloadDataCompleted;
        }

        internal event DownloaderProgressChangedEventHandler DownloaderProgressChanged;
        internal event DownloaderCompletedEventHandler DownloaderCompleted;

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Logger.Debug(Methods.MethodFullName("Downloader", Thread.CurrentThread.ManagedThreadId, this.Language != null ? this.Language.ToString() : "null"));

            if (Methods.IsGameLatestVersion())
            {
                if (this.IsDownloadingInClientFolder)
                {
                    Logger.Info("Downloading backup");

                    if (Methods.IsTranslationSupported(this.Language))
                    {
                        if (this.Worker.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }

                        ClosersFileManager.LoadFileConfiguration(this.Language);
                    }
                    else
                    {
                        throw new Exception(StringLoader.GetText("exception_backup_version_not_supported"));
                    }
                }
                else
                {
                    if (Methods.IsTranslationSupported(this.Language))
                    {
                        bool isTranslationOutdated = Methods.IsTranslationOutdated(this.Language);

                        if (isTranslationOutdated)
                        {
                            Methods.DeleteBackups(this.Language);
                        }

                        if (Methods.HasNewTranslations(this.Language) || isTranslationOutdated)
                        {
                            if (this.Worker.CancellationPending)
                            {
                                e.Cancel = true;
                                return;
                            }

                            ClosersFileManager.LoadFileConfiguration(this.Language);
                        }
                        else
                        {
                            throw new Exception(StringLoader.GetText("exception_already_latest_translation", Methods.DateToString(this.Language.LastUpdate)));
                        }
                    }
                    else
                    {
                        throw new Exception(StringLoader.GetText("exception_version_not_supported"));
                    }
                }
            }
            else
            {
                throw new Exception(StringLoader.GetText("exception_not_latest_client"));
            }
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled || e.Error != null)
            {
                this.DownloaderCompleted?.Invoke(sender, new DownloaderCompletedEventArgs(e.Cancelled, e.Error));
            }
            else
            {
                this.DownloadNext(0);
            }
        }

        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            this.DownloaderProgressChanged?.Invoke(sender, new DownloaderProgressChangedEventArgs((int)e.UserState + 1, ClosersFileManager.Count, Path.GetFileNameWithoutExtension(ClosersFileManager.GetElementAt((int)e.UserState).Name), e));
        }

        private void Client_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            if (e.Cancelled || e.Error != null)
            {
                this.DownloaderCompleted?.Invoke(sender, new DownloaderCompletedEventArgs(e.Cancelled, e.Error));
            }
            else
            {
                var index = (int)e.UserState;
                ClosersFile swFile = ClosersFileManager.GetElementAt(index);

                string swFilePath;

                if (this.IsDownloadingInClientFolder)
                {
                    swFilePath = Path.Combine(UserSettings.GamePath, swFile.Path, Path.GetFileName(swFile.PathD));
                }
                else
                {
                    swFilePath = Path.Combine(this.Language.Path, swFile.Path, Path.GetFileName(swFile.PathD));
                }
                string swFileDirectory = swFileDirectory = Path.GetDirectoryName(swFilePath);

                Directory.CreateDirectory(swFileDirectory);
                File.WriteAllBytes(swFilePath, e.Result);

                if (ClosersFileManager.Count > ++index)
                {
                    this.DownloadNext(index);
                }
                else
                {
                    this.DownloaderCompleted?.Invoke(sender, new DownloaderCompletedEventArgs(this.Language, this.IsDownloadingInClientFolder, e.Cancelled, e.Error));
                }
            }
        }

        private void DownloadNext(int index)
        {
            Uri uri = new Uri(Urls.TranslationHome + this.Language.Path + '/' + ClosersFileManager.GetElementAt(index).PathD);

            this.Client.DownloadDataAsync(uri, index);

            Logger.Debug(Methods.MethodFullName(System.Reflection.MethodBase.GetCurrentMethod(), uri.AbsoluteUri));
        }

        internal void Cancel()
        {
            this.Worker.CancelAsync();
            this.Client.CancelAsync();
        }

        internal void Run(Language language)
        {
            if (this.Worker.IsBusy || this.Client.IsBusy)
            {
                return;
            }

            this.IsDownloadingInClientFolder = false;
            this.Language = language;
            this.Worker.RunWorkerAsync();
        }

        internal void Run(Region region)
        {
            if (this.Worker.IsBusy || this.Client.IsBusy)
            {
                return;
            }

            this.IsDownloadingInClientFolder = true;
            this.Language = Language.BackupLanguage(region.Id);
            this.Worker.RunWorkerAsync();
        }
    }
}

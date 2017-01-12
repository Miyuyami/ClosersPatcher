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
    public delegate void DownloaderProgressChangedEventHandler(object sender, DownloaderProgressChangedEventArgs e);
    public delegate void DownloaderCompletedEventHandler(object sender, DownloaderCompletedEventArgs e);

    public class Downloader
    {
        private BackgroundWorker Worker;
        private WebClient Client;
        private Language Language;

        public Downloader()
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

        public event DownloaderProgressChangedEventHandler DownloaderProgressChanged;
        public event DownloaderCompletedEventHandler DownloaderCompleted;

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Logger.Debug(Methods.MethodFullName("Downloader", Thread.CurrentThread.ManagedThreadId.ToString(), this.Language.ToString()));

            if (Methods.IsGameLatestVersion())
            {
                if (Methods.IsTranslationSupported(this.Language))
                {
                    if (Methods.HasNewTranslations(this.Language) || Methods.IsTranslationOutdated(this.Language))
                    {
                        ClosersFileManager.LoadFileConfiguration();
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

                string swFilePath = Path.Combine(this.Language.Name, swFile.Path, Path.GetFileName(swFile.PathD));
                string swFileDirectory = Path.GetDirectoryName(swFilePath);

                Directory.CreateDirectory(swFileDirectory);
                File.WriteAllBytes(swFilePath, e.Result);

                if (ClosersFileManager.Count > ++index)
                {
                    this.DownloadNext(index);
                }
                else
                {
                    this.DownloaderCompleted?.Invoke(sender, new DownloaderCompletedEventArgs(this.Language, e.Cancelled, e.Error));
                }
            }
        }

        private void DownloadNext(int index)
        {
            Uri uri = new Uri(Urls.TranslationHome + this.Language.Name + '/' + ClosersFileManager.GetElementAt(index).PathD);

            this.Client.DownloadDataAsync(uri, index);

            Logger.Debug(Methods.MethodFullName(System.Reflection.MethodBase.GetCurrentMethod(), uri.AbsoluteUri));
        }

        public void Cancel()
        {
            this.Worker.CancelAsync();
            this.Client.CancelAsync();
        }

        public void Run(Language language)
        {
            if (this.Worker.IsBusy || this.Client.IsBusy)
            {
                return;
            }

            this.Language = language;
            this.Worker.RunWorkerAsync();
        }
    }
}

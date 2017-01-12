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

using ClosersPatcher.General;
using System;

namespace ClosersPatcher.Downloading
{
    public class DownloaderCompletedEventArgs : EventArgs
    {
        public Language Language { get; private set; }
        public bool Cancelled { get; private set; }
        public Exception Error { get; private set; }

        public DownloaderCompletedEventArgs(bool cancelled, Exception error)
        {
            this.Language = null;
            this.Cancelled = cancelled;
            this.Error = error;
        }

        public DownloaderCompletedEventArgs(Language language, bool cancelled, Exception error)
        {
            this.Language = language;
            this.Cancelled = cancelled;
            this.Error = error;
        }
    }
}

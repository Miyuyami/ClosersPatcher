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

using ClosersPatcher.Helpers.GlobalVariables;
using MadMilkman.Ini;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;

namespace ClosersPatcher.General
{
    internal static class ClosersFileManager
    {
        private static List<ClosersFile> ClosersFiles;
        internal static int Count => ClosersFiles.Count;
        internal static ClosersFile GetElementAt(int index) => ClosersFiles[index];
        internal static ReadOnlyCollection<ClosersFile> GetFiles() => ClosersFiles.AsReadOnly();

        internal static void LoadFileConfiguration(Language language)
        {
            if (ClosersFiles == null)
            {
                InternalLoadFileConfiguration(Urls.TranslationHome + language.Path + '/' + Strings.IniName.TranslationPackData);
            }
        }

        private static void InternalLoadFileConfiguration(string url)
        {
            ClosersFiles = new List<ClosersFile>();

            byte[] packData;
            using (var client = new WebClient())
            {
                packData = client.DownloadData(url);
            }

            IniFile ini = new IniFile();
            using (var stream = new MemoryStream(packData))
            {
                ini.Load(stream);
            }

            foreach (IniSection section in ini.Sections)
            {
                string name = section.Name;
                string path = section.Keys[Strings.IniName.Pack.KeyPath].Value;
                string pathD = section.Keys[Strings.IniName.Pack.KeyPathOfDownload].Value;

                ClosersFiles.Add(new ClosersFile(name, path, pathD));
            }
        }
    }
}

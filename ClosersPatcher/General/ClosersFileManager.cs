/*
 * This file is part of Soulworker Patcher.
 * Copyright (C) 2016-2017 Miyu, Dramiel Leayal
 * 
 * Soulworker Patcher is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * Soulworker Patcher is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with Soulworker Patcher. If not, see <http://www.gnu.org/licenses/>.
 */

using ClosersPatcher.Helpers.GlobalVariables;
using MadMilkman.Ini;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;

namespace ClosersPatcher.General
{
    static class ClosersFileManager
    {
        private static List<ClosersFile> ClosersFiles;
        public static int Count => ClosersFiles.Count;
        public static ClosersFile GetElementAt(int index) => ClosersFiles[index];
        public static ReadOnlyCollection<ClosersFile> GetFiles() => ClosersFiles.AsReadOnly();

        internal static void LoadFileConfiguration()
        {
            if (ClosersFiles == null)
            {
                InternalLoadFileConfiguration();
            }
        }

        private static void InternalLoadFileConfiguration()
        {
            ClosersFiles = new List<ClosersFile>();

            byte[] packData;
            using (var client = new WebClient())
            {
                packData = client.DownloadData(Urls.TranslationHome + Strings.IniName.TranslationPackData);
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

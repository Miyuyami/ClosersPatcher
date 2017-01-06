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

namespace ClosersPatcher.Helpers.GlobalVariables
{
    public static class Strings
    {
        public static class PasteBin
        {
            public const string DevKey = "2e5bee04f7455774443dd399934494bd";
            public const string Username = "SWPatcher";
            public const string Password = "pIIrwSL8lNJOjPhW";
        }

        public static class FileName
        {
            public const string CMFScriptPack = "SCRIPT_PACK.CMF";
            public const string GameExe = "CW.EXE";
            public const string ClosersExe = "CLOSERS.EXE";
            public const string LauncherExe = "LAUNCHER.EXE";
            public const string SecurityExe = "DirectoryRights.exe";
            public const string Log = ".log";
        }

        public static class Registry
        {
            public const string Key64 = @"SOFTWARE\WOW6432Node\Nexon\Closers";
            public const string Key32 = @"SOFTWARE\Nexon\Closers";
            public const string Name = "RootPath";
        }

        public static class FolderName
        {
            public const string Data = "DAT";
            public const string Backup = "backup";
        }

        public static class IniName
        {
            public const string ClientVer = "VER.DLL";
            public const string Translation = "Translation.ini";
            public const string LanguagePack = "LanguagePacks.ini";
            public const string TranslationPackData = "TranslationPackData.ini";

            public static class Ver
            {
                public const string Section = "Ver";
                public const string KeyMVer = "MVer";
                public const string KeyTime = "Time";
            }

            public static class Patcher
            {
                public const string Section = "Patcher";
                public const string KeyAddress = "address";
            }

            public static class Pack
            {
                public const string KeyDate = "date";
                public const string KeyPath = "path";
                public const string KeyPathOfDownload = "path_d";
                public const string KeyBaseValue = "__base__";
            }
        }
    }
}

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

namespace ClosersPatcher.Helpers.GlobalVariables
{
    internal static class Strings
    {
        internal static class PasteBin
        {
            internal const string DevKey = "590bc5306a68dcbc1abdc596cf883bcf";
            internal const string Username = "ClosersPatcher";
            internal const string Password = "RiG*$mY3v0kG37&ByI#4UxTLB5!Ppz2K";
        }

        internal static class FileName
        {
            internal const string CMFScriptPack = "SCRIPT_PACK.CMF";
            internal const string GameExe = "CW.EXE";
            internal const string ClosersExe = "CLOSERS.EXE";
            internal const string LauncherExe = "LAUNCHER.EXE";
            internal const string SecurityExe = "DirectoryRights.exe";
            internal const string Log = ".log";
        }

        internal static class Registry
        {
            internal class KR
            {
                internal static Microsoft.Win32.RegistryKey RegistryKey = Microsoft.Win32.Registry.LocalMachine;
                internal const string Key32Path = @"SOFTWARE\Nexon\Closers";
                internal const string Key64Path = @"SOFTWARE\WOW6432Node\Nexon\Closers";
                internal const string FolderPath = "RootPath";
            }
        }

        internal static class FolderName
        {
            internal const string Data = "DAT";
            internal const string Backup = "backup";
        }

        internal static class IniName
        {
            internal const string ClientVer = "VER.DLL";
            internal const string Translation = "Translation.ini";
            internal const string LanguagePack = "LanguagePacks.xml";
            internal const string TranslationPackData = "TranslationPackData.ini";

            internal static class Ver
            {
                internal const string Section = "Ver";
                internal const string KeyMVer = "MVer";
                internal const string KeyTime = "Time";
            }

            internal static class Patcher
            {
                internal const string Section = "Patcher";
                internal const string KeyDate = "date";
            }

            internal static class Pack
            {
                internal const string KeyPath = "path";
                internal const string KeyPathOfDownload = "path_d";
                internal const string KeyParts = "parts";
                internal const string KeyBaseValue = "__base__";
            }
        }

        internal static class Xml
        {
            internal const string Value = "value";
            internal const string Regions = "regions";
            internal const string Languages = "languages";
            internal const string Supports = "supports";
            internal const string Uri = "uri";

            internal static class Attributes
            {
                internal const string Name = "name";
            }
        }
    }
}

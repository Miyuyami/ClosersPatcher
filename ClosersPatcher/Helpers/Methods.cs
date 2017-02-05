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
using ClosersPatcher.Helpers.GlobalVariables;
using MadMilkman.Ini;
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace ClosersPatcher.Helpers
{
    internal static class Methods
    {
        private static string DateFormat = "d/MMM/yyyy h:mm tt";

        internal static DateTime ParseDate(string date)
        {
            return ParseDate(date, DateFormat);
        }

        internal static DateTime ParseDate(string date, string format)
        {
            return DateTime.ParseExact(date, format, CultureInfo.InvariantCulture);
        }

        internal static string DateToString(DateTime date)
        {
            return DateToString(date, DateFormat);
        }

        internal static string DateToString(DateTime date, string format)
        {
            return date.ToString(format, CultureInfo.InvariantCulture);
        }

        internal static bool LoadIni(out IniFile iniFile, string iniPath)
        {
            return LoadIni(out iniFile, new IniOptions(), iniPath);
        }

        internal static bool LoadIni(out IniFile iniFile, IniOptions iniOptions, string iniPath)
        {
            if (!File.Exists(iniPath))
            {
                iniFile = null;
                return false;
            }

            iniFile = new IniFile(iniOptions);
            iniFile.Load(iniPath);

            return true;
        }

        internal static bool LoadVerIni(out IniFile verIni, string verIniPath)
        {
            return LoadVerIni(out verIni, new IniOptions(), verIniPath);
        }

        internal static bool LoadVerIni(out IniFile verIni, IniOptions verIniOptions, string verIniPath)
        {
            if (!LoadIni(out verIni, verIniOptions, verIniPath))
            {
                return false;
            }

            if (!verIni.Sections.Contains(Strings.IniName.Ver.Section))
            {
                return false;
            }

            IniSection clientVerSection = verIni.Sections[Strings.IniName.Ver.Section];
            if (!clientVerSection.Keys.Contains(Strings.IniName.Ver.KeyMVer) ||
                !clientVerSection.Keys.Contains(Strings.IniName.Ver.KeyTime))
            {
                return false;
            }

            return true;
        }

        internal static bool LoadPatcherIni(out IniFile patcherIni, string patcherIniPath)
        {
            return LoadPatcherIni(out patcherIni, new IniOptions(), patcherIniPath);
        }

        internal static bool LoadPatcherIni(out IniFile patcherIni, IniOptions patcherIniOptions, string patcherIniPath)
        {
            if (!LoadVerIni(out patcherIni, patcherIniOptions, patcherIniPath))
            {
                return false;
            }

            if (!patcherIni.Sections.Contains(Strings.IniName.Patcher.Section))
            {
                return false;
            }

            IniSection clientVerSection = patcherIni.Sections[Strings.IniName.Patcher.Section];
            if (!clientVerSection.Keys.Contains(Strings.IniName.Patcher.KeyDate) ||
                !clientVerSection.Keys.Contains(Strings.IniName.Patcher.KeyRegion))
            {
                return false;
            }

            return true;
        }

        internal static bool HasNewTranslations(Language language)
        {
            string translationFolder = language.Path;

            if (!Directory.Exists(translationFolder))
            {
                return true;
            }

            string selectedTranslationIniPath = Path.Combine(translationFolder, Strings.IniName.Translation);
            if (!LoadPatcherIni(out IniFile translationIni, selectedTranslationIniPath))
            {
                return false;
            }
            IniSection translationPatcherSection = translationIni.Sections[Strings.IniName.Patcher.Section];
            string date = translationPatcherSection.Keys[Strings.IniName.Patcher.KeyDate].Value;

            return language.LastUpdate > ParseDate(date);
        }

        internal static bool IsGameLatestVersion()
        {
#if DEBUG
            return true;
#endif
            IniFile regionIni = GetIniFromUrl(Urls.ClosersSettingsHome + Strings.IniName.ClientVer);
            IniSection regionVerSection = regionIni.Sections[Strings.IniName.Ver.Section];

            string clientIniPath = Path.Combine(UserSettings.GamePath, Strings.IniName.ClientVer);
            if (!LoadVerIni(out IniFile clientIni, clientIniPath))
            {
                return false;
            }
            IniSection clientVerSection = clientIni.Sections[Strings.IniName.Ver.Section];

            string regionMVer = regionVerSection.Keys[Strings.IniName.Ver.KeyMVer].Value;
            string clientMVer = clientVerSection.Keys[Strings.IniName.Ver.KeyMVer].Value;
            if (clientMVer != regionMVer)
            {
                return false;
            }

            string regionTime = regionVerSection.Keys[Strings.IniName.Ver.KeyTime].Value;
            string clientTime = clientVerSection.Keys[Strings.IniName.Ver.KeyTime].Value;
            if (clientTime != regionTime)
            {
                return false;
            }

            return true;
        }

        internal static bool IsTranslationOutdated(Language language)
        {
            string selectedTranslationIniPath = Path.Combine(language.Path, Strings.IniName.Translation);
            if (!LoadPatcherIni(out IniFile translationIni, selectedTranslationIniPath))
            {
                return true;
            }
            IniSection translationPatcherSection = translationIni.Sections[Strings.IniName.Patcher.Section];
            IniSection translationVerSection = translationIni.Sections[Strings.IniName.Ver.Section];

            string clientIniPath = Path.Combine(UserSettings.GamePath, Strings.IniName.ClientVer);
            if (!LoadVerIni(out IniFile clientIni, clientIniPath))
            {
                throw new Exception(StringLoader.GetText("exception_generic_read_error", clientIniPath));
            }
            IniSection clientVerSection = clientIni.Sections[Strings.IniName.Ver.Section];

            string translationMVer = translationVerSection.Keys[Strings.IniName.Ver.KeyMVer].Value;
            string clientMVer = clientVerSection.Keys[Strings.IniName.Ver.KeyMVer].Value;
            if (clientMVer != translationMVer)
            {
                return true;
            }

            string translationTime = translationVerSection.Keys[Strings.IniName.Ver.KeyTime].Value;
            string clientTime = clientVerSection.Keys[Strings.IniName.Ver.KeyTime].Value;
            if (clientTime != translationTime)
            {
                return true;
            }

            return false;
        }

        internal static bool IsTranslationSupported(Language language)
        {
#if DEBUG
            return true;
#endif
            IniFile supportedTranslationIni = GetIniFromUrl(Urls.TranslationHome + language.Path + '/' + Strings.IniName.ClientVer);
            IniSection supportedTranslationVerSection = supportedTranslationIni.Sections[Strings.IniName.Ver.Section];

            string clientIniPath = Path.Combine(UserSettings.GamePath, Strings.IniName.ClientVer);
            if (!LoadVerIni(out IniFile clientIni, clientIniPath))
            {
                throw new Exception(StringLoader.GetText("exception_generic_read_error", clientIniPath));
            }
            IniSection clientVerSection = clientIni.Sections[Strings.IniName.Ver.Section];

            string supportedTranslationMVer = supportedTranslationVerSection.Keys[Strings.IniName.Ver.KeyMVer].Value;
            string clientMVer = clientVerSection.Keys[Strings.IniName.Ver.KeyMVer].Value;
            if (clientMVer != supportedTranslationMVer)
            {
                return false;
            }

            string supportedTranslationTime = supportedTranslationVerSection.Keys[Strings.IniName.Ver.KeyTime].Value;
            string clientTime = clientVerSection.Keys[Strings.IniName.Ver.KeyTime].Value;
            if (clientTime != supportedTranslationTime)
            {
                return false;
            }

            return true;
        }

        internal static void DeleteBackups(Language language)
        {
            try
            {
                string[] filePaths = Directory.GetFiles(language.BackupPath, "*", SearchOption.AllDirectories);

                foreach (var file in filePaths)
                {
                    File.Delete(file);
                }
            }
            catch (DirectoryNotFoundException)
            {
                Directory.CreateDirectory(language.BackupPath);
            }
        }

        internal static bool BackupExists(Language language)
        {
            try
            {
                string[] filePaths = Directory.GetFiles(language.BackupPath, "*", SearchOption.AllDirectories);

                return filePaths.Length > 0;
            }
            catch (DirectoryNotFoundException)
            {
                Directory.CreateDirectory(language.BackupPath);
            }

            return false;
        }

        internal static IniFile GetIniFromUrl(string url)
        {
            using (var client = new WebClient())
            {
                try
                {
                    byte[] iniData = client.DownloadData(url);

                    IniFile ini = new IniFile();
                    using (MemoryStream ms = new MemoryStream(iniData))
                    {
                        ini.Load(ms);
                    }

                    return ini;
                }
                catch (WebException e)
                {
                    if (e.InnerException is SocketException innerException)
                    {
                        if (innerException.SocketErrorCode == SocketError.ConnectionRefused)
                        {
                            throw new Exception(StringLoader.GetText("exception_region_refused_connection"));
                        }
                        else
                        {
                            throw;
                        }
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }

        internal static string MethodName(MethodBase method)
        {
            return $"{method.ReflectedType.FullName}.{method.Name}";
        }

        internal static string MethodParams(params object[] args)
        {
            return $"{(String.Join(", ", args))}";
        }

        internal static string MethodFullName(MethodBase method, params object[] args)
        {
            return $"{MethodName(method)}({MethodParams(args)})";
        }

        internal static string MethodFullName(string method, params object[] args)
        {
            return $"{method}({MethodParams(args)})";
        }

        internal static void CheckRunningPrograms()
        {
            string[] processes = GetRunningGameProcesses();

            if (processes.Length > 0)
            {
                throw new Exception(StringLoader.GetText("exception_game_already_open", String.Join("/", processes)));
            }
        }

        internal static string[] GetRunningGameProcesses()
        {
            string[] processNames = new[] { Strings.FileName.GameExe, Strings.FileName.ClosersExe, Strings.FileName.LauncherExe };

            return processNames.SelectMany(pn => Process.GetProcessesByName(Path.GetFileNameWithoutExtension(pn))).Select(p => Path.GetFileName(Methods.GetProcessPath(p.Id))).Where(pn => processNames.Contains(pn)).ToArray();
        }

        private static string GetProcessPath(int processId)
        {
            var buffer = new StringBuilder(1024);
            IntPtr hprocess = NativeMethods.OpenProcess(NativeMethods.ProcessAccessFlags.QueryLimitedInformation, false, processId);
            if (hprocess != IntPtr.Zero)
            {
                try
                {
                    int size = buffer.Capacity;
                    if (NativeMethods.QueryFullProcessImageName(hprocess, 0, buffer, out size))
                    {
                        return buffer.ToString();
                    }
                }
                finally
                {
                    NativeMethods.CloseHandle(hprocess);
                }
            }

            throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        internal static string GetRegistryValue(RegistryKey regKey, string path, string varName)
        {
            return GetRegistryValue(regKey, path, varName, String.Empty);
        }

        internal static string GetRegistryValue(RegistryKey regKey, string path, string varName, object defaultValue)
        {
            using (RegistryKey key = regKey.OpenSubKey(path))
            {
                if (key == null)
                {
                    return defaultValue.ToString();
                }

                return Convert.ToString(key.GetValue(varName, defaultValue));
            }
        }
    }
}

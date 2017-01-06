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

using ClosersPatcher.General;
using ClosersPatcher.Helpers.GlobalVariables;
using MadMilkman.Ini;
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
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;

namespace ClosersPatcher.Helpers
{
    internal static class Methods
    {
        private static string DateFormat = "d/MMM/yyyy h:mm tt";

        internal static DateTime ParseDate(string date)
        {
            return DateTime.ParseExact(date, DateFormat, CultureInfo.InvariantCulture);
        }

        internal static string DateToString(DateTime date)
        {
            return date.ToString(DateFormat, CultureInfo.InvariantCulture);
        }

        internal static bool HasNewTranslations(Language language)
        {
            string directory = language.Name;

            if (!Directory.Exists(directory))
            {
                return true;
            }

            string filePath = Path.Combine(directory, Strings.IniName.Translation);
            if (!File.Exists(filePath))
            {
                return true;
            }

            IniFile ini = new IniFile();
            ini.Load(filePath);

            if (!ini.Sections.Contains(Strings.IniName.Patcher.Section))
            {
                return true;
            }

            IniSection section = ini.Sections[Strings.IniName.Patcher.Section];
            if (!section.Keys.Contains(Strings.IniName.Pack.KeyDate))
            {
                return true;
            }

            string date = section.Keys[Strings.IniName.Pack.KeyDate].Value;

            return language.LastUpdate > ParseDate(date);
        }

        internal static bool IsClosersPath(string path)
        {
            bool f1 = Directory.Exists(path);
            string dataPath = Path.Combine(path, Strings.FolderName.Data);
            bool f2 = Directory.Exists(dataPath);
            bool f3 = File.Exists(Path.Combine(path, Strings.FileName.GameExe));
            bool f4 = File.Exists(Path.Combine(path, Strings.IniName.ClientVer));
            bool f5 = File.Exists(Path.Combine(dataPath, Strings.FileName.CMFScriptPack));

            return f1 && f2 && f3 && f4 && f5;
        }

        internal static bool IsGameLatestVersion()
        {
            IniFile serverIni = GetIniFromUrl(Urls.ClosersSettingsHome + Strings.IniName.ClientVer);
            IniFile clientIni = new IniFile();
            clientIni.Load(Path.Combine(UserSettings.GamePath, Strings.IniName.ClientVer));
            
            string serverMVer = serverIni.Sections[Strings.IniName.Ver.Section].Keys[Strings.IniName.Ver.KeyMVer].Value;
            string clientMVer = clientIni.Sections[Strings.IniName.Ver.Section].Keys[Strings.IniName.Ver.KeyMVer].Value;
            if (clientMVer != serverMVer)
            {
                return false;
            }

            string serverTime = serverIni.Sections[Strings.IniName.Ver.Section].Keys[Strings.IniName.Ver.KeyTime].Value;
            string clientTime = clientIni.Sections[Strings.IniName.Ver.Section].Keys[Strings.IniName.Ver.KeyTime].Value;
            if (clientTime != serverTime)
            {
                return false;
            }

            return true;
        }

        internal static bool IsTranslationOutdated(Language language)
        {
            string selectedTranslationPath = Path.Combine(language.Name, Strings.IniName.Translation);
            if (!File.Exists(selectedTranslationPath))
            {
                return true;
            }

            IniFile translationIni = new IniFile();
            translationIni.Load(selectedTranslationPath);

            if (!translationIni.Sections[Strings.IniName.Patcher.Section].Keys.Contains(Strings.IniName.Ver.KeyMVer) ||
                !translationIni.Sections[Strings.IniName.Patcher.Section].Keys.Contains(Strings.IniName.Ver.KeyTime))
            {
                throw new Exception(StringLoader.GetText("exception_read_translation_ini"));
            }

            IniFile clientIni = new IniFile();
            clientIni.Load(Path.Combine(UserSettings.GamePath, Strings.IniName.ClientVer));

            string translationMVer = translationIni.Sections[Strings.IniName.Patcher.Section].Keys[Strings.IniName.Ver.KeyMVer].Value;
            string clientMVer = clientIni.Sections[Strings.IniName.Ver.Section].Keys[Strings.IniName.Ver.KeyMVer].Value;
            if (clientMVer != translationMVer)
            {
                return true;
            }

            string translationTime = translationIni.Sections[Strings.IniName.Patcher.Section].Keys[Strings.IniName.Ver.KeyTime].Value;
            string clientTime = clientIni.Sections[Strings.IniName.Ver.Section].Keys[Strings.IniName.Ver.KeyTime].Value;
            if (clientTime != translationTime)
            {
                return true;
            }

            return false;
        }

        internal static bool IsTranslationSupported(Language language)
        {
            IniFile clientIni = new IniFile();
            clientIni.Load(Path.Combine(UserSettings.GamePath, Strings.IniName.ClientVer));
            IniFile supportedIni = GetIniFromUrl(Urls.TranslationHome + language.Name + '/' + Strings.IniName.ClientVer);

            string translationMVer = supportedIni.Sections[Strings.IniName.Ver.Section].Keys[Strings.IniName.Ver.KeyMVer].Value;
            string clientMVer = clientIni.Sections[Strings.IniName.Ver.Section].Keys[Strings.IniName.Ver.KeyMVer].Value;
            if (clientMVer != translationMVer)
            {
                return false;
            }

            string translationTime = supportedIni.Sections[Strings.IniName.Ver.Section].Keys[Strings.IniName.Ver.KeyTime].Value;
            string clientTime = clientIni.Sections[Strings.IniName.Ver.Section].Keys[Strings.IniName.Ver.KeyTime].Value;
            if (clientTime != translationTime)
            {
                return false;
            }

            return true;
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
                    if (e.InnerException is SocketException)
                    {
                        var innerException = e.InnerException as SocketException;
                        if (innerException.SocketErrorCode == SocketError.ConnectionRefused)
                        {
                            Logger.Error(e);
                            MsgBox.Error(StringLoader.GetText("exception_server_refused_connection"));
                        }
                    }
                }

                return null;
            }
        }

        internal static string MethodName(MethodBase method)
        {
            return $"{method.ReflectedType.FullName}.{method.Name}";
        }

        internal static string MethodParams(params string[] args)
        {
            return $"{(String.Join(", ", args))}";
        }

        internal static string MethodFullName(MethodBase method, params string[] args)
        {
            return $"{MethodName(method)}({MethodParams(args)})";
        }

        internal static string MethodFullName(string method, params string[] args)
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
    }
}

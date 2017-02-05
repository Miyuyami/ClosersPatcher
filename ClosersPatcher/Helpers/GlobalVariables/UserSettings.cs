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

using ClosersPatcher.Properties;
using System;
using System.IO;
using System.Reflection;

namespace ClosersPatcher.Helpers.GlobalVariables
{
    internal static class UserSettings
    {
        internal static string PatcherPath
        {
            get
            {
                if (!Directory.Exists(Settings.Default.PatcherWorkingDirectory))
                {
                    return PatcherPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Assembly.GetExecutingAssembly().GetName().Name);
                }
                else
                {
                    return Settings.Default.PatcherWorkingDirectory.Replace("\\\\", "\\");
                }
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    value = value.Replace("\\\\", "\\");
                    Directory.CreateDirectory(value);
                    Directory.SetCurrentDirectory(value);
                }

                Settings.Default.PatcherWorkingDirectory = value;
                Settings.Default.Save();
                Logger.Info($"Patcher path set to [{value}]");
            }
        }

        internal static string GamePath
        {
            get
            {
                return Settings.Default.GameDirectory.Replace("\\\\", "\\");
            }
            set
            {
                value = value.Replace("\\\\", "\\");
                Settings.Default.GameDirectory = value;
                Settings.Default.Save();
                Logger.Info($"Soulworker path set to [{value}]");
            }
        }

        internal static string LanguageId
        {
            get
            {
                return Settings.Default.LanguageId;
            }
            set
            {
                Settings.Default.LanguageId = value;
                Settings.Default.Save();
            }
        }

        internal static string RegionId
        {
            get
            {
                return Settings.Default.RegionId;
            }
            set
            {
                Settings.Default.RegionId = value;
                Settings.Default.Save();
            }
        }

        internal static string UILanguageCode
        {
            get
            {
                return Settings.Default.UILanguage;
            }
            set
            {
                Settings.Default.UILanguage = value;
                Settings.Default.Save();
                Logger.Info($"UI Language set to [{value}]");
            }
        }

        internal static bool HasSound
        {
            get
            {
                return Settings.Default.Sound;
            }
            set
            {
                Settings.Default.Sound = value;
                Settings.Default.Save();
            }
        }
    }
}

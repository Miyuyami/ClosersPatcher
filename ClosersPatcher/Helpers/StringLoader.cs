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
using System;
using System.Globalization;

namespace ClosersPatcher.Helpers
{
    internal static class StringLoader
    {
        internal static string GetText(string name)
        {
            return GetText(name, UserSettings.UILanguageCode);
        }
        internal static string GetText(string name, params object[] args)
        {
            return String.Format(GetText(name, UserSettings.UILanguageCode), args);
        }

        private static string GetText(string name, string languageCode)
        {
            switchagain:
            switch (languageCode)
            {
                case "default":
                    languageCode = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
                    goto switchagain;
                case "en":
                    return Resources.en.ResourceManager.GetString(name);
                default:
                    return Resources.en.ResourceManager.GetString(name);
            }
        }
    }
}

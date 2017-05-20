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
    internal static class Uris
    {
        internal const string ClosersWebsite = "http://closershq.com/Discussion-Unofficial-Closers-English-Translation-Project";
        internal const string ClosersKRHome = "https://clogin.nexon.com/common/clogin.aspx?redirect=http%3a%2f%2fclosers.nexon.com%2f";
        internal const string ClosersJPHome = "http://cls.happytuk.co.jp/cls/index";

#if PUBLIC_TEST
        internal static string TranslationHome = System.AppDomain.CurrentDomain.BaseDirectory;
#elif DEBUG
        internal static string TranslationHome = System.Environment.ExpandEnvironmentVariables(@"%userprofile%\Documents\GitHub\ClosersPatcher\Translations\");
#else
        internal const string TranslationHome = "https://raw.githubusercontent.com/Miyuyami/ClosersPatcher/master/Translations/";
#endif

        internal const string ClosersSettingsHome = "http://closers.dn.nexoncdn.co.kr/CLOSERS_LIVE_NEXON/";
    }
}

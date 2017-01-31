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

namespace ClosersPatcher.General
{
    internal class ClosersFile
    {
        internal string Name { get; private set; }
        internal string Path { get; private set; }
        internal string PathD { get; private set; }

        internal ClosersFile(string name, string path, string pathD)
        {
            this.Name = name;
            this.Path = path;
            this.PathD = pathD;
        }
    }
}

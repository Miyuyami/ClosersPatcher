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

using System;

namespace ClosersPatcher.General
{
    public class Language
    {
        public string Name { get; private set; }
        public DateTime LastUpdate { get; private set; }

        public Language(string name)
        {
            this.Name = name;
        }

        public Language(string name, DateTime lastUpdate)
        {
            this.Name = name;
            this.LastUpdate = lastUpdate;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
                return false;
            Language language = obj as Language;
            return this.Name == language.Name;
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}

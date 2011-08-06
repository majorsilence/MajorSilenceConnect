/*
MajorSilenceConnect is a simple user interface using the main ultravnc server program (winvnc.exe) instead of single click.
Copyright (C) 2011  Peter Gill <peter@majorsilence.com>

This program is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation; either version 2
of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace MajorSilenceConnect
{
    class Globals
    {
        private static int vWinVncProcessID = -1;
        private static string vAddress = "";

        private Globals() { }


        public static string IPAddress
        {
            get { return vAddress; }
            set { vAddress = value; }
        }


        public static string MajorSilenceLocalAppDataDirectory
        {
            get
            {
                string msDir = null;
                msDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                msDir = System.IO.Path.Combine(msDir, "MajorSilence");
                return msDir;
            }
        }

        public static string MajorSilenceSupportLocalAppDataDirectory
        {
            get { return System.IO.Path.Combine(MajorSilenceLocalAppDataDirectory, "MajorSilenceConnect"); }
        }


        public static int WinVncProcessID
        {
            get { return vWinVncProcessID; }
            set { vWinVncProcessID = value; }
        }
    }
}

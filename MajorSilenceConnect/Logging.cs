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
    public class Logging
    {
        private static volatile Logging instance;
        private static object syncRoot = new Object();

        private Logging() { }

        public static Logging Instance
        {
          get 
          {
             if (instance == null) 
             {
                lock (syncRoot) 
                {
                    if (instance == null)
                    {
                        instance = new Logging();
                    }
                }
             }

             return instance;
          }
        }

        public void WriteLine(string msg)
        {
            WriteLine(msg, "UNKNOWN");
        }
        public void WriteLine(string msg, string category)
        {

            string filePath = System.IO.Path.Combine(Globals.MajorSilenceLocalAppDataDirectory, "MajorSilence-Connect-Debug.txt");

            System.IO.File.AppendAllText(filePath, DateTime.Now.ToString() + System.Environment.NewLine);
            System.IO.File.AppendAllText(filePath, category + System.Environment.NewLine);
            System.IO.File.AppendAllText(filePath, msg + System.Environment.NewLine);
            System.IO.File.AppendAllText(filePath, System.Environment.NewLine + System.Environment.NewLine);

        }

        public void WriteLine(Exception value)
        {
            WriteLine(value, "UNKNOWN");
        }
        public void WriteLine(Exception value, string category)
        {
            WriteLine(value.Message + System.Environment.NewLine + value.StackTrace, category);
        }

    }
}

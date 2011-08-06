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
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace MajorSilenceConnect
{
    class WinVncServer
    {

	    private string outputFolder;

        public WinVncServer()
	    {
            if (System.IO.Directory.Exists(Globals.MajorSilenceLocalAppDataDirectory) == false)
            {
                System.IO.Directory.CreateDirectory(Globals.MajorSilenceLocalAppDataDirectory);
		    }

            if (System.IO.Directory.Exists(Globals.MajorSilenceSupportLocalAppDataDirectory) == false)
            {
                System.IO.Directory.CreateDirectory(Globals.MajorSilenceSupportLocalAppDataDirectory);
		    }

		    outputFolder = Globals.MajorSilenceSupportLocalAppDataDirectory;
	    }

        /// <summary>
        /// Run the WinVNC.exe server
        /// </summary>
        /// <param name="port">Integer</param>
	    public void RunServer(int port)
	    {
		    Directory.SetCurrentDirectory(outputFolder);

		    ExtractToTemp();


		    // See: http://support1.uvnc.com:8080/index.php?section=16
		    string s = null;

		    s = string.Format(Helper.ProgramCulture(), "-sc_prompt -sc_exit -autoreconnect -connect {0}:{1} -run", Globals.IPAddress, port);
		    //s = String.Format("-sc_prompt -sc_exit -autoreconnect -connect 10.0.216.25:5500 -run")
		    //Parameters are order dependent !! 
		    //winvnc [-sc_promt] [-sc_exit]  [-id:????] [-autoreconnect[ ID:????]] [-connect host[:port]] [-connect host[::port]] [-run] 

		    System.Diagnostics.Process p = new System.Diagnostics.Process();
		    p.StartInfo.FileName = "winvnc.exe";
		    //System.IO.Path.Combine(Globals.ProcomSupportLocalAppDataDirectory, "winvnc.exe")
		    p.StartInfo.Arguments = s;
		    p.Start();
		    Globals.WinVncProcessID = p.Id;
	    }

	    ///
	    /// <summary>Extract the vnc files from the resource and write ultravnc.ini</summary>
	    ///

	    private void ExtractToTemp()
	    {

            System.IO.File.WriteAllText(System.IO.Path.Combine(outputFolder, "ultravnc.ini"), MajorSilenceConnect.Properties.Resources.ultravnc);

            FileStream cadStream = new FileStream(System.IO.Path.Combine(outputFolder, "cad.exe"), FileMode.Create);
            BinaryWriter cadWriter = new BinaryWriter(cadStream);
            cadWriter.Write(MajorSilenceConnect.Properties.Resources.cad);
            cadWriter.Close();
            cadStream.Close();

            FileStream MsStream = new FileStream(System.IO.Path.Combine(outputFolder, "MSRC4Plugin.dsm"), FileMode.Create);
            BinaryWriter MsWriter = new BinaryWriter(MsStream);
            MsWriter.Write(MajorSilenceConnect.Properties.Resources.MSRC4Plugin);
            MsWriter.Close();
            MsStream.Close();

            FileStream SCHookStream = new FileStream(System.IO.Path.Combine(outputFolder, "SCHook.dll"), FileMode.Create);
            BinaryWriter SCHookWriter = new BinaryWriter(SCHookStream);
            SCHookWriter.Write(MajorSilenceConnect.Properties.Resources.SCHook);
            SCHookWriter.Close();
            SCHookStream.Close();

            FileStream VncHookStream = new FileStream(System.IO.Path.Combine(outputFolder, "vnchooks.dll"), FileMode.Create);
            BinaryWriter VncHookWriter = new BinaryWriter(VncHookStream);
            VncHookWriter.Write(MajorSilenceConnect.Properties.Resources.vnchooks);
            VncHookWriter.Close();
            VncHookStream.Close();

            FileStream winVncStream = new FileStream(System.IO.Path.Combine(outputFolder, "winvnc.exe"), FileMode.Create);
            BinaryWriter winVncWriter = new BinaryWriter(winVncStream);
            winVncWriter.Write(MajorSilenceConnect.Properties.Resources.winvnc);
            winVncWriter.Close();
            winVncStream.Close();

	    }

    }
}

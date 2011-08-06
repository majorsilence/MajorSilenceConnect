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
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Globalization;


[assembly: CLSCompliant(true)]

namespace MajorSilenceConnect
{
    /// <summary>
    /// Description of MainForm.
    /// </summary>
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

            DataSet ds = new DataSet();
            ds.Locale = CultureInfo.InvariantCulture;

            ds.ReadXml(new StringReader(MajorSilenceConnect.Properties.Resources.helpdesk));
            lstSupport.DataSource = ds.Tables["helpdesk"];
            lstSupport.DisplayMember = "support";
            lstSupport.ValueMember = "address";

        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.majorsilence.com/");
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lstSupport_DoubleClick(object sender, EventArgs e)
        {
            if (lstSupport.SelectedIndex < 0)
            {
                return;
            }

            int port = 0;
            string value = null;
            value = lstSupport.SelectedValue.ToString();

            port = Convert.ToInt32(value.Split(char.Parse(":"))[1], Helper.ProgramCulture());
            Globals.IPAddress = value.Split(char.Parse(":"))[0];

            WinVncServer f = new WinVncServer();
            try
            {
                f.RunServer(port);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error running MajorSilence Software Support." + System.Environment.NewLine + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            bwMonitorWinVnc.RunWorkerAsync(Globals.WinVncProcessID);

            this.Hide();
            //Notify user that connection has started
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.ShowBalloonTip(5000);

        }

        private void bwMonitorWinVnc_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            int id = 0;
            id = Convert.ToInt32(e.Argument.ToString(), Helper.ProgramCulture());
            p = System.Diagnostics.Process.GetProcessById(id);

            while (true)
            {

                if (p.HasExited == true)
                {
                    break; 
                }

                System.Threading.Thread.Sleep(1000);
            }

            Application.Exit();

        }

        private void exitMajorSilenceSupportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            this.notifyIcon1.ContextMenuStrip.Show();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            //Try to kill winvnc on exit in case it is still running.
            //Since this is a single instance app there should only be one instance running
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            if (Globals.WinVncProcessID != -1)
            {
                p = System.Diagnostics.Process.GetProcessById(Globals.WinVncProcessID);
                p.Kill();
                p.WaitForExit();  // give time to exit before proceeding to try the delete
            }

            //System.Diagnostics.Process.Start(System.IO.Path.Combine(Globals.ProcomSupportLocalAppDataDirectory, "winvnc.exe") & " -kill")
            //System.Threading.Thread.Sleep(5000)

            try
            {
                // Since this is a single instance app there should only be able to be one copy of this directory
                // So we get rid of our temp files when finished.
                //System.IO.Directory.Delete(Globals.MajorSilenceSupportLocalAppDataDirectory, true);
                if (System.IO.Directory.Exists(Globals.MajorSilenceSupportLocalAppDataDirectory))
                {
                    DeleteFilesRecursively(new DirectoryInfo(Globals.MajorSilenceSupportLocalAppDataDirectory));
                }

            }
            catch (UnauthorizedAccessException uex)
            {
                MessageBox.Show("Error cleaning up temp support files." + uex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (IOException iex)
            {
                MessageBox.Show("Error cleaning up temp support files." + iex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            MessageBox.Show("MajorSilenceConnect Software Support is now closed.", "MajorSilenceConnect Support Closed", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        public void DeleteFilesRecursively(DirectoryInfo target)
        {
            foreach (DirectoryInfo dir in target.GetDirectories())
            {
                DeleteFilesRecursively(dir);
            }
            foreach (FileInfo file in target.GetFiles())
            {
                file.Delete();
            }
        }



    }
}

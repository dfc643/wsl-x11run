using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wsl_x11run
{
    public partial class Form1 : Form
    {
        public String[] args;
        public Form1()
        {
            InitializeComponent();
        }


        public static string BashCommand(string command)
        {

            ProcessStartInfo procStartInfo = new ProcessStartInfo("bash", "-c 'DISPLAY=:0.0 " + command + "'")
            {
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process proc = new Process())
            {
                proc.StartInfo = procStartInfo;
                proc.Start();

                string output = proc.StandardOutput.ReadToEnd();

                if (string.IsNullOrEmpty(output))
                    output = proc.StandardError.ReadToEnd();

                return output;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Hide();
            string cmd = string.Join(" ", args);
            BashCommand(cmd);
            Application.Exit();
        }
    }
}

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

namespace x11run
{
    public partial class Form1 : Form
    {
        public string[] args;
        private string x11mode = Properties.Settings.Default["mode"].ToString();
        private string x11sshIp = Properties.Settings.Default["ssh_ip"].ToString();
        private string x11sshPort = Properties.Settings.Default["ssh_port"].ToString();
        private string x11sshUser = Properties.Settings.Default["ssh_user"].ToString();
        private string x11sshPwd = Properties.Settings.Default["ssh_password"].ToString();
        private string x11servIp = Properties.Settings.Default["xserver_ip"].ToString();
        private string x11servPort = Properties.Settings.Default["xserver_port"].ToString();

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 调用 Win32 方法隐藏命令框执行命令
        /// </summary>
        /// <param name="img">被执行主进映像名</param>
        /// <param name="args">执行时附加参数</param>
        /// <returns>执行结果</returns>
        public static string RunCommand(string img, string args)
        {
            ProcessStartInfo procStartInfo = new ProcessStartInfo(img, args)
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

        /// <summary>
        /// 通过不同的方式来实现 X11 程序运行
        /// </summary>
        /// <param name="command">X11 程序及命令参数</param>
        /// <returns>执行结果</returns>
        public string X11Run(string command)
        {
            string result = "";
            switch (x11mode)
            {
                case "wsl":
                case "wsl2":
                    result = RunCommand("bash", "-c 'DISPLAY=" + x11servIp + ":" + x11servPort + " " + command + "'");
                    break;

                case "ssh":
                    result = RunCommand("cmd", "/c echo y | plink " + x11sshUser + "@" + x11sshIp + " -P " + x11sshPort + " -pw " + x11sshPwd + " \"DISPLAY=" + x11servIp + ":" + x11servPort + " " + command + "\"");
                    break;

                default:
                    MessageBox.Show("Unsupported mode：\n\n1. wsl: support WSL and WSL2 on Windows 10\n2. ssh: support all linux container or remote linux machine",
                        "Unsupported mode", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
            }
            return result;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Hide();
            string cmd = string.Join(" ", args);
            X11Run(cmd);
            Application.Exit();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CPtest2
{
    public partial class Form1 : Form
    {
        Thread t;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            t = new Thread(Thread1);//前台线程
            t.IsBackground = true;//设置为后台线程
            t.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (t != null)
            {
                t.Abort();
            }
            textBox1.Clear();
            textBox2.Clear();
            textBox1.Focus();
        }

        void Thread1()
        {
            using (Process p = new Process())
            {
                string filename = "3.exe";
                string strInput = textBox1.Text;
                ProcessStartInfo startInfo = new ProcessStartInfo(filename, strInput);
                p.StartInfo = startInfo;
                p.StartInfo.UseShellExecute = false;
                // 接受来自调用程序的输入信息
                p.StartInfo.RedirectStandardInput = true;
                //输出信息
                p.StartInfo.RedirectStandardOutput = true;
                // 输出错误
                p.StartInfo.RedirectStandardError = true;
                //不显示程序窗口
                p.StartInfo.CreateNoWindow = true;
                p.Start();
                p.StandardInput.WriteLine(strInput + "&exit");
                string strOuput = p.StandardOutput.ReadToEnd();
                p.WaitForExit();
                p.Close();
                textBox2.Text = strOuput;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
        }
    }
}

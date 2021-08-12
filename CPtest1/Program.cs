using System;
using System.Diagnostics;

namespace CPtest1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Process p = new Process())
            {
                while (true)
                {
                    string filename = "3.exe";
                    Console.WriteLine("请输入要翻译的单词:");
                    string strInput = Console.ReadLine();
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
                    Console.WriteLine(strOuput);
                    Console.ReadKey();
                }
            }
        }
    }
}

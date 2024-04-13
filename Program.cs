using System;
using System.Management;
using System.Diagnostics;

namespace USBMonitor
{
    class Program
    {
        static Process mspaint = null;

        static void Main(string[] args)
        {
            ManagementEventWatcher watcher = new ManagementEventWatcher(new WqlEventQuery("SELECT * FROM Win32_DeviceChangeEvent WHERE EventType = 2"));
            watcher.EventArrived += (s, e) =>
            {
                if (mspaint == null || mspaint.HasExited)
                {
                    mspaint = new Process();
                    mspaint.StartInfo.FileName = "mspaint.exe";
                    mspaint.Start();
                    //Process.Start("mspaint.exe");
                }
            };
            watcher.Start();
            Console.WriteLine("Enter to exit.");
            Console.ReadLine();
            watcher.Stop();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Diagnostics;
//using System.Threading;
//using System.Collections;
//using System.IO;
using System.Timers;

namespace CPUWatcher
{
    class Program
    {
        static Timer timer = new Timer(1000);

        static void Main(string[] args)
        {
            timer.Elapsed += new ElapsedEventHandler(clockElapsed);
            timer.Start();
            Console.ReadLine();
        }

        static void clockElapsed(object sender, EventArgs e)
        {
            string procName = "High CPU Test";
            Process[] runningNow = Process.GetProcessesByName(procName);
            if (runningNow.Length > 1)
            {
                Console.WriteLine(runningNow.Length + " Processes FOUND (ambiguous)");
            }
            else if (runningNow.Length > 0)
            {
                var cpuCounter = new PerformanceCounter("Process", "% Processor Time", runningNow[0].ProcessName, true);
                double pct = cpuCounter.NextValue();
                System.Threading.Thread.Sleep(150); // need minimum 100ms to make an accurated value
                pct = cpuCounter.NextValue() / Environment.ProcessorCount;
                Console.WriteLine("Process FOUND running at " + pct + "%");
            }
            else
            {
                Console.WriteLine("Process NOT found");
            }
        }
    }
}

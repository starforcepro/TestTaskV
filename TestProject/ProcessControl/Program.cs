using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace ProcessControl
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var startTime = DateTime.Now.Ticks;
            
            if (args.Length != 3)
                throw new ArgumentException("app can handle only three arguments");
            if (!int.TryParse(args[1], out var lifeTimeMinutes))
                throw new ArgumentException("second argument(life time) should be int");
            if (!int.TryParse(args[2], out var checkFrequencyMinutes))
                throw new ArgumentException("third argument(check frequency) should be int");
            var processName = args[0];

            var needToKillProcesses = true;
            var processes = new Process[0];
            while (DateTime.Now.Ticks < startTime + TimeSpan.FromMinutes(lifeTimeMinutes).Ticks)
            {
                processes = Process.GetProcessesByName(processName);

                if (processes.Length == 0)
                {
                    needToKillProcesses = false;

                    break;
                }
                
                Thread.Sleep(TimeSpan.FromMinutes(checkFrequencyMinutes));
            }

            if (needToKillProcesses)
                processes.ToList().ForEach(x => x.Kill());

            Console.WriteLine("processes are ended");
        }
    }
}
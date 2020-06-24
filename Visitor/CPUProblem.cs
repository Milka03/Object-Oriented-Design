using System;
using Solvers;

namespace Problems
{
    class CPUProblem : Problem
    {
        public int RequiredThreads { get; }

        public CPUProblem(string name, Func<int> computation, int requiredThreads) : base(name, computation)
        {
            RequiredThreads = requiredThreads;
        }


        //------------- Visitor methods for CPU Problem ---------------------
        public override void VisitCPU(CPU cpu)
        {
            if (RequiredThreads <= cpu.Threads)
            {
                TryMarkAsSolved(Computation());
                Console.WriteLine($"CPU {cpu.Model} solved {Name}");
                return;
            }
            Console.WriteLine($"CPU {cpu.Model} cannot solve {Name}");
        }

        public override void VisitGPU(GPU gpu)
        {
            gpu.Temperature += (RequiredThreads * gpu.GetCPUMultiplier());
            TryMarkAsSolved(Computation());
            Console.WriteLine($"GPU {gpu.Model} solved {Name} (temperature: {gpu.Temperature})");
        }

        public override void VisitEthernet(Ethernet e)
        {
            Console.WriteLine($"Ethernet {e.Model} cannot solve {Name}");
        }

        public override void VisitWiFi(WiFi wifi)
        {
            Console.WriteLine($"WiFi {wifi.Model} cannot solve {Name}");
        }

    }
}
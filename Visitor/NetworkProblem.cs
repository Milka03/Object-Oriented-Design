using System;
using Solvers;

namespace Problems
{
    class NetworkProblem : Problem
    {
        public int DataToTransfer { get; }

        public NetworkProblem(string name, Func<int> computation, int dataToTransfer) : base(name, computation)
        {
            DataToTransfer = dataToTransfer;
        }

        //------------- Visitor methods for NetworkProblem ---------------------

        public override void VisitCPU(CPU cpu)
        {
            Console.WriteLine($"CPU {cpu.Model} cannot solve {Name}");
        }

        public override void VisitGPU(GPU gpu)
        {
            Console.WriteLine($"GPU {gpu.Model} cannot solve {Name}");
        }

        public override void VisitEthernet(Ethernet e)
        {
            if(DataToTransfer > e.DataLimit)
            {
                Console.WriteLine($"Ethernet {e.Model} cannot solve {Name}");
                return;
            }
            TryMarkAsSolved(Computation());
            Console.WriteLine($"Ethernet {e.Model} solved {Name}");
        }

        public override void VisitWiFi(WiFi wifi)
        {
            if (DataToTransfer <= wifi.DataLimit)
            {
                if (wifi.Rng.NextDouble() < wifi.PacketLossChance)
                {
                    Console.WriteLine($"WiFi {wifi.Model} cannot solve {Name}");
                    return;
                }
                TryMarkAsSolved(Computation());
                Console.WriteLine($"WiFi {wifi.Model} solved {Name}");
                return;
            }
            Console.WriteLine($"WiFi {wifi.Model} cannot solve {Name}");
        }


    }
}
using System;
using Solvers;

namespace Problems
{
    class GPUProblem : Problem
    {
        public int GpuTemperatureIncrease { get; }

        public GPUProblem(string name, Func<int> computation, int gpuTemperatureIncrease) : base(name, computation)
        {
            GpuTemperatureIncrease = gpuTemperatureIncrease;
        }

        //------------- Visitor methods for GPU Problem ---------------------
        public override void VisitCPU(CPU cpu)
        {
            Console.WriteLine($"CPU {cpu.Model} cannot solve {Name}");
        }

        public override void VisitGPU(GPU gpu)
        {
            if (gpu.DidThermalThrottle())
            {
                return;
            }
            gpu.Temperature += GpuTemperatureIncrease;
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
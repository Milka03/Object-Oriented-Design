using System;
using Problems;

namespace Solvers
{
    class GPU : ISolver
    {
        static private int MaxTemperature { get; } = 100;
        static private int CPUProblemTemperatureMultiplier { get; } = 3;

        private readonly string model;
        private int temperature;
        private int coolingFactor;

        public int Temperature
        {
            get { return temperature; }
            set { temperature = value; }
        }
        public string Model { get { return model; } }

        public GPU(string model, int temperature, int coolingFactor)
        {
            this.model = model;
            this.temperature = temperature;
            this.coolingFactor = coolingFactor;
        }

        public bool DidThermalThrottle()
        {
            if (temperature > MaxTemperature)
            {
                Console.WriteLine($"GPU {model} thermal throttled");
                CoolDown();
                return true;
            }
            return false;
        }

        private void CoolDown()
        {
            temperature -= coolingFactor;
        }

        public int GetMaxTempeature() => MaxTemperature;
        public int GetCPUMultiplier() => CPUProblemTemperatureMultiplier;

        public void Accept(Problem visitor)
        {
            visitor.VisitGPU(this);
        }
    }
}
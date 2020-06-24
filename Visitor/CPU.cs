using System;
using Problems;

namespace Solvers
{
    class CPU : ISolver
    {
        private readonly string model;
        private readonly int threads;

        public CPU(string model, int threads)
        {
            this.model = model;
            this.threads = threads;
        }

        public int Threads { get { return threads; } }
        public string Model { get { return model; } }

        public void Accept(Problem visitor)
        {
            visitor.VisitCPU(this);
        }
    }
}
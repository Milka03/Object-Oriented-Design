using System;
using Solvers;

namespace Problems
{

    abstract class Problem  //IVisitor
    {
        public string Name { get; }
        public Func<int> Computation { get; }

        public bool Solved { get; private set; }
        public int? Result { get; private set; }

        protected Problem(string name, Func<int> computation)
        {
            Name = name;
            Computation = computation;
        }
        
        protected void TryMarkAsSolved(int? result)
        {
            if (result.HasValue)
                MarkAsSolved(result.Value);
        }

        private void MarkAsSolved(int result)
        {
            Solved = true;
            Result = result;
        }

        //------------ Visitor methods ----------------
        public abstract void VisitCPU(CPU cpu);
        public abstract void VisitGPU(GPU gpu);
        public abstract void VisitEthernet(Ethernet e);
        public abstract void VisitWiFi(WiFi wifi);

    }
}
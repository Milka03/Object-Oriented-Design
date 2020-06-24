using System;
using System.Collections.Generic;
using System.Linq;
using ResultsCombiners;
using Solvers;

namespace Problems
{
    class CompositeProblem : Problem
    {
        private readonly IEnumerable<Problem> problems;
        private readonly IResultsCombiner resultsCombiner;
        private List<int> results = new List<int>();

        public CompositeProblem(string name, IEnumerable<Problem> problems,
            IResultsCombiner resultsCombiner) : base(name, () => 0)
        {
            this.problems = problems;
            this.resultsCombiner = resultsCombiner;
        }

        public bool AllSolved()
        {
            foreach(Problem p in problems)
            {
                if (!p.Solved) return false;
            }
            return true;
        }

        //----------- Visitor methods for CompositeProblem ----------------
        public override void VisitCPU(CPU cpu)
        {
            foreach (Problem p in problems)
            {
                if (!p.Solved)
                {
                    p.VisitCPU(cpu);
                    if (p.Solved)
                    {
                        results.Add((int)p.Result);
                        Console.WriteLine($"    Result of {p.Name}: {p.Result}");
                    }
                }
            }

            if (AllSolved())
            {
                TryMarkAsSolved(resultsCombiner.CombineResults(results));
                //Console.WriteLine($"Composite Problem {Name} solved,");
            }
        }

        public override void VisitGPU(GPU gpu)
        {
            foreach (Problem p in problems)
            {
                if (!p.Solved)
                {
                    p.VisitGPU(gpu);
                    if (p.Solved)
                    {
                        results.Add((int)p.Result);
                        Console.WriteLine($"    Result of {p.Name}: {p.Result}");
                    }
                }
            }
            if (AllSolved()) TryMarkAsSolved(resultsCombiner.CombineResults(results));
        }

        public override void VisitEthernet(Ethernet e)
        {
            foreach (Problem p in problems)
            {
                if (!p.Solved)
                {
                    p.VisitEthernet(e);
                    if (p.Solved)
                    {
                        results.Add((int)p.Result);
                        Console.WriteLine($"    Result of {p.Name}: {p.Result}");
                    }
                }
            }
            if (AllSolved()) TryMarkAsSolved(resultsCombiner.CombineResults(results));
        }

        public override void VisitWiFi(WiFi wifi)
        {
            foreach (Problem p in problems)
            {
                if (!p.Solved)
                {
                    p.VisitWiFi(wifi);
                    if (p.Solved)
                    {
                        results.Add((int)p.Result);
                        Console.WriteLine($"    Result of {p.Name}: {p.Result}");
                    }
                }
            }
            if (AllSolved()) TryMarkAsSolved(resultsCombiner.CombineResults(results));
        }

    }
}
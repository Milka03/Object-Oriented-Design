
// I certify that this assignment is my own work entirely, performed independently and without any help from the sources which are not allowed.
// Emilia Wróblewska

using System;
using System.Collections.Generic;
using System.Text;
using BigTask2.Api;
using BigTask2.Data;
using BigTask2.Problems;
using BigTask2.Interfaces;

namespace BigTask2.Algorithms
{
    interface IAlgorithm
    {
        IEnumerable<Route> AcceptProblem(IRouteProblem route, string algorithm); //Accept(visitor)

    }

    class BFSSolver : IAlgorithm
    {
        public IEnumerable<Route> AcceptProblem(IRouteProblem route, string algorithm)
        {
            return route.CreateBFS(algorithm);
        }
    }

    class DFSSolver : IAlgorithm
    {
        public IEnumerable<Route> AcceptProblem(IRouteProblem route, string algorithm)
        {
            return route.CreateDFS(algorithm);
        }
    }

    class DijkstraSolver : IAlgorithm
    {
        public IEnumerable<Route> AcceptProblem(IRouteProblem route, string algorithm)
        {
            return route.CreateDijkstra(algorithm);
        }
    }

}

//This file Can be modified
// I certify that this assignment is my own work entirely, performed independently and without any help from the sources which are not allowed.
// Emilia Wróblewska

using System.Collections.Generic;
using BigTask2.Data;
using BigTask2.Api;
using BigTask2.Algorithms;

namespace BigTask2.Interfaces
{
    interface IRouteProblem
	{
        IGraphDatabase Graph { get; set; }

        //------------ Visitor methods ----------------
        IEnumerable<Route> CreateBFS(string name);
        IEnumerable<Route> CreateDFS(string name);
        IEnumerable<Route> CreateDijkstra(string name);

        //------------- Chain methods ----------------
        IRouteProblem Handle(string problem);
        void setNextMachine(IRouteProblem machine);
    }
}

//This file Can be modified
// I certify that this assignment is my own work entirely, performed independently and without any help from the sources which are not allowed.
// Emilia Wróblewska

using System.Collections.Generic;
using BigTask2.Data;
using BigTask2.Algorithms;
using BigTask2.Interfaces;
using BigTask2.Api;
using System;

namespace BigTask2.Problems
{
	class CostProblem : IRouteProblem
	{
		public string From, To;
		public CostProblem(string from, string to)
		{
			From = from;
			To = to;
		}

        public IGraphDatabase Graph { get; set; }

		public IRouteProblem nextMachine; //next problem

		//---------------- Visitor methods -------------------
		public IEnumerable<Route> CreateBFS(string name)
		{
			if (name == "BFS")
			{
				BFS bfs = new BFS();
				return bfs.Solve(Graph, Graph.GetByName(From), Graph.GetByName(To));
			}
			return null;
		}

		public IEnumerable<Route> CreateDFS(string name)
		{
			if (name == "DFS")
			{
				DFS bfs = new DFS();
				return bfs.Solve(Graph, Graph.GetByName(From), Graph.GetByName(To));
			}
			return null;
		}

		public IEnumerable<Route> CreateDijkstra(string name)
		{
			if (name == "Dijkstra")
			{
				//Console.WriteLine("Dijkstra Cost");
				DijkstraCost bfs = new DijkstraCost();
				return bfs.Solve(Graph, Graph.GetByName(From), Graph.GetByName(To));
			}
			return null;
		}

		//---------------- Chain methods -------------------
		public void setNextMachine(IRouteProblem codec)
		{
			nextMachine = codec;
		}

		public IRouteProblem Handle(string problem)
		{
			if (problem == "Cost")
			{
				return this;
			}
			else if (nextMachine != null) return nextMachine.Handle(problem);
			else return null;
		}
	}
}

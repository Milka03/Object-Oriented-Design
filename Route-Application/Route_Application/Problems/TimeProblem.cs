//This file Can be modified
// I certify that this assignment is my own work entirely, performed independently and without any help from the sources which are not allowed.
// Emilia Wróblewska

using System;
using BigTask2.Data;
using BigTask2.Interfaces;
using BigTask2.Algorithms;
using BigTask2.Api;
using System.Collections.Generic;

namespace BigTask2.Problems
{
	class TimeProblem : IRouteProblem
	{
		public IRouteProblem nextMachine; //next problem
		public IGraphDatabase Graph { get; set; }
        public string From, To;
		public TimeProblem(string from, string to)
		{
			From = from;
			To = to;
		}

		
		//------------------ Visitor methods --------------------
		public IEnumerable<Route> CreateBFS(string name)
		{
			if(name == "BFS")
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
				//Console.WriteLine("Dijkstra Time");
				DijkstraTime bfs = new DijkstraTime();
				return bfs.Solve(Graph, Graph.GetByName(From), Graph.GetByName(To));
			}
			return null;
		}

		//-------------------- Chain methods ------------------
		public void setNextMachine(IRouteProblem codec)
		{
			nextMachine = codec;
		}

		public IRouteProblem Handle(string problem)
		{
			if (problem == "Time")
			{
				return this;
			}
			else if (nextMachine != null) return nextMachine.Handle(problem);
			else return null;
		}
	}
}

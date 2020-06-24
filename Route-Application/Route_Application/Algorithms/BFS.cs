//This file contains fragments that You have to fulfill
// I certify that this assignment is my own work entirely, performed independently and without any help from the sources which are not allowed.
// Emilia Wróblewska

using BigTask2.Api;
using BigTask2.Data;
using System.Collections.Generic;

namespace BigTask2.Algorithms
{
	public class BFS
	{
		public IEnumerable<Route> Solve(IGraphDatabase graph, City from, City to)
		{
			Dictionary<City, Route> routes = new Dictionary<City, Route>();
			routes[from] = null;
			Queue<City> queue = new Queue<City>();
			queue.Enqueue(from);
			do
			{
				City city = queue.Dequeue();
				/*
				 * For each outgoing route from city...
				 */
				foreach(Route route in graph.GetRoutesFrom(graph.GetByName(city.Name)))
				{
					//Route route = null; /* Change to current Route*/
					if (routes.ContainsKey(route.To))
					{
						continue;
					}
					routes[route.To] = route;
					if (route.To == to)
					{
						break;
					}
					queue.Enqueue(route.To);
				}
			} while (queue.Count > 0);
			if (!routes.ContainsKey(to))
			{
				return null;
			}
			List<Route> result = new List<Route>();
			for (Route route = routes[to]; route != null; route = routes[route.From])
			{
				result.Add(route);
			}
			result.Reverse();
			return result;
		}
	}
}

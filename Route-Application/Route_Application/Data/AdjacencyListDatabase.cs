//This file contains fragments that You have to fulfill
// I certify that this assignment is my own work entirely, performed independently and without any help from the sources which are not allowed.
// Emilia Wróblewska

using BigTask2.Api;
using System.Collections.Generic;
using System;
using System.Linq;

namespace BigTask2.Data
{
	class AdjacencyListDatabase : IGraphDatabase
    {
		private Dictionary<string, City> cityDictionary = new Dictionary<string, City>();
		private Dictionary<City, List<Route>> routes = new Dictionary<City, List<Route>>();
		
		private void AddCity(City city)
		{
			if (!cityDictionary.ContainsKey(city.Name))
				cityDictionary[city.Name] = city;
		}
		public AdjacencyListDatabase(IEnumerable<Route> routes)
		{
			foreach(Route route in routes)
			{
				AddCity(route.From);
				AddCity(route.To);
				if (!this.routes.ContainsKey(route.From))
				{
					this.routes[route.From] = new List<Route>();
				}
				this.routes[route.From].Add(route);
			}
		}
		public AdjacencyListDatabase()
		{
		}
		public void AddRoute(City from, City to, double cost, double travelTime, VehicleType vehicle)
		{
			AddCity(from);
			AddCity(to);
			if (!routes.ContainsKey(from))
			{
				routes[from] = new List<Route>();
			}
			routes[from].Add(new Route { From = from, To = to, Cost = cost, TravelTime = travelTime, VehicleType = vehicle});
		}

		public virtual IEnumerable<Route> GetRoutesFrom(City from)
		{
			List<Route> result = new List<Route>();
			if (routes.ContainsKey(from) && routes[from] != null)
			{
				foreach (Route r in routes[from])
				{
					if (r == null) continue;
					result.Add(r);
				}
			}
			return result;
		}

		public virtual City GetByName(string cityName)
		{
			return cityDictionary.GetValueOrDefault(cityName);
		}


		//-------------> Methods for Iterator  <--------------
		public IGraphIterator CreateIterator()
		{
			return new AdjacencyListDatabaseIterator(this);
		}
		public int Count
		{
			get { return routes.Count; }
		}
		public int CountLIST(City Index)
		{
			return routes[Index].Count;
		}
		//Get item from collection
		public Route GetRoute(City dictIndex, int ListIndex)
		{
			return routes[dictIndex][ListIndex];
		}
		public City GetCityFromPosition(int Index)
		{
			var list = routes.ToList();
			City tmp = list[Index].Key;
			return tmp;
		}
	}


	// ------------------ ITERATOR -------------------------
	class AdjacencyListDatabaseIterator : IGraphIterator
	{
		private AdjacencyListDatabase database;
		private Route current;
		private int dictPosition = 0;
		private int listPosition = 0;

		public AdjacencyListDatabaseIterator(AdjacencyListDatabase db)
		{
			database = db;
			current = db.GetRoute(db.GetCityFromPosition(0), 0);
		}

		public Route CurrentItem()
		{
			return current;
		}

		public Route Next()
		{
			listPosition++;
			if (listPosition >= database.CountLIST(database.GetCityFromPosition(dictPosition)))
			{
				dictPosition++;
				listPosition = 0;
			}
			if (dictPosition >= database.Count)
			{
				return null;
			}
			current = database.GetRoute(database.GetCityFromPosition(dictPosition), listPosition);
			if (current == null) return Next();
			else return current;
		}
		
		public Route First()
		{
			dictPosition = 0;
			listPosition = 0;
			current = database.GetRoute(database.GetCityFromPosition(dictPosition), listPosition);
			return current;
		}
	}


	//--------------------- DECORATOR -----------------------
	class AdjacencyDatabaseFilter : AdjacencyListDatabase
	{
		private Filter filter;
		public AdjacencyDatabaseFilter(IEnumerable<Route> routes, Filter f) : base(routes)
		{
			filter = f;
		}

		public override IEnumerable<Route> GetRoutesFrom(City from)
		{
			List<Route> result = new List<Route>();
			foreach (Route r in base.GetRoutesFrom(from))
			{
				if (!filter.AllowedVehicles.Contains(r.VehicleType)) continue;
				if (filter.RestaurantRequired && (!r.From.HasRestaurant || !r.To.HasRestaurant)) continue;
				if (r.From.Population < filter.MinPopulation || r.To.Population < filter.MinPopulation) continue;
				result.Add(r);
			}
			return result;
		}

		public override City GetByName(string cityName)
		{
			if (filter.RestaurantRequired && !base.GetByName(cityName).HasRestaurant || filter.MinPopulation > base.GetByName(cityName).Population)
				return new City();
			return base.GetByName(cityName);
		}
	}
}

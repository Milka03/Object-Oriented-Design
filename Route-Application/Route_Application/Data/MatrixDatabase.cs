//This file contains fragments that You have to fulfill
// I certify that this assignment is my own work entirely, performed independently and without any help from the sources which are not allowed.
// Emilia Wróblewska

using BigTask2.Api;
using System.Collections.Generic;
using System.Linq;
using System;

namespace BigTask2.Data
{
	class MatrixDatabase : IGraphDatabase
	{
		private Dictionary<City, int> cityIds = new Dictionary<City, int>();
		private Dictionary<string, City> cityDictionary = new Dictionary<string, City>();
		private List<List<Route>> routes = new List<List<Route>>();

		private void AddCity(City city)
		{
			if (!cityDictionary.ContainsKey(city.Name))
			{
				cityDictionary[city.Name] = city;
				cityIds[city] = cityIds.Count;
				foreach (var routes in routes)
				{
					routes.Add(null);
				}
				routes.Add(new List<Route>(Enumerable.Repeat<Route>(null, cityDictionary.Count)));
			}
		}
		public MatrixDatabase(IEnumerable<Route> routes)
		{
            foreach (var route in routes)
			{
				AddCity(route.From);
				AddCity(route.To);
			}
			foreach (var route in routes)
			{
				this.routes[cityIds[route.From]][cityIds[route.To]] = route;
			}
		}

		public void AddRoute(City from, City to, double cost, double travelTime, VehicleType vehicle)
		{
			AddCity(from);
			AddCity(to);
			routes[cityIds[from]][cityIds[to]] = new Route { From = from, To = to, Cost = cost, TravelTime = travelTime, VehicleType = vehicle };
		}

		public virtual IEnumerable<Route> GetRoutesFrom(City from)
		{
			List<Route> result = new List<Route>();
			if (cityDictionary.ContainsKey(from.Name) && routes[cityIds[from]] != null)
			{
				foreach (Route r in routes[cityIds[from]])
				{
					if (r == null) continue;
					//Console.WriteLine("Route: " + r.From.Name + "-" + r.To.Name + "   " + r.TravelTime);
					result.Add(r);
				}
			}
			return result;
		}

		public virtual City GetByName(string cityName)
		{
			return cityDictionary[cityName];
		}


		//------------> Methods for Iterator  <---------------
		public IGraphIterator CreateIterator()
		{
			return new MatrixDatabaseIterator(this);
		}
		public int Count()
		{
			return routes.Count; 
		}

		public int CountIN(int ListPosition)
		{
			return routes[ListPosition].Count;
		}

		//Get item from collection
		public Route GetRoute(int Index1, int Index2)
		{
			return routes[Index1][Index2];
		}
	}


	// ------------------ ITERATOR -------------------------
	class MatrixDatabaseIterator : IGraphIterator
	{
		private MatrixDatabase database;
		private Route current;
		private int position = 0;
		private int positionInside = 0;
		//List
		public MatrixDatabaseIterator(MatrixDatabase db)
		{
			database = db;
			current = db.GetRoute(0, 0);
		}

		public Route CurrentItem()
		{
			return current;
		}

		public Route Next()
		{
			positionInside++;
			if(positionInside >= database.CountIN(position))
			{
				position++;
				positionInside = 0;
			}
			if (position == database.Count())
			{
				//Console.WriteLine(database.Count() + "   " + position);
				return null;
			}
			current = database.GetRoute(position, positionInside);
			if (current == null) return Next();
			else return current;
		}
		
		public Route First()
		{
			position = 0;
			positionInside = 0;
			current = database.GetRoute(position, positionInside);
			if (current == null) return Next();
			return current;
		}
		
	}


	//------------------- FILTER ------------------------
	class MatrixDatabaseFilter : MatrixDatabase
	{
		private Filter filter;
		public MatrixDatabaseFilter(IEnumerable<Route> routes, Filter f) : base(routes)
		{
			filter = f;
		}

		public override IEnumerable<Route> GetRoutesFrom(City from)
		{
			List<Route> result = new List<Route>();
			foreach(Route r in base.GetRoutesFrom(from))
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

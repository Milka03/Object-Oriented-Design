using BigTask2.Api;
using BigTask2.Data;
using BigTask2.Interfaces;
using BigTask2.Problems;
using BigTask2.Ui;
using BigTask2.Algorithms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// I certify that this assignment is my own work entirely, performed independently and without any help from the sources which are not allowed.
// Emilia Wróblewska

namespace BigTask2
{
	class Program
	{
        static IEnumerable<Route> ServeRequest(Request request) 
        {
            (IGraphDatabase cars, IGraphDatabase trains) = MockData.InitDatabases();
            IGraphDatabase database = new AdjacencyDatabaseFilter(new List<Route>(), request.Filter);

            if (!ValidateRequest(request)) return null;
            if (request.Filter.AllowedVehicles.Contains(VehicleType.Car))
            {
                MergeDatabase(database, cars);
            }
            if (request.Filter.AllowedVehicles.Contains(VehicleType.Train))
            {
                MergeDatabase(database, trains);
            }
            //PrintDatabase(database);  //printing merged database

            IRouteProblem timeproblem = new TimeProblem(request.From, request.To);
            IRouteProblem costproblem = new CostProblem(request.From, request.To);
            timeproblem.Graph = database;
            costproblem.Graph = database;
            timeproblem.setNextMachine(costproblem);
            IRouteProblem finalproblem = timeproblem.Handle(request.Problem);
            
            List<IAlgorithm> solvers = new List<IAlgorithm> { new BFSSolver(), new DFSSolver(), new DijkstraSolver() };
            foreach (IAlgorithm alg in solvers)
            {
                var list = alg.AcceptProblem(finalproblem, request.Solver);
                if (list != null && list.Count() > 0)
                    return list;
            }
            return null;
		}

        static bool ValidateRequest(Request request)
        {
            if(String.IsNullOrEmpty(request.From) || String.IsNullOrEmpty(request.To) || request.Filter.MinPopulation < 0 || request.Filter.AllowedVehicles.Count == 0)
            {
                return false;
            }
            return true;
        }

        static void MergeDatabase(IGraphDatabase tomerge, IGraphDatabase cars_trains)
        {
            IGraphIterator i = cars_trains.CreateIterator();
            Route road = i.First();
            while (road != null)
            {
                tomerge.AddRoute(road.From, road.To, road.Cost, road.TravelTime, road.VehicleType);
                road = i.Next();
            }
        }

        static void PrintDatabase(IGraphDatabase graph)
        {
            Console.WriteLine("\n\n===============================\n");
            IGraphIterator iter = graph.CreateIterator();
            Route r = iter.First();
            while (r != null)
            {
                Console.WriteLine(r.From.Name + "-" + r.To.Name + "  time:" + r.TravelTime + "   v:" + r.VehicleType);
                r = iter.Next();
            }
            Console.WriteLine("===============================\n");
        }


        static void Main(string[] args)
		{
            //----------------------- TESTING PART ---------------------------

            //ISet<VehicleType> h = new HashSet<VehicleType>();
            //h.Add(VehicleType.Car); //h.Add(VehicleType.Train);
            //Request req = new Request { From = "Stormwind", To = "Wyzima", Solver = "BFS", Problem = "Time", Filter = new Filter { MinPopulation = 200000, RestaurantRequired = true, AllowedVehicles = h }, };
            //var res = ServeRequest(req);

            //-----------------------------------------------------------------

            Console.WriteLine("---- Xml Interface ----");

            XMLFactory xmlFactory = new XMLFactory();
            ISystem xmlSystem = CreateSystem(xmlFactory);
            Execute(xmlSystem, "xml_input.txt");
            Console.WriteLine();

            Console.WriteLine("---- KeyValue Interface ----");

            INIFactory keyFactory = new INIFactory();
            ISystem keyValueSystem = CreateSystem(keyFactory);
            Execute(keyValueSystem, "key_value_input.txt");
            Console.WriteLine();
        }

        /* Prepare method Create System here (add return type, arguments and body)*/
        static ISystem CreateSystem(IFactory factory) 
        {
            return factory.CreateSystem(factory.CreateForm(), factory.CreateDisplay());
        }

        static void Execute(ISystem system, string path)
        {
            IEnumerable<IEnumerable<string>> allInputs = ReadInputs(path);
            foreach (var inputs in allInputs)
            {
                foreach (string input in inputs)
                {
                    system.Form.Insert(input); //Insert Input -> command line to operate
                }
                var request = RequestMapper.Map(system.Form);
                var result = ServeRequest(request);
                system.Display.Print(result);
                Console.WriteLine("==============================================================");
                system.Form.Reset();  //clearing dictionary for next input
            }
        }

        private static IEnumerable<IEnumerable<string>> ReadInputs(string path)
        {
            using (StreamReader file = new StreamReader(path))
            {
                List<List<string>> allInputs = new List<List<string>>();
                while (!file.EndOfStream)
                {
                    string line = file.ReadLine();
                    List<string> inputs = new List<string>();
                    while (!string.IsNullOrEmpty(line))
                    {
                        inputs.Add(line);
                        line = file.ReadLine();
                    }
                    if (inputs.Count > 0)
                    {
                        allInputs.Add(inputs);
                    }
                }
                return allInputs;
            }
        }
    }
}

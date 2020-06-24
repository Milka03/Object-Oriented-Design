
// I certify that this assignment is my own work entirely, performed independently and without any help from the sources which are not allowed.
// Emilia Wróblewska

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BigTask2.Api;
using BigTask2.Data;

namespace BigTask2.Ui
{

    class XMLForm : IForm
    {
        public Dictionary<string, string> lines = new Dictionary<string, string>();
        public XMLForm() { }
        public void Insert(string command)
        {
            char[] charsToTrim = { '<', '>'};
            string tmp = command.Trim(charsToTrim);
            string[] first = tmp.Split('>');
            string[] second = first[1].Split('<');

            //if (lines.ContainsKey(first[0]))
            lines[first[0]] = second[0];
        }

        public bool GetBoolValue(string name)
        {
            if (!lines.ContainsKey(name))
            {
                Console.WriteLine("Error! Key should be in the dictionary!");
                return false;
            }
            string tmp = lines[name];
            if (tmp == "True") return true;
            if (tmp == "False") return false;
            return false;
        }
        public string GetTextValue(string name)
        {
            if (!lines.ContainsKey(name))
            {
                Console.WriteLine("Error! Key should be in the dictionary!");
                return "";
            }
            return lines[name];
        }
        public int GetNumericValue(string name)
        {
            int result = int.Parse(lines[name]);
            return result;
        }


        public void Reset()
        {
            lines.Clear();
        }
       
    }

    class INIForm : IForm
    {
        public Dictionary<string, string> lines = new Dictionary<string, string>();
        public INIForm() { }
        public void Insert(string command)
        {
            string[] tmp = command.Split('=');

            //if (lines.ContainsKey(first[0]))
            lines[tmp[0]] = tmp[1];
        }
        public bool GetBoolValue(string name)
        {
            if (!lines.ContainsKey(name))
            {
                Console.WriteLine("Error! Key should be in the dictionary!");
                return false;
            }
            string tmp = lines[name];
            if (tmp == "True") return true;
            if (tmp == "False") return false;
            return false;
        }
        public string GetTextValue(string name)
        {
            if (!lines.ContainsKey(name))
            {
                Console.WriteLine("Error! Key should be in the dictionary!");
                return "";
            }
            return lines[name];
        }
        public int GetNumericValue(string name)
        {
            int result = int.Parse(lines[name]);
            return result;
        }

        public void Reset()
        {
            lines.Clear();
        }
    
    }

    //-----------------------------------------------------
    class XMLDisplay : IDisplay
    {
        public void Print(IEnumerable<Route> routes)
        {
            if(routes == null || routes.Count() == 0)
            {
                Console.WriteLine("<>");
                return;
            }
            double totalTime = 0;
            double totalCost = 0;

            Console.WriteLine("<City/>");
            Console.WriteLine("<Name>" + routes.First().From.Name + "</Name>");
            Console.WriteLine("<Population>" + routes.First().From.Population + "</Population>");
            Console.WriteLine("<HasRestaurant>" + routes.First().From.HasRestaurant + "</HasRestaurant>");
            Console.WriteLine();

            foreach(Route r in routes)
            {
                Console.WriteLine("<Route/>");
                Console.WriteLine("<Vehicle>" + r.VehicleType + "</Vehicle>");
                Console.WriteLine("<Cost>" + r.Cost + "</Cost>");
                Console.WriteLine("<TravelTime>" + r.TravelTime + "</TravelTime>");
                Console.WriteLine();
                Console.WriteLine("<City/>");
                Console.WriteLine("<Name>" + r.To.Name + "</Name>");
                Console.WriteLine("<Population>" + r.To.Population + "</Population>");
                Console.WriteLine("<HasRestaurant>" + r.To.HasRestaurant + "</HasRestaurant>");
                Console.WriteLine();
                totalCost += r.Cost;
                totalTime += r.TravelTime;
            }
            Console.WriteLine("<totalTime>" + Math.Round(totalTime,2) + "</totalTime>");
            Console.WriteLine("<totalCost>" + Math.Round(totalCost,2) + "</totalCost>");
            Console.WriteLine();
        }
    }

    class INIDisplay : IDisplay
    {
        public void Print(IEnumerable<Route> routes)
        {
            if (routes == null || routes.Count() == 0)
            {
                Console.WriteLine("=");
                return;
            }
            double totalTime = 0;
            double totalCost = 0;

            Console.WriteLine("=City=");
            Console.WriteLine("Name=" + routes.First().From.Name);
            Console.WriteLine("Population=" + routes.First().From.Population);
            Console.WriteLine("HasRestaurant=" + routes.First().From.HasRestaurant);
            Console.WriteLine();

            foreach (Route r in routes)
            {
                Console.WriteLine("=Route=");
                Console.WriteLine("Vehicle=" + r.VehicleType);
                Console.WriteLine("Cost=" + r.Cost);
                Console.WriteLine("TravelTime=" + r.TravelTime);
                Console.WriteLine();
                Console.WriteLine("=City=");
                Console.WriteLine("Name=" + r.To.Name);
                Console.WriteLine("Population=" + r.To.Population);
                Console.WriteLine("HasRestaurant=" + r.To.HasRestaurant);
                Console.WriteLine();
                totalCost += r.Cost;
                totalTime += r.TravelTime;
            }
            Console.WriteLine("totalTime=" + Math.Round(totalTime,2));
            Console.WriteLine("totalCost=" + Math.Round(totalCost,2));
            Console.WriteLine();
        }

    }



    //-----------------------------------------------------

    class XMLSystem : ISystem
    {
        private XMLForm form;
        private XMLDisplay display;

        public XMLSystem(XMLForm f, XMLDisplay disp)
        {
            form = f;
            display = disp;
        }
        public IForm Form { get { return form; } }
        public IDisplay Display { get { return display; } }

    }

    class INISystem : ISystem
    {
        private INIForm form;
        private INIDisplay display;

        public INISystem(INIForm f, INIDisplay disp)
        {
            form = f;
            display = disp;
        }
        public IForm Form { get { return form; } }
        public IDisplay Display { get { return display; } }

    }

    //-----------------------------------------------------

    class XMLFactory : IFactory
    {
        public IForm CreateForm() { return new XMLForm(); }

        public IDisplay CreateDisplay() { return new XMLDisplay(); }

        public ISystem CreateSystem(IForm f, IDisplay d) { return new XMLSystem((XMLForm)f, (XMLDisplay)d); }
    }

    class INIFactory : IFactory
    {
        public IForm CreateForm() { return new INIForm(); }

        public IDisplay CreateDisplay() { return new INIDisplay(); }

        public ISystem CreateSystem(IForm f, IDisplay d) { return new INISystem((INIForm)f, (INIDisplay)d); }

    }

}

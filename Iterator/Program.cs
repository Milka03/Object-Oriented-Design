using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFrames
{
    class Program
    {
        static void PrintDataFrame(IEnumerable<Record> data)
        {
            foreach(var p in data)
            {
                Console.WriteLine(p.Name + " " + p.Surname + " " + p.Gender + " " + p.Age);
            }
            Console.WriteLine();
        }

        static Tuple<string, double> Mode(IEnumerable<Record> data)
        {
            double Mcount = 0;
            double Fcount = 0;
            double ratio = 0;
            string result;
            foreach(Record d in data)
            {
                if (d.Gender == "M") Mcount++;
                if (d.Gender == "F") Fcount++;
            }
            if (Mcount > Fcount) { result = "M"; ratio = (Mcount / (Mcount + Fcount))*100; }
            if (Mcount == Fcount) { result = "M equal F"; ratio = 50; }
            else { result = "F"; ratio = (Fcount / (Mcount + Fcount))*100; }
            return Tuple.Create(result, ratio);
        }

        

        static List<Record> ConcatenationWithCondition(IEnumerable<Record> data1, IEnumerable<Record> data2, Func<Record, bool> func)
        {
            List<Record> result = new List<Record>();
            foreach(Record r in data1)
            {
                if (func(r)) result.Add(r);
            }
            foreach (Record r in data2)
            {
                if (func(r)) result.Add(r);
            }
            return result;
        }

        static void Main(string[] args)
        {
            List < Record > people = new List<Record>()
            {
                new Record("Keanu", "Reeves", "M", 25),
                new Record("Agent", "Smith", "M", 35),
                new Record("Carrie-Anne", "Moss", "F", 25),
                new Record("Gloria", "Foster", "F", 123),
                new Record("Belinda", "McClory", "F", 27),
                new Record("Melissa", "Jones", "F", 44),
                new Record("Jack", "Sparrow", "M", 58),
                new Record("Judy", "Stern", "F", 15),
                new Record("Pink", "Panther", "M", 70)
            };
            ListDataFrame ldf = new ListDataFrame(people);
            PrintDataFrame(ldf);
            Tuple<string, double> res = Mode(ldf);
            Console.WriteLine($"mode = {res.Item1}, percent = {res.Item2}\n");

            FileDataFrame fdf = new FileDataFrame("dataFrame1.txt");
            PrintDataFrame(fdf);
            Tuple<string, double> res2 = Mode(fdf);
            Console.WriteLine($"mode = {res2.Item1}, percent = {res2.Item2}\n");

            Console.WriteLine("\nConcatenation\n");
            Func<Record, bool> f = r => r.Gender == "F";
            PrintDataFrame(new ListDataFrame(ConcatenationWithCondition(ldf, fdf, f)));
            FileDataFrame fdf2 = new FileDataFrame(ConcatenationWithCondition(ldf, fdf, f), "dataFrame2.txt");
        }
    }
}

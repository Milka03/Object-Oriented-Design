using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFrames
{
    public class Record
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }

        public Record()
        { }
        public Record(string name, string surname, string gender, int age)
        {
            Name = name;
            Surname = surname;
            Gender = gender;
            Age = age;
        }

    }
    
    public class ListDataFrame : IEnumerable<Record>
    {
        private List<Record> list;

        public List<Record> List
        {
            get { return list; }
            set { list = value; }
        }

        public ListDataFrame(List<Record> list)
        {
            this.list = new List<Record>(list);
        }

        public IEnumerator<Record> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }


    }

    public class FileDataFrame : IEnumerable<Record>
    {
        private string filename;
        List<Record> list = new List<Record>();

        public string Filename
        {
            get { return filename; }
            set { filename = value; }
        }

        public FileDataFrame(string filename)
        {
            this.filename = filename;
            string line;
            StreamReader reader = new StreamReader(filename);
            while((line = reader.ReadLine()) != null)
            {
                string[] parts = line.Split(',');
                list.Add(new Record(parts[0].Trim(), parts[1].Trim(), parts[2].Trim(), int.Parse(parts[3].Trim())));
            }
            reader.Close();
        }

        public FileDataFrame(List<Record> mylist, string file)
        {
            this.filename = file;
            StreamWriter writer = new StreamWriter(filename);
            foreach(Record r in mylist)
            {
                writer.WriteLine($"{r.Name}, {r.Surname}, {r.Gender}, {r.Age}");
            }
            writer.Close();
        }

        public IEnumerator<Record> GetEnumerator()
        {
            return new FileDataFrameEnumerator(filename);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return  this.GetEnumerator();
        }

    }


    public class FileDataFrameEnumerator : IEnumerator<Record>
    {
        string file;
        string line;
        Record current;
        StreamReader reader;
        private int position = -1;

        public FileDataFrameEnumerator(string filename)
        {
            this.file = filename;
            reader = new StreamReader(file);
        }

        public Record Current
        {
            get
            {
                try
                {
                    string[] info = line.Split(',');
                    return new Record(info[0].Trim(), info[1].Trim(), info[2].Trim(), int.Parse(info[3].Trim()));
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
            
        }

        public bool MoveNext()
        {
            position++;
            if ((line = reader.ReadLine()) == null) return false;
            return true;
        }

        public void Reset()
        {
            position = -1;
            reader = new StreamReader(file);
        }

        public void Dispose()
        {
            reader.Close();   
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }

     

    }
}

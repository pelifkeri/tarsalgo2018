using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Tarsalgo_2018
{
    class Program
    {
        static List<Entry> Entries = new List<Entry>();

        static void Main(string[] args)
        {
            ReadFilesFromFile();

            Task1();
            Task2();
        }

        private static void Task1()
        {
            Console.WriteLine($"Az első belépő: {Entries.First(x => x.IsIncoming).Id}");
            Console.WriteLine($"Az utolsó kilépő: {Entries.Last(x => !x.IsIncoming).Id}");
        }

        private static void Task2()
        {

        }

        private static void ReadFilesFromFile()
        {
            using StreamReader o = new StreamReader("ajto.txt");
            while (!o.EndOfStream)
            {
                Entries.Add(new Entry(o.ReadLine()));
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Tarsalgo_2018
{
    class Program
    {
        static List<Entry> Entries = new List<Entry>();
        static List<IGrouping<int, Entry>> EntriesGroupedById = new List<IGrouping<int, Entry>>();
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;

            Task1();
            Task2();
            Task3();
            Task4();
            Task5();

            Console.ReadLine();
        }

        private static void Task1()
        {
            using StreamReader o = new StreamReader("ajto.txt");
            while (!o.EndOfStream)
            {
                Entries.Add(new Entry(o.ReadLine()));
            }
        }

        private static void Task2()
        {
            Console.WriteLine($"Az első belépő: {Entries.First(x => x.IsIncoming).Id}");
            Console.WriteLine($"Az utolsó kilépő: {Entries.Last(x => !x.IsIncoming).Id}");
        }

        private static void Task3()
        {
            EntriesGroupedById = Entries
               .GroupBy(x => x.Id)
               .OrderBy(x => x.Key)
               .ToList();

            using StreamWriter writer = new StreamWriter("athaladas.txt");
            EntriesGroupedById.ForEach(entry =>
            {
                writer.WriteLine($"{entry.Key} {entry.Count()}");
            });
        }

        private static void Task4()
        {
            EntriesGroupedById.ForEach(person =>
            {
                if (person.Last().IsIncoming)
                {
                    Console.WriteLine($"Az {person.Key} azonosítójú személy bent tartózkodott a vizsgált idő végén.");
                }
            });
        }

        private static void Task5()
        {
            int currentCount = 0, maxCount = 0, indexOfMaxReached = 0;

            for (int i = 0; i < Entries.Count; i++)
            {
                currentCount = Entries[i].IsIncoming ? currentCount + 1 : currentCount - 1;

                if (currentCount > maxCount)
                {
                    maxCount = currentCount;
                    indexOfMaxReached = i;
                }
            }

            Console.WriteLine($"{Entries[indexOfMaxReached].Hours}:{Entries[indexOfMaxReached].Minutes}-kor voltak a legtöbben a társalgóban.");
        }
    }
}

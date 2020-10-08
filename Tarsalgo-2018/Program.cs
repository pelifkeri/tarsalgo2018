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
        static int SelectedPersonId;
        static List<Entry> SelectedPersonsEntries;

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;

            Task1();
            Task2();
            Task3();
            Task4();
            Task5();
            Task6();
            Task7();
            Task8();

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
            Console.WriteLine("2. feladat");
            Console.WriteLine($"Az első belépő: {Entries.First(x => x.IsIncoming).Id}");
            Console.WriteLine($"Az utolsó kilépő: {Entries.Last(x => !x.IsIncoming).Id}");
            Console.WriteLine();
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
            Console.WriteLine("4. feladat");
            var lastRemaining = EntriesGroupedById
                .Where(x => x.Last().IsIncoming)
                .ToList();

            string ids = string.Empty;
            lastRemaining.ForEach(person =>
            {
                ids += $"{person.Key} ";
            });
            Console.WriteLine($"A végén a társalgóban voltak: {ids}");
            Console.WriteLine();
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

            Console.WriteLine("5. feladat");
            Console.WriteLine($"{Entries[indexOfMaxReached].Hours}:{Entries[indexOfMaxReached].Minutes}-kor voltak a legtöbben a társalgóban. \n");
        }

        private static void Task6()
        {
            Console.WriteLine("6. feladat");
            Console.WriteLine("Adja meg egy személy azonosítóját!");
            SelectedPersonId = Convert.ToInt32(Console.ReadLine());
            SelectedPersonsEntries = EntriesGroupedById
                .Find(x => x.Key == SelectedPersonId)
                .ToList();

            Console.WriteLine();
        }

        private static void Task7()
        {
            Console.WriteLine("7. feladat");
            for (int i = 0; i < SelectedPersonsEntries.Count(); i++)
            {
                string timeRange = $"{SelectedPersonsEntries[i].Hours}:{SelectedPersonsEntries[i].Minutes}";
                if (i % 2 == 0)
                {
                    Console.Write($"{timeRange}-");
                }
                else
                {
                    Console.Write($"{timeRange}\n");
                }
            }

            Console.WriteLine();
        }

        private static void Task8()
        {
            Console.WriteLine();
            Console.WriteLine("8. feladat");

            int spentMinutes = 0;
            SelectedPersonsEntries.ForEach(entry =>
            {
                spentMinutes = entry.IsIncoming ? spentMinutes - entry.GetPassedMinutesSinceMidnight : spentMinutes + entry.GetPassedMinutesSinceMidnight;
            });

            bool stayedInside = SelectedPersonsEntries.Last().IsIncoming;

            string isInside = stayedInside ? "a társalgóban volt." : "nem volt a társalgóban";
            spentMinutes = stayedInside ? spentMinutes + 15 * 60 : spentMinutes;

            Console.WriteLine($"A(z) {SelectedPersonId}. személy összesen {spentMinutes} percet volt bent, a megfigyelés végén {isInside}");
        }
    }
}

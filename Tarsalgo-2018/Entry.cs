using System;

namespace Tarsalgo_2018
{
    public class Entry
    {
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int Id { get; set; }
        public bool IsIncoming { get; set; }

        public Entry(string line)
        {
            var values = line.Split(' ');

            Hours = Convert.ToInt32(values[0]);
            Minutes = Convert.ToInt32(values[1]);
            Id = Convert.ToInt32(values[2]);
            IsIncoming = values[3] == "be";
        }

        public int GetPassedMinutesSinceMidnight => Hours * 60 + Minutes;
    }
}

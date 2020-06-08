using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Helpers
{
    public class TimeHelpers
    {
        public string HoursAndMinutesString(int minutes)
        {
            string time;
            int m;
            int h = Math.DivRem(minutes, 60, out m);
            if (h == 0)
            {
                time = $"{m} min";
            }
            else
            {
                time = $"{h} h {m} min";
            }
            return time;
        }
       
    }
    public static class Extensions
    {
        public static void Add(this List<Tuple<DateTime, DateTime>> list, DateTime x, DateTime y)
        {
            list.Add(new Tuple<DateTime, DateTime>(x, y));
        }
    }
}

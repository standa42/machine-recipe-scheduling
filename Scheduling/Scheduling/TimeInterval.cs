using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling
{
    internal class TimeInterval
    {
        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public TimeSpan GetDuration()
        {
            return End - Start;
        }

        public TimeInterval(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        public TimeInterval(DateTime start, TimeSpan duration)
        {
            Start = start;
            End = start + duration;
        }

        public static bool Intersection(TimeInterval timeInterval1, TimeInterval timeInterval2)
        {
            // TODO: change to better comparison of datetimes
            // This is pretty basic quick solution based on https://stackoverflow.com/questions/1985317/equivalent-of-math-min-math-max-for-dates
            var minimumOfEndDates = new[] { timeInterval1.End, timeInterval2.End }.Min();
            var maximumOfStartDates = new[] { timeInterval1.Start, timeInterval2.Start }.Max();
            return minimumOfEndDates > maximumOfStartDates;
        }
    }
}

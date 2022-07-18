using System;
using System.Linq;

namespace Scheduling
{
    public class DateTimeInterval
    {
        public DateTime Start { get; }

        public DateTime End { get; }

        public TimeSpan GetDuration()
        {
            return End - Start;
        }

        public DateTimeInterval(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        public DateTimeInterval(DateTime start, TimeSpan duration)
        {
            Start = start;
            End = start + duration;
        }

        /// <summary>
        /// Checks intersection between datetime intervals. Intervals are considered to be open, so if one ends in the same moment the other one start, it is not considered as intersection.
        /// </summary>
        /// <returns>True if intervals have intersection, false otherwise</returns>
        public static bool Intersection(DateTimeInterval timeInterval1, DateTimeInterval timeInterval2)
        {
            // TODO: change to better comparison of datetimes
            // This is pretty basic quick solution based on https://stackoverflow.com/questions/1985317/equivalent-of-math-min-math-max-for-dates
            var minimumOfEndDates = new[] { timeInterval1.End, timeInterval2.End }.Min();
            var maximumOfStartDates = new[] { timeInterval1.Start, timeInterval2.Start }.Max();
            return minimumOfEndDates > maximumOfStartDates;
        }
    }
}

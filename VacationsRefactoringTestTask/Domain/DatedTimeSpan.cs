using System;
using System.Collections.Generic;

namespace VacationsRefactoringTestTask
{
    public readonly struct DatedTimeSpan
    {
        public DatedTimeSpan(DateTime start, DateTime end) : this()
        {
            Start = start;
            End = end;
            TimeSpan = end - start;
        }

        public DateTime Start { get; }
        public DateTime End { get; }
        public TimeSpan TimeSpan { get; }

        public bool Contains(DateTime date)
            => Start <= date && date <= End;

        public bool Intersects(DatedTimeSpan interval, bool intersectOnBorder)
        {
            var min1 = GetMin(Start, End);
            var min2 = GetMin(interval.Start, interval.End);
            var max1 = GetMax(Start, End);
            var max2 = GetMax(interval.Start, interval.End);

            if (intersectOnBorder)
                return !(max1 < min2 || max2 < min1);
            else 
                return !(max1 <= min2 || max2 <= min1);
        }

        public IEnumerable<DateTime> EnumerateDays()
        {
            var increment = Start <= End ? 1 : -1;
            for (var day = Start; day < End; day = day.AddDays(increment))
            {
                yield return day;
            }
        }

        public override string ToString()
        {
            return $"[{Start}; {End}]";
        }

        private static DateTime GetMin(DateTime date1, DateTime date2)
        {
            if (date2 < date1)
                return date2;
            else return date1;
        }

        private static DateTime GetMax(DateTime date1, DateTime date2)
        {
            if (date2 > date1)
                return date2;
            else return date1;
        }
    }
}

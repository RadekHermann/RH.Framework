using System;
using System.Collections.Generic;
using System.Linq;

namespace RH.App.Common.Extensions
{
    public static class DateTimeExt
    {
        public static (DateTime Date, string Name)[] GetHolidaysInYear(int year)
        {
            var result = new List<(DateTime Date, string Name)> {
                (Date: new DateTime(year, 1, 1), Name: "Den obnovy samostatného českého státu"),
                (Date: GetEasterSundayInYear(year).AddDays(1), Name: "Velikonoční pondělí"),
                (Date: new DateTime(year, 5, 1), Name: "Svátek práce"),
                (Date: new DateTime(year, 5, 8), Name: "Den vítězství"),
                (Date: new DateTime(year, 7, 5), Name: "Den věrozvěstů Cyrila a Metoděje"),
                (Date: new DateTime(year, 7, 6), Name: "Den upálení mistra Jana Husa"),
                (Date: new DateTime(year, 9, 28), Name: "Den české státnosti"),
                (Date: new DateTime(year, 10, 28), Name: "Den vzniku Československa"),
                (Date: new DateTime(year, 11, 17), Name: "Den boje za svobodu a demokracii"),
                (Date: new DateTime(year, 12, 24), Name: "Štědrý den"),
                (Date: new DateTime(year, 12, 25), Name: "1. svátek vánoční"),
                (Date: new DateTime(year, 12, 26), Name: "2. svátek vánoční")
            };

            if (year >= 2016)
            {
                result.Add((Date: GetEasterSundayInYear(year).AddDays(-2), Name: "Velký pátek"));
            }

            return result.OrderBy(o => o.Date).ToArray();
        }

        public static bool IsHoliday(this DateTime date)
        {
            return GetHolidaysInYear(date.Year).Any(a => a.Date == date.Date);
        }

        public static bool IsWeekend(this DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Sunday || date.DayOfWeek == DayOfWeek.Saturday;
        }

        public static DateTime Now()
        {
            var date = DateTime.Now;
            return new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second);
        }

        public static DateTimeOffset OffsetNow()
        {
            return new DateTimeOffset(Now());
        }

        private static DateTime GetEasterSundayInYear(int year)
        {
            const int m = 24;
            const int n = 5;

            var a = year % 19;
            var b = year % 4;
            var c = year % 7;

            var d = (19 * a + m) % 30;
            var e = (n + 2 * b + 4 * c + 6 * d) % 7;

            var u = d + e - 9;
            int v;

            if (u == 25 && d == 28 && e == 6 && a > 10)
            {
                u = 18;
                v = 4;
            }
            else if (u >= 1 && u <= 25)
            {
                v = 4;
            }
            else if (u > 25)
            {
                u -= 7;
                v = 4;
            }
            else
            {
                u = 22 + d + e;
                v = 3;
            }

            return new DateTime(year, v, u);
        }
    }
}

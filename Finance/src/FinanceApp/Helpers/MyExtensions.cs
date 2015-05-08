using System;
using System.Globalization;

namespace App.Helpers
{
    public static class MyExtensions
    {
        public static string ToBrString(this DateTime date)
        {
            return date.ToString("dd/MM/yyyy");
        }

        public static string ToUsString(this DateTime date)
        {
            return date.ToString("yyyy-MM-dd");
        }

        public static bool HaveTheSameMonthAndYear(this DateTime date, DateTime anotherDate)
        {
            return date.Month == anotherDate.Month && date.Year == anotherDate.Year;
        }

        public static DateTime ToDate(this string str)
        {
            string[] dateparts;
            if (str.Contains("-"))
            {
                dateparts = str.Split('-');
                return new DateTime(int.Parse(dateparts[0]), int.Parse(dateparts[1]), int.Parse(dateparts[2]));
            }

            dateparts = str.Split('/');
            return new DateTime(int.Parse(dateparts[2]), int.Parse(dateparts[1]),
                int.Parse(dateparts[0]));
        }

        public static DateTime GetFirstFromSixMonthAgo(this DateTime? date)
        {
            var sixMonthsAgo = DateTime.Today.AddMonths(-6);
            return date ?? new DateTime(sixMonthsAgo.Year, sixMonthsAgo.Month, 1);
        }

        public static DateTime GetTodayIfNull(this DateTime? date)
        {
            return date ?? DateTime.Today;
        }

        public static string ToCurrency(this decimal value)
        {
            return value == 0 ? "" : (value).ToString(CultureInfo.InvariantCulture);
        }
    }
}

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
                return new DateTime(Convert.ToInt32(dateparts[0]), Convert.ToInt32(dateparts[1]), Convert.ToInt32(dateparts[2]));
            }

            dateparts = str.Split('/');
            return new DateTime(Convert.ToInt32(dateparts[2]), Convert.ToInt32(dateparts[1]),
                Convert.ToInt32(dateparts[0]));
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
            return value == 0 ? "" : Math.Round(value, 2).ToString(CultureInfo.InvariantCulture);
        }
    }
}

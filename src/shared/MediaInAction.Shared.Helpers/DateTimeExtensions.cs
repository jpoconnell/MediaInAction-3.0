using System;
using System.Threading;

namespace MediaInAction.Helpers
{
    public static class DateTimeExtensions
    {
        public static DateTime FirstDateInWeek(this DateTime dt)
        {
            while (dt.DayOfWeek != Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek)
            {
                dt = dt.AddDays(-1);
            }

            return dt;
        }
    }
}

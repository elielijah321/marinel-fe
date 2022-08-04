using System;

namespace SchoolDraftWebsite.Helpers
{
    public static class DateTimeHelper
    {
        public static string ToYearMonthDay(DateTime date)
        {
            return date.ToString("yyyy-MM-dd");
        }

        public static string ToDayMonthYear(DateTime date)
        {
            return date.ToString("dd/MM/yyyy");
        }
    }
}

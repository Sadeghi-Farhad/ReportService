using System.Globalization;

namespace ReportService.Domain.Common
{
    public static class DateHelper
    {
        public static string ConvertToShamsiDateTime(DateTime? input)
        {
            string Shdate = "";
            if (input != null)
            {
                Shdate = ConvertToShamsiDate(input);
                Shdate += " " + input.Value.ToString("HH:mm:ss");
            }

            return Shdate;
        }

        public static string ConvertToShamsiDate(DateTime? input)
        {
            PersianCalendar pc = new PersianCalendar();
            string Shdate = "";
            if (input == null)
                return "";
            DateTime date = input.Value;

            string y = pc.GetYear(date).ToString();
            string m = pc.GetMonth(date) < 10 ? "0" + pc.GetMonth(date).ToString() : pc.GetMonth(date).ToString();
            string d = pc.GetDayOfMonth(date) < 10 ? "0" + pc.GetDayOfMonth(date).ToString() : pc.GetDayOfMonth(date).ToString();
            Shdate = y + "/" + m + "/" + d;
            return Shdate;
        }
    }
}

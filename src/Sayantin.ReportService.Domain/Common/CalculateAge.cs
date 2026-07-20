namespace ReportService.Domain.Common
{
    /// <summary>
    /// Contains general functions.
    /// </summary>
    public partial class Functions
    {
        public static int CalculateAge(DateTime birthday)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - birthday.Year;

            // Adjust age if the birthday hasn't occurred yet this year
            if (today < birthday.AddYears(age))
            {
                age--;
            }

            return age;
        }

        public static int CalculateAge(DateOnly birthday)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - birthday.Year;

            // Adjust age if the birthday hasn't occurred yet this year
            if (today < new DateTime(birthday.AddYears(age), default))
            {
                age--;
            }

            return age;
        }
    }
}
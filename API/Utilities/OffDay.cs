namespace API.Utilities
{
    // Check IsOffDay
    public class OffDay
    {
        // List of holidays
        private List<DateTime> holidays = new List<DateTime>
        {
            new DateTime(DateTime.Now.Year, 1, 1),  // New Year
            new DateTime(DateTime.Now.Year, 12, 25), // Christmas
        };

        public bool IsOffDay(DateTime date)
        {
            if (holidays.Contains(date.Date) || date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

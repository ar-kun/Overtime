using System.ComponentModel.DataAnnotations;

namespace API.Utilities.Enums
{
    public enum TypeDayLevel
    {
        [Display(Name = "Week Day")] WeekDay,
        [Display(Name = "Off Day")] OffDay
    }
}
using API.Models;
using API.Utilities;
using API.Utilities.Enums;

namespace API.DTOs.Overtimes
{
    public class CreateOvertimeDto
    {
        public Guid EmployeeGuid { get; set; }
        public DateTime DateRequest { get; set; }
        public int Duration { get; set; }
        public string Remarks { get; set; }

        // Implicit conversion operator from OvertimeDto to Overtime Model
        public static implicit operator Overtime(CreateOvertimeDto createOvertimeDto)
        {
            // Check WeekDay or OffDay
            var isOffDay = new OffDay();
            TypeDayLevel typeOfDay = isOffDay.IsOffDay(createOvertimeDto.DateRequest)
                ? TypeDayLevel.OffDay
                : TypeDayLevel.WeekDay;

            return new Overtime
            {
                EmployeeGuid = createOvertimeDto.EmployeeGuid,
                DateRequest = createOvertimeDto.DateRequest,
                Duration = createOvertimeDto.Duration,
                Status = 0,
                Remarks = createOvertimeDto.Remarks,
                TypeOfDay = typeOfDay
            };
        }
    }
}
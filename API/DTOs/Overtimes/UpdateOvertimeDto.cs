using API.Models;
using API.Utilities.Enums;

namespace API.DTOs.Overtimes
{
    public class UpdateOvertimeDto
    {
        public Guid Guid { get; set; }
        public Guid EmployeeGuid { get; set; }
        public DateTime DateRequest { get; set; }
        public int Duration { get; set; }
        public StatusLevel Status { get; set; }
        public string Remarks { get; set; }
        public TypeDayLevel TypeOfDay { get; set; }

        // Implicit conversion operator from OvertimeDto to Overtime Model
        public static implicit operator Overtime(UpdateOvertimeDto updateOvertimeDto)
        {
            return new Overtime
            {
                Guid = updateOvertimeDto.Guid,
                EmployeeGuid = updateOvertimeDto.EmployeeGuid,
                DateRequest = updateOvertimeDto.DateRequest,
                Duration = updateOvertimeDto.Duration,
                Status = updateOvertimeDto.Status,
                Remarks = updateOvertimeDto.Remarks,
                TypeOfDay = updateOvertimeDto.TypeOfDay
            };
        }
    }
}
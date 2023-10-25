using API.Models;
using API.Utilities.Enums;

namespace API.DTOs.Overtimes
{
    public class StoreOvertimeDto
    {
        public Guid Guid { get; set; }
        public Guid EmployeeGuid { get; set; }
        public Guid PaymentDetailGuid { get; set; }
        public DateTime DateRequest { get; set; }
        public int Duration { get; set; }
        public StatusLevel Status { get; set; }
        public string Remarks { get; set; }
        public TypeDayLevel TypeOfDay { get; set; }

        // Implicit conversion operator from OvertimeDto to Overtime Model
        public static implicit operator Overtime(StoreOvertimeDto storeOvertimeDto)
        {
            return new Overtime
            {
                Guid = storeOvertimeDto.Guid,
                EmployeeGuid = storeOvertimeDto.EmployeeGuid,
                PaymentDetailsGuid = storeOvertimeDto.PaymentDetailGuid,
                DateRequest = storeOvertimeDto.DateRequest,
                Duration = storeOvertimeDto.Duration,
                Status = storeOvertimeDto.Status,
                Remarks = storeOvertimeDto.Remarks,
                TypeOfDay = storeOvertimeDto.TypeOfDay
            };
        }
    }
}
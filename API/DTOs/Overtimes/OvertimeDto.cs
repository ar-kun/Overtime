using API.Models;

namespace API.DTOs.Overtimes
{
    // DTO for GetByGuid,GetAll, response display, etc
    public class OvertimeDto
    {
        public Guid Guid { get; set; }
        public Guid EmployeeGuid { get; set; }
        public Guid PaymentDetailGuid { get; set; }
        public DateTime DateRequest { get; set; }
        public int Duration { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public string TypeOfDay { get; set; }

        // Explicit conversion operator from Overtime Model to OvertimeDto
        public static explicit operator OvertimeDto(Overtime overtime)
        {
            return new OvertimeDto
            {
                Guid = overtime.Guid,
                EmployeeGuid = overtime.EmployeeGuid,
                PaymentDetailGuid = overtime.PaymentDetailsGuid,
                DateRequest = overtime.DateRequest,
                Duration = overtime.Duration,
                Status = overtime.Status.ToString(),
                Remarks = overtime.Remarks,
                TypeOfDay = overtime.TypeOfDay.ToString()
            };
        }
    }
}
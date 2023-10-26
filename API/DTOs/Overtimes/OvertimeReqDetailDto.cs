using API.Models;

namespace API.DTOs.Overtimes
{
    // DTO for GetByGuid,GetAll, response display, etc
    public class OvertimeReqDetailDto
    {
        public Guid Guid { get; set; }
        public Guid EmployeeGuid { get; set; }
        public string EmployeeFullName { get; set; }
        public Guid? ManagerGuid { get; set; }
        public string ManagerFullName { get; set; }
        public DateTime DateRequest { get; set; }
        public int Duration { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public string TypeOfDay { get; set; }

        // Explicit conversion operator from Overtime Model to OvertimeDto
        public static explicit operator OvertimeReqDetailDto(Overtime overtime)
        {
            return new OvertimeReqDetailDto
            {
                Guid = overtime.Guid,
                EmployeeGuid = overtime.EmployeeGuid,
                DateRequest = overtime.DateRequest,
                Duration = overtime.Duration,
                Status = overtime.Status.ToString(),
                Remarks = overtime.Remarks,
                TypeOfDay = overtime.TypeOfDay.ToString()
            };
        }
    }
}
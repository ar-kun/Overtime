using API.Utilities.Enums;

namespace API.DTOs.Employees
{
    public class BaseEmployeeDto
    {
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime HiringDate { get; set; }
        public GenderLevel Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int Salary { get; set; }
        public Guid? ManagerGuid { get; set; }
    }
}
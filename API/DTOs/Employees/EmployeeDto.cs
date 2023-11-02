using API.Models;
using System.Globalization;

namespace API.DTOs.Employees
{
    // DTO for GetByGuid, GetAll, response display, etc
    public class EmployeeDto
    {
        //public string FirstName { get; set; }
        //public string? LastName { get; set; }
        //public DateTime BirthDate { get; set; }
        //public DateTime HiringDate { get; set; }
        //public string Email { get; set; }
        //public string PhoneNumber { get; set; }
        //public Guid Guid { get; set; }
        //public string Nik { get; set; }
        //public string Gender { get; set; }
        //public int Salary { get; set; }
        //public Guid? ManagerGuid { get; set; }

        public string FullName { get; set; }
        public string BirthDate { get; set; }
        public string HiringDate { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Guid Guid { get; set; }
        public string Nik { get; set; }
        public string Gender { get; set; }
        public string Salary { get; set; }
        public Guid? ManagerGuid { get; set; }

        // Operator konversi eksplisit dari Employee ke EmployeeDto
        public static explicit operator EmployeeDto(Employee employee)
        {
            return new EmployeeDto
            {
                //Guid = employee.Guid,
                //Nik = employee.Nik,
                //FirstName = employee.FirstName,
                //LastName = employee.LastName,
                //BirthDate = employee.BirthDate,
                //Gender = employee.Gender.ToString(),
                //HiringDate = employee.HiringDate,
                //Email = employee.Email,
                //PhoneNumber = employee.PhoneNumber,
                //Salary = employee.Salary,
                //ManagerGuid = employee.ManagerGuid

                Guid = employee.Guid,
                Nik = employee.Nik,
                FullName = string.Concat(employee.FirstName, " ", employee.LastName),
                BirthDate = employee.BirthDate.ToString("dd MMM yyyy"),
                Gender = employee.Gender.ToString(),
                HiringDate = employee.HiringDate.ToString("dd MMM yyyy"),
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                Salary = employee.Salary.ToString("C", new CultureInfo("id-ID")),
                ManagerGuid = employee.ManagerGuid
            };
        }
    }
}
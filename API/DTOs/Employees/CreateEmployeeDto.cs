using API.Models;

namespace API.DTOs.Employees
{
    // DTO for Creating Employee request
    public class CreateEmployeeDto : BaseEmployeeDto
    {
        // Implicit conversion operator from CreateEmployeeDto to Employee model
        public static implicit operator Employee(CreateEmployeeDto createEmployeeDto)
        {
            return new Employee
            {
                FirstName = createEmployeeDto.FirstName,
                LastName = createEmployeeDto.LastName,
                BirthDate = createEmployeeDto.BirthDate,
                Gender = createEmployeeDto.Gender,
                HiringDate = createEmployeeDto.HiringDate,
                Email = createEmployeeDto.Email,
                PhoneNumber = createEmployeeDto.PhoneNumber,
                Salary = createEmployeeDto.Salary,
                ManagerGuid = createEmployeeDto.ManagerGuid,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }
    }
}
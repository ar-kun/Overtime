﻿using API.Models;

namespace API.Contracts
{
    // Defines an interface IEmployeeRepository that inherits from the IGeneralRepository<Employee> interface
    public interface IEmployeeRepository : IGeneralRepository<Employee>
    {
        string? GetLastNik();

        Employee? GetByEmail(string email);
        string? GetEmail(Guid? employeeGuid);

        Guid GetManagerGuid(string nik);
        Employee? GetByGuid(Guid? managerGuid);
        IEnumerable<Employee> GetByManagerGuid(Guid guid);
    }
}
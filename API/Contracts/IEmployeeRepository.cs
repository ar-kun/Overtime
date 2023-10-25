using API.Models;

namespace API.Contracts
{
    // Defines an interface IEmployeeRepository that inherits from the IGeneralRepository<Employee> interface
    public interface IEmployeeRepository : IGeneralRepository<Employee>
    {
        Employee GetLastNik();
    }
}

using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    // Defines EmployeeRepository that inherits from GeneralRepository<Employee>
    // Implements the IEmployeeRepository interface
    public class EmployeeRepository : GeneralRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(OvertimeDbContext context) : base(context) { }

        Employee IEmployeeRepository.GetLastNik()
        {
            var lastNik = _context.Employees.OrderByDescending(e => e.Nik).FirstOrDefault();
            return lastNik;
        }
    }
}

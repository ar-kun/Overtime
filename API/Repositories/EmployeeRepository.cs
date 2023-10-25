using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    // Defines EmployeeRepository that inherits from GeneralRepository<Employee>
    // Implements the IEmployeeRepository interface
    public class EmployeeRepository : GeneralRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(OvertimeDbContext context) : base(context)
        {
        }

        // Get Last NIK
        public string? GetLastNik()
        {
            var lastNik = _context.Set<Employee>()
                .OrderByDescending(e => e.Nik)
                .FirstOrDefault()?.Nik;

            return lastNik;
        }

        // Get Employee Email
        public Employee? GetByEmail(string email)
        {
            // Menggunakan LINQ untuk mencari Employee berdasarkan email
            return _context.Set<Employee>()
                .FirstOrDefault(e => e.Email == email);
        }

        // Get Manager Guid by Manager's NIK
        public Guid GetManagerGuid(string nik)
        {
            // Take one employee data
            var employee = _context.Set<Employee>()
                .Where(e => e.Nik == nik)
                .SingleOrDefault();

            if (employee != null)
            {
                return employee.Guid;
            }

            throw new Exception("Manager not found");
        }
    }
}
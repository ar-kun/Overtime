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

        // Get Email By EmployeeGuid
        public string? GetEmail(Guid? employeeGuid)
        {
           return _context.Set<Employee>()
                    .Where(e => e.Guid == employeeGuid)
                    .Select(e => e.Email)
                    .SingleOrDefault();
        }

        // Get Last NIK
        public string? GetLastNik()
        {
            var lastNik = _context.Set<Employee>()
                .OrderByDescending(e => e.Nik)
                .FirstOrDefault()?.Nik;

            return lastNik;
        }

        // Get Employee By Email
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

        public Employee? GetByGuid(Guid? managerGuid)
        {
            var entity = _context.Set<Employee>().Find(managerGuid);
            _context.ChangeTracker.Clear();
            return entity;
        }

        public IEnumerable<Employee> GetByManagerGuid(Guid guid)
        {
            return _context.Set<Employee>()
                .Where(e => e.ManagerGuid == guid)
                .ToList();
        }
    }
}
using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    // Defines OvertimeRepository that inherits from GeneralRepository<Overtime>
    // Implements the IOvertimeRepository interface
    public class OvertimeRepository : GeneralRepository<Overtime>, IOvertimeRepository
    {
        public OvertimeRepository(OvertimeDbContext context) : base(context) { }

        public void Update(Overtime overtime)
        {
            _context.Entry(overtime).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public IEnumerable<Overtime> GetByManagerGuid(Guid guid)
        {
            return _context.Set<Overtime>()
                .Where(o => o.Employee.ManagerGuid == guid)
                .ToList();
        }
    }
}

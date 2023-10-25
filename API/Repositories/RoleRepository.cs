using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    // Defines RoleRepository that inherits from GeneralRepository<Role>
    // Implements the IRoleRepository interface
    public class RoleRepository : GeneralRepository<Role>, IRoleRepository
    {
        public RoleRepository(OvertimeDbContext context) : base(context) { }

        public Guid? GetDefaultRoleGuid()
        {
            return _context.Set<Role>().FirstOrDefault(r => r.Name == "Employee")!.Guid;
        }
    }
}

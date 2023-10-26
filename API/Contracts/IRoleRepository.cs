using API.Models;

namespace API.Contracts
{
    // Defines an interface IRoleRepository that inherits from the IGeneralRepository<Role> interface
    public interface IRoleRepository : IGeneralRepository<Role>
    {
        Guid? GetDefaultRoleGuid();
    }
}

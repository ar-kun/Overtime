using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    // Defines AccountRoleRepository that inherits from GeneralRepository<AccountRole>
    // Implements the IAccountRoleRepository interface
    public class AccountRoleRepository : GeneralRepository<AccountRole>, IAccountRoleRepository
    {
        public AccountRoleRepository(OvertimeDbContext context) : base(context) { }
    }
}

using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    // Defines AccountRepository that inherits from GeneralRepository<Account>
    // Implements the IAccountRepository interface
    public class AccountRepository : GeneralRepository<Account>, IAccountRepository
    {
        public AccountRepository(OvertimeDbContext context) : base(context) { }
    }
}

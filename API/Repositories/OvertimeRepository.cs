using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    // Defines OvertimeRepository that inherits from GeneralRepository<Overtime>
    // Implements the IOvertimeRepository interface
    public class OvertimeRepository : GeneralRepository<Overtime>, IOvertimeRepository
    {
        public OvertimeRepository(OvertimeDbContext context) : base(context) { }
    }
}

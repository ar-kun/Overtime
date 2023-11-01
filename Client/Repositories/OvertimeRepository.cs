
using API.DTOs.Overtimes;
using Client.Contracts;

namespace Client.Repositories
{
    public class OvertimeRepository : GeneralRepository<OvertimeDto, Guid>, IOvertimeRepository
    {
        public OvertimeRepository(string request = "Overtime/") : base(request)  { }
    }
}

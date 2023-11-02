using API.DTOs.Overtimes;

namespace Client.Contracts
{
    public interface IOvertimeRepository : IRepository<OvertimeDto, Guid>
    {
    }
}

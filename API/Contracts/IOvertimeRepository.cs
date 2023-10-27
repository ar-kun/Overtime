using API.Models;

namespace API.Contracts
{
    // Defines an interface IOvertimeRepository that inherits from the IGeneralRepository<Overtime> interface
    public interface IOvertimeRepository : IGeneralRepository<Overtime>
    {
        void Update(Overtime overtime);

        IEnumerable<Overtime> GetByManagerGuid(Guid guid);
    }
}

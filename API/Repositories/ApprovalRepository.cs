using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    // Defines ApprovalRepository that inherits from GeneralRepository<Approval>
    // Implements the IApprovalRepository interface
    public class ApprovalRepository : GeneralRepository<Approval>, IApprovalRepository
    {
        public ApprovalRepository(OvertimeDbContext context) : base(context) { }
    }
}

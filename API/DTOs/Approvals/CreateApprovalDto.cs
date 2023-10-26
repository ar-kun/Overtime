using API.Models;
using API.Utilities.Enums;

namespace API.DTOs.Approvals
{
    public class CreateApprovalDto
    {
        public Guid Guid { get; set; }
        public ApprovalLevel ApprovalStatus { get; set; }
        public string ApprovedBy { get; set; }
        public string? Remarks { get; set; }

        // Declares a public static implicit conversion operator that takes a CreateApprovalDto parameter and returns a Approval object.
        public static implicit operator Approval(CreateApprovalDto createApprovalDto)
        {
            return new Approval
            {
                Guid = createApprovalDto.Guid,
                ApprovalStatus = createApprovalDto.ApprovalStatus,
                ApprovedBy = createApprovalDto.ApprovedBy,
                Remarks = createApprovalDto.Remarks,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }
    }
}

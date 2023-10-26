using API.DTOs.Employees;
using API.Models;
using API.Utilities.Enums;

namespace API.DTOs.Approvals
{
    public class ApprovalDto
    {
        public Guid Guid { get; set; }
        public ApprovalLevel ApprovalStatus { get; set; }
        public string ApprovedBy { get; set; }
        public string? Remarks { get; set; }

        // Declares a public static explicit conversion operator that takes a Approval parameter and returns a ApprovalDto object.
        public static explicit operator ApprovalDto(Approval approval)
        {
            return new ApprovalDto
            {
                Guid = approval.Guid,
                ApprovalStatus = approval.ApprovalStatus,
                ApprovedBy = approval.ApprovedBy,
                Remarks = approval.Remarks
            };
        }

        // Declares a public static implicit conversion operator that takes a ApprovalDto parameter and returns a Approval object.
        public static implicit operator Approval(ApprovalDto approvalDto)
        {
            return new Approval
            {
                Guid = approvalDto.Guid,
                ApprovalStatus = approvalDto.ApprovalStatus,
                ApprovedBy = approvalDto.ApprovedBy,
                Remarks = approvalDto.Remarks,
                ModifiedDate = DateTime.Now
            };
        }
    }
}

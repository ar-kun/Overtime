using API.DTOs.Approvals;
using FluentValidation;

namespace API.Utilities.Validations.Approvals
{
    public class UpdateApprovalValidator : AbstractValidator<ApprovalDto>
    {
        public UpdateApprovalValidator() 
        {
            // Declares a rule for Guid, ApprovalStatus, and ApprovedBy
            RuleFor(a => a.Guid).NotEmpty().WithMessage("Guid must not be empty");

            RuleFor(a => a.ApprovalStatus)
               .NotNull().WithMessage("ApprovalStatus must not be null")
               .IsInEnum();

            RuleFor(a => a.ApprovedBy).NotEmpty().WithMessage("ApprovedBy must not be empty");
        }
    }
}

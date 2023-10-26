using API.DTOs.AccountRoles;
using FluentValidation;

namespace API.Utilities.Validations.AccountRoles
{
    public class UpdateAccountRoleValidator : AbstractValidator<AccountRoleDto>
    {
        public UpdateAccountRoleValidator()
        {
            // Declares a rule for Guid, AccountGuid, and RoleGuid
            RuleFor(ar => ar.Guid).NotEmpty().WithMessage("Guid must not be empty");
            RuleFor(ar => ar.AccountGuid).NotEmpty().WithMessage("AccountGuid must not be empty");
            RuleFor(ar => ar.RoleGuid).NotEmpty().WithMessage("RoleGuid must not be empty");
        }
    }
}

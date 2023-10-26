using API.DTOs.AccountRoles;
using FluentValidation;

namespace API.Utilities.Validations.AccountRoles
{
    public class CreateAccountRoleValidator : AbstractValidator<CreateAccountRoleDto>
    {
        public CreateAccountRoleValidator()
        {
            // Declares a rule for AccountGuid and RoleGuid
            RuleFor(ar => ar.AccountGuid).NotEmpty().WithMessage("AccountGuid must not be empty");
            RuleFor(ar => ar.RoleGuid).NotEmpty().WithMessage("RoleGuid must not be empty");
        }
    }
}

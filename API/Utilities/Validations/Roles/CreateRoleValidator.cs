using API.DTOs.Roles;
using FluentValidation;

namespace API.Utilities.Validations.Roles
{
    public class CreateRoleValidator : AbstractValidator<CreateRoleDto>
    {
        public CreateRoleValidator()
        {
            // Declares a rule for the Name property.
            RuleFor(r => r.Name)
                .NotEmpty().WithMessage("Name must not be empty")
                .Matches(@"^[a-zA-Z ]+$").WithMessage("Name must only contain letters and spaces")
                .MaximumLength(100).WithMessage("Name must have at most 100 characters");
        }
    }
}

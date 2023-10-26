using API.DTOs.Accounts;
using FluentValidation;

namespace API.Utilities.Validations.Accounts
{
    public class CreateAccountValidator : AbstractValidator<CreateAccountDto>
    {
        public CreateAccountValidator() 
        {
            // Declares a rule for the Guid property must not be empty.
            RuleFor(a => a.Guid).NotEmpty().WithMessage("Guid must not be empty");

            // Declares a rule for the Password property.
            RuleFor(a => a.Password)
                .NotEmpty().WithMessage("Password must not be empty")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter")
                .Matches("[0-9]").WithMessage("Password must contain at least one digit")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character");
                //.Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).{8,}$")

            // Declares a rule for Otp, IsUsed and ExpiredDate
            RuleFor(a => a.Otp).NotNull().WithMessage("Otp must not be null");
            RuleFor(a => a.IsUsed).NotEmpty().WithMessage("IsUsed must not be empty");
            RuleFor(a => a.ExpiredDate).NotEmpty().WithMessage("ExpiredDate must not be empty");
        }
    }
}

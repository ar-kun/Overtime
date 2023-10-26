using API.DTOs.Accounts;
using FluentValidation;

namespace API.Utilities.Validations.Accounts
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordDto>
    {
        public ChangePasswordValidator()
        {
            // Declares a rule for Email
            RuleFor(a => a.Email)
               .NotEmpty().WithMessage("Email must not be empty")
               .EmailAddress().WithMessage("Incorrect email format")
               .MaximumLength(100).WithMessage("Email must have at most 100 characters");

            // Declares a rule for Otp
            RuleFor(a => a.Otp).NotNull().WithMessage("Otp must not be null");

            // Declares a rule for NewPassword 
            RuleFor(a => a.NewPassword)
                .NotEmpty().WithMessage("Password must not be empty")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter")
                .Matches("[0-9]").WithMessage("Password must contain at least one digit")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character");

            // Declares a rule for ConfirmPassword
            RuleFor(a => a.ConfirmPassword)
                .NotEmpty().WithMessage("Password must not be empty")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long")
                .Matches(a => a.NewPassword).WithMessage("ConfirmPassword must matches with NewPassword");
        }
    }
}

using API.DTOs.Accounts;
using FluentValidation;

namespace API.Utilities.Validations.Accounts
{
    public class RegisterValidator : AbstractValidator<RegisterDto>
    {
        public RegisterValidator() 
        {
            // Declares a rule for FirstName
            RuleFor(r => r.FirstName)
                .NotEmpty().WithMessage("FirstName must not be empty")
                .Matches(@"^[a-zA-Z ]+$").WithMessage("Major must only contain letters and spaces")
                .MaximumLength(100).WithMessage("Major must have at most 100 characters");

            // Declares a rule for BirthDate
            RuleFor(r => r.BirthDate)
               .NotEmpty().WithMessage("BirthDate must not be empty")
               .LessThanOrEqualTo(DateTime.Now.AddYears(-18)).WithMessage("Age must be greater than or equal to 18 years old");

            // Declares a rule for Gender
            RuleFor(r => r.Gender)
               .NotNull().WithMessage("Gender must not be null")
               .IsInEnum();

            // Declares a rule for HiringDate 
            RuleFor(r => r.HiringDate)
                .NotEmpty().WithMessage("HiringDate must not be empty")
                .LessThanOrEqualTo(DateTime.Today).WithMessage("HiringDate not valid");

            // Declares a rule for Email
            RuleFor(r => r.Email)
               .NotEmpty().WithMessage("Email must not be empty")
               .EmailAddress().WithMessage("Incorrect email format")
               .MaximumLength(100).WithMessage("Email must have at most 100 characters");

            // Declares a rule for PhoneNumber 
            RuleFor(r => r.PhoneNumber)
               .NotEmpty().WithMessage("PhoneNumber must not be empty")
               .Matches(@"^[0-9]+$").WithMessage("PhoneNumber must only contain numbers")
               .MinimumLength(10)
               .MaximumLength(20);

            // Declares a rule for Password 
            RuleFor(r => r.Password)
                .NotEmpty().WithMessage("Password must not be empty")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter")
                .Matches("[0-9]").WithMessage("Password must contain at least one digit")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character");

            // Declares a rule for ConfirmPassword 
            RuleFor(r => r.ConfirmPassword)
                .NotEmpty().WithMessage("Password must not be empty")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long")
                .Matches(a => a.Password).WithMessage("ConfirmPassword must matches with NewPassword");
        }
    }
}

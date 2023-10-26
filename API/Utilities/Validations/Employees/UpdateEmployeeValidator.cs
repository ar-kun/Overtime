using API.DTOs.Employees;
using FluentValidation;

namespace API.Utilities.Validations.Employees
{
    public class UpdateEmployeeValidator : AbstractValidator<UpdateEmployeeDto>
    {
        public UpdateEmployeeValidator() 
        {
            // Declares a rule for Guid
            RuleFor(e => e.Guid).NotEmpty().WithMessage("Guid must not be empty");

            // Declares a rule for Nik
            RuleFor(e => e.Nik)
                .NotEmpty().WithMessage("Nik must not be empty")
                .MaximumLength(6).WithMessage("Nik must have at most 6 characters");

            // Declares a rule for FirstName 
            RuleFor(e => e.FirstName)
                .NotEmpty().WithMessage("FirstName must not be empty")
                .Matches(@"^[a-zA-Z ]+$").WithMessage("Major must only contain letters and spaces")
                .MaximumLength(100).WithMessage("Major must have at most 100 characters");

            // Declares a rule for BirthDate 
            RuleFor(e => e.BirthDate)
               .NotEmpty().WithMessage("BirthDate must not be empty")
               .LessThanOrEqualTo(DateTime.Now.AddYears(-18)).WithMessage("Age must be greater than or equal to 18 years old");

            // Declares a rule for Gender
            RuleFor(e => e.Gender)
               .NotNull().WithMessage("Gender must not be null")
               .IsInEnum();

            // Declares a rule for HiringDate 
            RuleFor(e => e.HiringDate)
                .NotEmpty().WithMessage("HiringDate must not be empty")
                .LessThanOrEqualTo(DateTime.Today).WithMessage("HiringDate not valid");

            // Declares a rule for Email
            RuleFor(e => e.Email)
               .NotEmpty().WithMessage("Email must not be empty")
               .EmailAddress().WithMessage("Incorrect email format")
               .MaximumLength(100).WithMessage("Email must have at most 100 characters");

            // Declares a rule for PhoneNumber
            RuleFor(e => e.PhoneNumber)
               .NotEmpty().WithMessage("PhoneNumber must not be empty")
               .Matches(@"^[0-9]+$").WithMessage("PhoneNumber must only contain numbers")
               .MinimumLength(10)
               .MaximumLength(20);

            // Declares a rule for Salary
            RuleFor(e => e.Salary).NotEmpty().WithMessage("Salary must not be empty");
        }
    }
}

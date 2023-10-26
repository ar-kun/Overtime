using API.DTOs.Overtimes;
using FluentValidation;

namespace API.Utilities.Validations.Overtimes
{
    public class OvertimeValidator : AbstractValidator<StoreOvertimeDto>
    {
        public OvertimeValidator() 
        {
            // Declares a rule for Guid, EmployeeGuid, DateRequest, and Duration
            RuleFor(o => o.Guid).NotEmpty().WithMessage("Guid must not be empty");
            RuleFor(o => o.EmployeeGuid).NotEmpty().WithMessage("EmployeeGuid must not be empty");
            RuleFor(o => o.DateRequest).NotEmpty().WithMessage("DateRequest must not be empty");
            RuleFor(o => o.Duration).NotEmpty().WithMessage("Duration must not be empty");

            // Declares a rule for Status
            RuleFor(o => o.Status)
               .NotNull().WithMessage("Status must not be null")
               .IsInEnum();

            // Declares a rule for TypeOfDay
            RuleFor(o => o.TypeOfDay)
               .NotNull().WithMessage("TypeOfDay must not be null")
               .IsInEnum();
        }
    }
}

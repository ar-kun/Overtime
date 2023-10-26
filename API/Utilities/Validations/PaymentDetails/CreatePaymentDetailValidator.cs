using API.DTOs.PaymentDetails;
using FluentValidation;

namespace API.Utilities.Validations.PaymentDetails
{
    public class CreatePaymentDetailValidator : AbstractValidator<CreatePaymentDetailDto>
    {
        public CreatePaymentDetailValidator() 
        {
            // Declares a rule for Guid and TotalPay
            RuleFor(p => p.Guid).NotEmpty().WithMessage("Guid must not be empty");
            RuleFor(p => p.TotalPay).NotEmpty().WithMessage("TotalPay must not be empty");
        }
    }
}

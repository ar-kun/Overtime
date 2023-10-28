using API.Models;
using API.Utilities.Enums;

namespace API.Contracts
{
    // Defines an interface IPaymentDetailsRepository that inherits from the IGeneralRepository<PaymentDetails> interface
    public interface IPaymentDetailRepository : IGeneralRepository<PaymentDetail>
    {
        int GetTotalPay(TypeDayLevel typeOfDay, int duration, int salary);

        IEnumerable<PaymentDetail> GetByEmployeeGuid(Guid guid);
    }
}

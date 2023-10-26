using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    // Defines PaymentDetailsRepository that inherits from GeneralRepository<PaymentDetails>
    // Implements the IPaymentDetailsRepository interface
    public class PaymentDetailRepository : GeneralRepository<PaymentDetail>, IPaymentDetailRepository
    {
        public PaymentDetailRepository(OvertimeDbContext context) : base(context) { }
    }
}

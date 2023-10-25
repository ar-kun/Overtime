using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    // Defines PaymentDetailsRepository that inherits from GeneralRepository<PaymentDetails>
    // Implements the IPaymentDetailsRepository interface
    public class PaymentDetailsRepository : GeneralRepository<PaymentDetails>, IPaymentDetailsRepository
    {
        public PaymentDetailsRepository(OvertimeDbContext context) : base(context) { }
    }
}

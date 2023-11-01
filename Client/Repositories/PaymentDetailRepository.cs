using API.Models;
using Client.Contracts;

namespace Client.Repositories
{
    public class PaymentDetailRepository : GeneralRepository<PaymentDetail, Guid>, IPaymentDetailRepository
    {
        public PaymentDetailRepository(string request = "PaymentDetail/") : base(request)
        {
        }
    }
}

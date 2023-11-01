using API.DTOs.PaymentDetails;
using Client.Contracts;

namespace Client.Repositories
{
    public class PaymentDetailRepository : GeneralRepository<EmployeesPayrollDto, Guid>, IPaymentDetailRepository
    {
        public PaymentDetailRepository(string request = "PaymentDetail/details/") : base(request)
        {
        }
    }
}

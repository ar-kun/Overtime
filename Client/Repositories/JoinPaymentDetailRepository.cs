using API.DTOs.PaymentDetails;
using Client.Contracts;

namespace Client.Repositories
{
    public class JoinPaymentDetailRepository : GeneralRepository<EmployeesPayrollDto, Guid>, IJoinPaymentDetailRepository
    {
        public JoinPaymentDetailRepository(string request = "PaymentDetail/") : base(request)
        {
        }
    }
}

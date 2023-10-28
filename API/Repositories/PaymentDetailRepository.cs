using API.Contracts;
using API.Data;
using API.Models;
using API.Utilities.Enums;

namespace API.Repositories
{
    // Defines PaymentDetailsRepository that inherits from GeneralRepository<PaymentDetails>
    // Implements the IPaymentDetailsRepository interface
    public class PaymentDetailRepository : GeneralRepository<PaymentDetail>, IPaymentDetailRepository
    {
        public PaymentDetailRepository(OvertimeDbContext context) : base(context) { }

        public int GetTotalPay(TypeDayLevel typeOfDay, int duration, int salary)
        {
            var totalPay = 0;
            if (typeOfDay == TypeDayLevel.WeekDay)
            {
                if (duration <= 1)
                {
                    totalPay = (int)Math.Ceiling(1.5 * (1.0 / 173) * salary);
                }
                else
                {
                    totalPay = (int)Math.Ceiling((1.5 * (1.0 / 173) * salary) + ((duration - 1) * (2.0 / 173) * salary));
                }
            }
            else
            {
                if (duration <= 8)
                {
                    totalPay = (int)Math.Ceiling(duration * (2 * (1.0 / 173) * salary));
                }
                else if (duration == 9)
                {
                    totalPay = (int)Math.Ceiling((8 * (2 * (1.0 / 173) * salary)) + (3 * (1.0 / 173) * salary));
                }
                else if (duration >= 10 && duration <= 12)
                {
                    totalPay = (int)Math.Ceiling((8 * (2 * (1.0 / 173) * salary)) + (3 * (1.0 / 173) * salary) + ((duration - 9) * (4 * (1.0 / 173) * salary)));
                }
            }
            return totalPay;
        }

        public IEnumerable<PaymentDetail> GetByEmployeeGuid(Guid guid)
        {
            return _context.Set<PaymentDetail>()
                .Where(o => o.Overtime.EmployeeGuid == guid)
                .ToList();
        }
    }
}

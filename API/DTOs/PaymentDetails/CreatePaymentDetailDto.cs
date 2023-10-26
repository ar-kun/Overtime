using API.DTOs.Approvals;
using API.Models;

namespace API.DTOs.PaymentDetails
{
    public class CreatePaymentDetailDto
    {
        public Guid Guid { get; set; }
        public int TotalPay { get; set; }

        // Declares a public static implicit conversion operator that takes a CreateApprovalDto parameter and returns a Approval object.
        public static implicit operator PaymentDetail(CreatePaymentDetailDto createPaymentDetailDto)
        {
            return new PaymentDetail
            {
                Guid = createPaymentDetailDto.Guid,
                TotalPay = createPaymentDetailDto.TotalPay,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }
    }
}

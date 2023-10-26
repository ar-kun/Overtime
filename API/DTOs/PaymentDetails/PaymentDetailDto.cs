using API.DTOs.Approvals;
using API.Models;

namespace API.DTOs.PaymentDetails
{
    public class PaymentDetailDto
    {
        public Guid Guid { get; set; }
        public string TotalPay { get; set; }

        // Declares a public static explicit conversion operator that takes a PaymentDetail parameter and returns a PaymentDetailDto object.
        public static explicit operator PaymentDetailDto(PaymentDetail paymentDetail)
        {
            return new PaymentDetailDto
            {
                Guid = paymentDetail.Guid,
                TotalPay = paymentDetail.TotalPay
            };
        }

        // Declares a public static implicit conversion operator that takes a PaymentDetailDto parameter and returns a PaymentDetail object.
        public static implicit operator PaymentDetail(PaymentDetailDto paymentDetailDto)
        {
            return new PaymentDetail
            {
                Guid = paymentDetailDto.Guid,
                TotalPay = paymentDetailDto.TotalPay,
                ModifiedDate = DateTime.Now
            };
        }
    }
}

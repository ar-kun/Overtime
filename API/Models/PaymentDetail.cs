using API.Utilities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    // PaymentDetail entity
    [Table(name: "tb_m_payment_details")]
    public class PaymentDetail : BaseEntity
    {
        [Column(name: "total_pay")]
        public int TotalPay { get; set; }
        [Column(name: "payment_status")]
        public PaymentLevel PaymentStatus { get; set; }

        [Column(name: "created_date")]
        public DateTime CreatedDate { get; set; }

        [Column(name: "modified_date")]
        public DateTime ModifiedDate { get; set; }

        // Cardinality
        public Overtime? Overtime { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    // PaymentDetails entity
    [Table(name: "tb_m_payment_details")]
    public class PaymentDetails : BaseEntity
    {
        [Column(name: "total_pay", TypeName = "nvarchar(30)")]
        public string TotalPay { get; set; }

        [Column(name: "created_date")]
        public DateTime CreatedDate { get; set; }

        [Column(name: "modified_date")]
        public DateTime ModifiedDate { get; set; }

        // Cardinality
        public ICollection<Overtime>? Overtimes { get; set; }
    }
}

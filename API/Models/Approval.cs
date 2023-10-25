using API.Utilities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    // Approval entity
    [Table(name: "tb_m_approvals")]
    public class Approval : BaseEntity
    {
        [Column(name: "approval_status")]
        public ApprovalLevel ApprovalStatus { get; set; }

        [Column(name: "approved_by", TypeName = "nvarchar(100)")]
        public string ApprovedBy { get; set; }

        [Column(name: "remarks", TypeName = "nvarchar(max)")]
        public string Remarks { get; set; }
        
        [Column(name: "created_date")]
        public DateTime CreatedDate { get; set; }

        [Column(name: "modified_date")]
        public DateTime ModifiedDate { get; set; }

        // Cardinality
        public Overtime? Overtime { get; set; }
    }
}

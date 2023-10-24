using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    // Role entity
    [Table(name: "tb_m_roles")]
    public class Role : BaseEntity
    {
        [Column(name: "name", TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        [Column(name: "created_date")]
        public DateTime CreatedDate { get; set; }

        [Column(name: "modified_date")]
        public DateTime ModifiedDate { get; set; }

        // Cardinality
        public ICollection<AccountRole>? AccountRoles { get; set; }
    }
}
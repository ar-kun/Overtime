using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

namespace API.Models
{
    // Account Role entity
    [Table(name: "tb_m_account_roles")]
    public class AccountRole : BaseEntity
    {
        [Column(name: "account_guid")]
        public Guid AccountGuid { get; set; }

        [Column(name: "role_guid")]
        public Guid RoleGuid { get; set; }

        [Column(name: "created_date")]
        public DateTime CreatedDate { get; set; }

        [Column(name: "modified_date")]
        public DateTime ModifiedDate { get; set; }

        // Cardinality
        public Account? Account { get; set; }

        public Role? Role { get; set; }
    }
}
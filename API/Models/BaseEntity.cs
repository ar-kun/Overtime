using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class BaseEntity
    {
        [Key, Column(name: "guid")]
        public Guid Guid { get; set; }
    }
}
using API.Utilities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Overtime : BaseEntity
    {
        [Column(name: "employee_guid")]
        public Guid EmployeeGuid { get; set; }

        [Column(name: "date_request")]
        public DateTime DateRequest { get; set; }

        [Column(name: "duration")]
        public int Duration { get; set; }

        [Column(name: "status")]
        public StatusLevel Status { get; set; }

        [Column(name: "remarks", TypeName = "nvarchar(max)")]
        public string Remarks { get; set; }

        [Column(name: "type_of_day")]
        public TypeDayLevel TypeOfDay { get; set; }

        // Cardinality
        public Employee? Employee { get; set; }
    }
}
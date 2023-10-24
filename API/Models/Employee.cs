using API.Utilities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

namespace API.Models
{
    // Employee entity
    [Table(name: "tb_m_employees")]
    public class Employee : BaseEntity
    {
        [Column(name: "manager_guid")]
        public Guid? ManagerGuid { get; set; }

        [Column(name: "nik", TypeName = "nchar(6)")]
        public string Nik { get; set; }

        [Column(name: "first_name", TypeName = "nvarchar(100)")]
        public string FirstName { get; set; }

        [Column(name: "last_name", TypeName = "nvarchar(100)")]
        public string? LastName { get; set; }

        [Column(name: "birth_date")]
        public DateTime BirthDate { get; set; }

        [Column(name: "gender")]
        public GenderLevel Gender { get; set; }

        [Column(name: "hiring_date")]
        public DateTime HiringDate { get; set; }

        [Column(name: "email", TypeName = "nvarchar(100)")]
        public string Email { get; set; }

        [Column(name: "phone_number", TypeName = "nvarchar(20)")]
        public string PhoneNumber { get; set; }

        [Column(name: "salary")]
        public int Salary { get; set; }

        [Column(name: "created_date")]
        public DateTime CreatedDate { get; set; }

        [Column(name: "modified_date")]
        public DateTime ModifiedDate { get; set; }

        // Cardinality
        public Account? Account { get; set; }

        public ICollection<Overtime>? Overtime { get; set; }

        public Employee? Manager { get; set; } // cardinality ke atasan

        public ICollection<Employee>? Subordinates { get; set; } // cardinality ke bawahan
    }
}
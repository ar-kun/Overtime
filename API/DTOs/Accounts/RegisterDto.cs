using API.Utilities.Enums;

namespace API.DTOs.Accounts
{
    public class RegisterDto
    {
        // Declares a public property for RegisterDto
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public GenderLevel Gender { get; set; }
        public DateTime HiringDate { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int Salary { get; set; }
        //public string? ManagerNik { get; set; }
        public Guid ManagerGuid { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
using API.Models;

namespace API.DTOs.Accounts
{
    public class ChangePasswordDto
    {
        // Declares a public property for ChangePasswordDto
        public string Email { get; set; } 
        public int Otp { get; set; } 
        public string NewPassword { get; set; } 
        public string ConfirmPassword { get; set; }

        public static implicit operator Account(ChangePasswordDto changePasswordDto)
        {
            // Returns a new Account object with the properties of the changePasswordDto parameter.
            return new Account
            {
                Password = changePasswordDto.NewPassword,
                Otp = changePasswordDto.Otp,
                IsUsed = true,
                ModifiedDate = DateTime.Now
            };
        }
    }
}

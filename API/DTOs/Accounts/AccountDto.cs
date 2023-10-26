using API.Models;

namespace API.DTOs.Accounts
{
    public class AccountDto
    {
        // Declares a public property for AccountDto
        public Guid Guid { get; set; }
        public string Password { get; set; } 
        public int Otp { get; set; } 
        public bool IsUsed { get; set; } 
        public DateTime ExpiredDate { get; set; }

        // Declares a public static explicit conversion operator that takes an Account parameter and returns an AccountDto object.
        public static explicit operator AccountDto(Account account)
        {
            return new AccountDto
            {
                Guid = account.Guid,
                Password = account.Password,
                Otp = account.Otp,
                IsUsed = account.IsUsed,
                ExpiredDate = account.ExpiredDate
            };
        }

        // Declares a public static implicit conversion operator that takes a AccountDto parameter and returns a Account object.
        public static implicit operator Account(AccountDto accountDto)
        {
            return new Account
            {
                Guid = accountDto.Guid,
                Password = accountDto.Password,
                Otp = accountDto.Otp,
                IsUsed = accountDto.IsUsed,
                ExpiredDate = accountDto.ExpiredDate,
                ModifiedDate = DateTime.Now
            };
        }
    }
}

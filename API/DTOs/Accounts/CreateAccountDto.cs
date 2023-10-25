﻿using API.Models;

namespace API.DTOs.Accounts
{
    public class CreateAccountDto
    {
        // Declares a public property for CreateAccountDto
        public Guid Guid { get; set; } 
        public string Password { get; set; } 
        public int Otp { get; set; } 
        public bool IsUsed { get; set; } 
        public DateTime ExpiredDate { get; set; }

        // Declares a public static implicit conversion operator that takes a CreateAccountDto parameter and returns a Account object.
        public static implicit operator Account(CreateAccountDto createAccountDto)
        {
            return new Account
            {
                Guid = createAccountDto.Guid,
                Password = createAccountDto.Password,
                Otp = createAccountDto.Otp,
                IsUsed = createAccountDto.IsUsed,
                ExpiredDate = createAccountDto.ExpiredDate,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }
    }
}
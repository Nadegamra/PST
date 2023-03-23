﻿using System.ComponentModel.DataAnnotations;

namespace Backend.Data.Views
{
    public class CompanyRegister
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string CompanyCode { get; set; }

        [Required]
        public string CompanyName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

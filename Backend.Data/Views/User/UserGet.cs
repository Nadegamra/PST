﻿namespace Backend.Data.Views.User
{
    public class UserGet
    {
        public int Id { get; set; }
        public string? Role { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? CompanyCode { get; set; }
        public string? CompanyName { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public bool? EmailConfirmed { get; set; }

        public string? Country { get; set; }
        public string? County { get; set; }

        public string? City { get; set; }
        public string StreetAddress { get; set; } = String.Empty;
        public string PostalCode { get; set; } = String.Empty;
        public bool? IsCompany { get; set; }
    }
}

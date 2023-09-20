using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace Backend.Test.Mocks
{
    public class MockData
    {
        public static List<User> Users = new List<User> {
            new User { Id = 1, IsCompany=false, FirstName = "Admy", LastName = "Nisterson", UserName = "admin@admin.com", NormalizedUserName = "ADMIN@ADMIN.COM", Email = "admin@admin.com", EmailConfirmed=true, NormalizedEmail = "ADMIN@ADMIN.COM", PasswordHash = "AQAAAAEAACcQAAAAEK4hVsHx9G6FTUDDlJaY/l1aRXqpoUZU9nkEkvECUI2uQ+FHoFYHjlJpmP3KOss/qg==", SecurityStamp = Guid.NewGuid().ToString()},
            new User { Id = 2, IsCompany=false,  FirstName = "Cuzy", LastName= "Tomerson", UserName = "customer@example.com", NormalizedUserName = "CUSTOMER@EXAMPLE.COM", Email = "customer@example.com", EmailConfirmed = true, NormalizedEmail = "CUSTOMER@EXAMPLE.COM", PasswordHash = "AQAAAAEAACcQAAAAEK4hVsHx9G6FTUDDlJaY/l1aRXqpoUZU9nkEkvECUI2uQ+FHoFYHjlJpmP3KOss/qg==", SecurityStamp = Guid.NewGuid().ToString()},
            new User { Id = 3, IsCompany=true,  CompanyName = "UAB „Tikra įmonė“", CompanyCode = "123456", UserName = "company@example.com", NormalizedUserName = "COMPANY@EXAMPLE.COM", Email = "company@example.com", EmailConfirmed = true, NormalizedEmail = "COMPANY@EXAMPLE.COM", PasswordHash = "AQAAAAEAACcQAAAAEK4hVsHx9G6FTUDDlJaY/l1aRXqpoUZU9nkEkvECUI2uQ+FHoFYHjlJpmP3KOss/qg==", SecurityStamp = Guid.NewGuid().ToString()}
        };

        public static List<IdentityRole<int>> Roles = new List<IdentityRole<int>> {
            new IdentityRole<int> { Id = 1, Name = "admin", NormalizedName = "ADMIN" },
            new IdentityRole<int> { Id = 2, Name = "lender", NormalizedName = "LENDER" },
            new IdentityRole<int> { Id = 3, Name = "borrower", NormalizedName = "BORROWER"}
        };

        public static List<IdentityUserRole<int>> UserRoles = new List<IdentityUserRole<int>> {
            new IdentityUserRole<int> { UserId = 1, RoleId = 1 },
            new IdentityUserRole<int> { UserId = 2, RoleId = 2 },
            new IdentityUserRole<int> { UserId = 3, RoleId = 3 }
        };
    }
}
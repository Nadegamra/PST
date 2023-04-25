﻿using Backend.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace Backend.Data
{
    public class AppDbContext: IdentityDbContext<User, IdentityRole<int>, int>
    {
        public DbSet<RegistrationRequest> RegistrationRequests { get; set; }
        
        public DbSet<Models.Console> Consoles  { get; set; }
        public DbSet<Accessory> Accessories { get; set; }
        public DbSet<Image> Images { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Console>()
                .HasMany(a => a.Images)
                .WithOne(c => c.Console)
                .HasForeignKey(a => a.ConsoleId);

            modelBuilder.Entity<IdentityRole<int>>().HasData(new IdentityRole<int> { Id = 1, Name = "admin", NormalizedName = "ADMIN" },
                                                            new IdentityRole<int> { Id = 2, Name = "lender", NormalizedName = "LENDER" },
                                                            new IdentityRole<int> { Id = 3, Name = "borrower", NormalizedName = "BORROWER"});

            modelBuilder.Entity<User>().HasData(new User { Id = 1, IsCompany=false, FirstName = "Admy", LastName = "Nisterson", UserName = "admin@admin.com", NormalizedUserName = "ADMIN@ADMIN.COM", Email = "admin@admin.com", EmailConfirmed=true, NormalizedEmail = "ADMIN@ADMIN.COM", PasswordHash = "AQAAAAEAACcQAAAAEK4hVsHx9G6FTUDDlJaY/l1aRXqpoUZU9nkEkvECUI2uQ+FHoFYHjlJpmP3KOss/qg==", SecurityStamp = Guid.NewGuid().ToString()},
                                                new User { Id = 2, IsCompany=false,  FirstName = "Cuzy", LastName= "Tomerson", UserName = "customer@example.com", NormalizedUserName = "CUSTOMER@EXAMPLE.COM", Email = "customer@example.com", EmailConfirmed = false, NormalizedEmail = "CUSTOMER@EXAMPLE.COM", PasswordHash = "AQAAAAEAACcQAAAAEK4hVsHx9G6FTUDDlJaY/l1aRXqpoUZU9nkEkvECUI2uQ+FHoFYHjlJpmP3KOss/qg==", SecurityStamp = Guid.NewGuid().ToString()},
                                                new User { Id = 3, IsCompany=true,  CompanyName = "UAB „Tikra įmonė“", CompanyCode = "123456", UserName = "company@example.com", NormalizedUserName = "COMPANY@EXAMPLE.COM", Email = "company@example.com", EmailConfirmed = true, NormalizedEmail = "COMPANY@EXAMPLE.COM", PasswordHash = "AQAAAAEAACcQAAAAEK4hVsHx9G6FTUDDlJaY/l1aRXqpoUZU9nkEkvECUI2uQ+FHoFYHjlJpmP3KOss/qg==", SecurityStamp = Guid.NewGuid().ToString() });

            modelBuilder.Entity<IdentityUserRole<int>>().HasData(new IdentityUserRole<int> { UserId = 1, RoleId = 1 },
                                                                 new IdentityUserRole<int> { UserId = 2, RoleId = 2 },
                                                                 new IdentityUserRole<int> { UserId = 3, RoleId = 3 });

            base.OnModelCreating(modelBuilder);
        }
    }
}

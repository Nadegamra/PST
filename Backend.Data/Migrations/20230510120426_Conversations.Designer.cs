﻿// <auto-generated />
using System;
using Backend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Backend.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230510120426_Conversations")]
    partial class Conversations
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Backend.Data.Models.Console", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<decimal>("DailyPrice")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Consoles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DailyPrice = 8m,
                            Description = "Microsoft Xbox One",
                            Name = "Xbox One"
                        },
                        new
                        {
                            Id = 2,
                            DailyPrice = 9m,
                            Description = "Sony Playstation 5",
                            Name = "Playstation 5"
                        });
                });

            modelBuilder.Entity("Backend.Data.Models.Conversation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("UserConsoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserConsoleId")
                        .IsUnique();

                    b.ToTable("Conversations");
                });

            modelBuilder.Entity("Backend.Data.Models.EmailChangeToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("NewEmail")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("UserId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("EmailChangeTokens");
                });

            modelBuilder.Entity("Backend.Data.Models.EmailConfirmationToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("UserId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("EmailConfirmationTokens");
                });

            modelBuilder.Entity("Backend.Data.Models.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("ConsoleId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("UserConsoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ConsoleId");

                    b.HasIndex("UserConsoleId");

                    b.ToTable("Images");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ConsoleId = 1,
                            Description = "",
                            Name = "1.jpeg",
                            Path = "gvoktfyvobny0j2umvtt"
                        },
                        new
                        {
                            Id = 2,
                            ConsoleId = 1,
                            Description = "",
                            Name = "2.jpeg",
                            Path = "owdqtg9fodxw8ubvmavs"
                        },
                        new
                        {
                            Id = 3,
                            ConsoleId = 1,
                            Description = "",
                            Name = "3.jpeg",
                            Path = "d0sid8ixuhrgcx4melbs"
                        },
                        new
                        {
                            Id = 4,
                            ConsoleId = 2,
                            Description = "",
                            Name = "P5.webp",
                            Path = "tmhke7yuza1v9zhourmc"
                        },
                        new
                        {
                            Id = 5,
                            ConsoleId = 2,
                            Description = "",
                            Name = "P5.jpeg",
                            Path = "hjzaamg3uuftq1vsgctt"
                        },
                        new
                        {
                            Id = 6,
                            ConsoleId = 2,
                            Description = "",
                            Name = "P5.png",
                            Path = "dnj7iggkdupgcl9wdide"
                        });
                });

            modelBuilder.Entity("Backend.Data.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ConversationId")
                        .HasColumnType("int");

                    b.Property<bool>("FromAdmin")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ConversationId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Backend.Data.Models.PasswordResetToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("UserId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("PasswordResetTokens");
                });

            modelBuilder.Entity("Backend.Data.Models.RegistrationRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CompanyCode")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("RegistrationRequests");
                });

            modelBuilder.Entity("Backend.Data.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<string>("CompanyCode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<string>("County")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("IsCompany")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<string>("StreetAddress")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AccessFailedCount = 0,
                            City = "",
                            CompanyCode = "",
                            CompanyName = "",
                            ConcurrencyStamp = "a224e2a4-2723-4402-ae80-6e0f72ab3117",
                            Country = "",
                            County = "",
                            Email = "admin@admin.com",
                            EmailConfirmed = true,
                            FirstName = "Admy",
                            IsCompany = false,
                            LastName = "Nisterson",
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@ADMIN.COM",
                            NormalizedUserName = "ADMIN@ADMIN.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAEK4hVsHx9G6FTUDDlJaY/l1aRXqpoUZU9nkEkvECUI2uQ+FHoFYHjlJpmP3KOss/qg==",
                            PhoneNumberConfirmed = false,
                            PostalCode = "",
                            SecurityStamp = "31f58844-416d-4daf-91fe-4887acd7316d",
                            StreetAddress = "",
                            TwoFactorEnabled = false,
                            UserName = "admin@admin.com"
                        },
                        new
                        {
                            Id = 2,
                            AccessFailedCount = 0,
                            City = "",
                            CompanyCode = "",
                            CompanyName = "",
                            ConcurrencyStamp = "a86a7c37-3058-4f9b-b26a-0320550acd44",
                            Country = "",
                            County = "",
                            Email = "customer@example.com",
                            EmailConfirmed = false,
                            FirstName = "Cuzy",
                            IsCompany = false,
                            LastName = "Tomerson",
                            LockoutEnabled = false,
                            NormalizedEmail = "CUSTOMER@EXAMPLE.COM",
                            NormalizedUserName = "CUSTOMER@EXAMPLE.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAEK4hVsHx9G6FTUDDlJaY/l1aRXqpoUZU9nkEkvECUI2uQ+FHoFYHjlJpmP3KOss/qg==",
                            PhoneNumberConfirmed = false,
                            PostalCode = "",
                            SecurityStamp = "8cc366dd-b639-4bc1-b102-8faee54b7bb5",
                            StreetAddress = "",
                            TwoFactorEnabled = false,
                            UserName = "customer@example.com"
                        },
                        new
                        {
                            Id = 3,
                            AccessFailedCount = 0,
                            City = "",
                            CompanyCode = "123456",
                            CompanyName = "UAB „Tikra įmonė“",
                            ConcurrencyStamp = "ae5e0e4e-028a-4724-9ab1-c8f7422fd298",
                            Country = "",
                            County = "",
                            Email = "company@example.com",
                            EmailConfirmed = true,
                            FirstName = "",
                            IsCompany = true,
                            LastName = "",
                            LockoutEnabled = false,
                            NormalizedEmail = "COMPANY@EXAMPLE.COM",
                            NormalizedUserName = "COMPANY@EXAMPLE.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAEK4hVsHx9G6FTUDDlJaY/l1aRXqpoUZU9nkEkvECUI2uQ+FHoFYHjlJpmP3KOss/qg==",
                            PhoneNumberConfirmed = false,
                            PostalCode = "",
                            SecurityStamp = "6ce6f9ce-62c1-48ef-b799-9582b60987f9",
                            StreetAddress = "",
                            TwoFactorEnabled = false,
                            UserName = "company@example.com"
                        });
                });

            modelBuilder.Entity("Backend.Data.Models.UserConsole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Accessories")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<int?>("ConsoleId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int>("ConsoleStatus")
                        .HasColumnType("int");

                    b.Property<int?>("ConversationId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ConsoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserConsoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = 2,
                            Name = "lender",
                            NormalizedName = "LENDER"
                        },
                        new
                        {
                            Id = 3,
                            Name = "borrower",
                            NormalizedName = "BORROWER"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            RoleId = 1
                        },
                        new
                        {
                            UserId = 2,
                            RoleId = 2
                        },
                        new
                        {
                            UserId = 3,
                            RoleId = 3
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Backend.Data.Models.Conversation", b =>
                {
                    b.HasOne("Backend.Data.Models.UserConsole", "UserConsole")
                        .WithOne("Conversation")
                        .HasForeignKey("Backend.Data.Models.Conversation", "UserConsoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserConsole");
                });

            modelBuilder.Entity("Backend.Data.Models.EmailChangeToken", b =>
                {
                    b.HasOne("Backend.Data.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Backend.Data.Models.EmailConfirmationToken", b =>
                {
                    b.HasOne("Backend.Data.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Backend.Data.Models.Image", b =>
                {
                    b.HasOne("Backend.Data.Models.Console", "Console")
                        .WithMany("Images")
                        .HasForeignKey("ConsoleId");

                    b.HasOne("Backend.Data.Models.UserConsole", "UserConsole")
                        .WithMany("Images")
                        .HasForeignKey("UserConsoleId");

                    b.Navigation("Console");

                    b.Navigation("UserConsole");
                });

            modelBuilder.Entity("Backend.Data.Models.Message", b =>
                {
                    b.HasOne("Backend.Data.Models.Conversation", "Conversation")
                        .WithMany("Messages")
                        .HasForeignKey("ConversationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Conversation");
                });

            modelBuilder.Entity("Backend.Data.Models.PasswordResetToken", b =>
                {
                    b.HasOne("Backend.Data.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Backend.Data.Models.UserConsole", b =>
                {
                    b.HasOne("Backend.Data.Models.Console", "Console")
                        .WithMany()
                        .HasForeignKey("ConsoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Backend.Data.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Console");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<int>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("Backend.Data.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("Backend.Data.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<int>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Backend.Data.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("Backend.Data.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Backend.Data.Models.Console", b =>
                {
                    b.Navigation("Images");
                });

            modelBuilder.Entity("Backend.Data.Models.Conversation", b =>
                {
                    b.Navigation("Messages");
                });

            modelBuilder.Entity("Backend.Data.Models.UserConsole", b =>
                {
                    b.Navigation("Conversation");

                    b.Navigation("Images");
                });
#pragma warning restore 612, 618
        }
    }
}

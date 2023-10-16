using Microsoft.AspNetCore.Identity;

namespace Backend.Test.Mocks
{
    public class MockData
    {
        public static List<User> Users = new List<User> {
            new User { Id = 1, IsCompany=false, FirstName = "Admy", LastName = "Nisterson", UserName = "admin@admin.com", NormalizedUserName = "ADMIN@ADMIN.COM", Email = "admin@admin.com", EmailConfirmed=true, NormalizedEmail = "ADMIN@ADMIN.COM", PasswordHash = "Password", SecurityStamp = Guid.NewGuid().ToString()},
            new User { Id = 2, IsCompany=false,  FirstName = "Cuzy", LastName= "Tomerson", UserName = "customer@example.com", NormalizedUserName = "CUSTOMER@EXAMPLE.COM", Email = "customer@example.com", EmailConfirmed = true, NormalizedEmail = "CUSTOMER@EXAMPLE.COM", PasswordHash = "Password", SecurityStamp = Guid.NewGuid().ToString()},
            new User { Id = 3, IsCompany=true,  CompanyName = "UAB „Tikra įmonė“", CompanyCode = "123456", UserName = "company@example.com", NormalizedUserName = "COMPANY@EXAMPLE.COM", Email = "company@example.com", EmailConfirmed = true, NormalizedEmail = "COMPANY@EXAMPLE.COM", PasswordHash = "Password", SecurityStamp = Guid.NewGuid().ToString()},
            new User { Id = 4, IsCompany=true,  CompanyName = "UAB „Ne įmonė“", CompanyCode = "567891", UserName = "company5@example.com", NormalizedUserName = "COMPANY5@EXAMPLE.COM", Email = "company5@example.com", EmailConfirmed = false, NormalizedEmail = "COMPANY5@EXAMPLE.COM", PasswordHash = "Password", SecurityStamp = Guid.NewGuid().ToString()},
            new User { Id = 5, IsCompany=true,  CompanyName = "UAB „Vieša įmonė“", CompanyCode = "678912", UserName = "company6@example.com", NormalizedUserName = "COMPANY6@EXAMPLE.COM", Email = "company6@example.com", EmailConfirmed = false, NormalizedEmail = "COMPANY6@EXAMPLE.COM", PasswordHash = "Password", SecurityStamp = Guid.NewGuid().ToString()},
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

        public static List<RegistrationRequest> RegistrationRequests = new List<RegistrationRequest>
        {
            new RegistrationRequest {Id = 1, Email = "company2@example.com", Password = "Password123!", CompanyName="UAB Pelninga Įmonė", CompanyCode="234567", DateCreated = DateTime.Now},
            new RegistrationRequest {Id = 2, Email = "company3@example.com", Password = "Password245!", CompanyName="UAB Nuostolinga Įmonė", CompanyCode="345678", DateCreated = DateTime.Now},
            new RegistrationRequest {Id = 3, Email = "company4@example.com", Password = "Password345!", CompanyName="UAB Godi Įmonė", CompanyCode="456789", DateCreated = DateTime.Now},
        };

        public static List<PasswordResetToken> PasswordResetTokens = new List<PasswordResetToken> {
            new PasswordResetToken {Id = 1, UserId = 1, Token = "passwordResetToken1"},
            new PasswordResetToken {Id = 2, UserId = 2, Token = "passwordResetToken2"},
        };

        public static List<EmailConfirmationToken> EmailConfirmationTokens = new List<EmailConfirmationToken>
        {
            new EmailConfirmationToken { Id = 1, UserId = 4, Token = "confirmationToken1"},
            new EmailConfirmationToken { Id = 2, UserId = 5, Token = "confirmationToken2"}
        };

        public static List<EmailChangeToken> EmailChangeTokens = new List<EmailChangeToken>
        {
            new EmailChangeToken {Id = 1, UserId = 2, NewEmail="gamer@example.com", Token="emailChangeToken1"},
            new EmailChangeToken {Id = 2, UserId = 4, NewEmail="owner@example.com", Token="emailChangeToken2"},
        };

        public static List<Conversation> Conversations = new List<Conversation>
        {
            new Conversation {Id = 1, UserConsoleId = 3},
            new Conversation {Id = 2, UserConsoleId = 5},
            new Conversation {Id = 3, BorrowingId = 1},
            new Conversation {Id = 4, BorrowingId = 3},
        };

        public static List<Message> Messages = new List<Message>
        {
            new Message {Id = 1, ConversationId = 1, FromAdmin = true, Text="Siunčiu sutartį", DateSent=DateTime.Now},
            new Message {Id = 2, ConversationId = 1, FromAdmin = false, Text="Siunčiu pasirašytą sutartį", DateSent=DateTime.Now},
            new Message {Id = 3, ConversationId = 1, FromAdmin = true, Text="Sutartis gauta", DateSent=DateTime.Now},
            new Message {Id = 4, ConversationId = 2, FromAdmin = true, Text="Siunčiu sutartį", DateSent=DateTime.Now},
            new Message {Id = 5, ConversationId = 2, FromAdmin = false, Text="Siunčiu pasirašytą sutartį", DateSent=DateTime.Now},
            new Message {Id = 6, ConversationId = 2, FromAdmin = true, Text="Sutartis gauta", DateSent=DateTime.Now},
            new Message {Id = 7, ConversationId = 3, FromAdmin = true, Text="Siunčiu sutartį", DateSent=DateTime.Now},
            new Message {Id = 8, ConversationId = 3, FromAdmin = false, Text="Siunčiu pasirašytą sutartį", DateSent=DateTime.Now},
            new Message {Id = 9, ConversationId = 3, FromAdmin = true, Text="Sutartis gauta", DateSent=DateTime.Now},
            new Message {Id = 10, ConversationId = 4, FromAdmin = true, Text="Siunčiu sutartį", DateSent=DateTime.Now},
            new Message {Id = 11, ConversationId = 4, FromAdmin = false, Text="Siunčiu pasirašytą sutartį", DateSent=DateTime.Now},
            new Message {Id = 12, ConversationId = 4, FromAdmin = true, Text="Sutartis gauta", DateSent=DateTime.Now},
        };

        public static List<MessageFile> MessageFiles = new List<MessageFile>
        {
            new MessageFile {Id = 1, MessageId = 1, Description="File description", Path="fgbdygj5yuynf53hbza3", Name = "Skolintojo_Sutartis.pdf"},
            new MessageFile {Id = 2, MessageId = 2, Description="File description", Path="45u9bbvajdbviabibasf", Name = "Pasirašyta_Sutartis.pdf"},
            new MessageFile {Id = 3, MessageId = 4, Description="File description", Path="u2nawonaw803t9jedaii", Name = "Skolintojo_Sutartis.pdf"},
            new MessageFile {Id = 4, MessageId = 5, Description="File description", Path="afaimwrniq0innkik3wh", Name = "Pasirašyta_Sutartis.pdf"},
            new MessageFile {Id = 5, MessageId = 7, Description="File description", Path="u3budvnaoefbaivybiab", Name = "Besiskolinančiojo_Sutartis.pdf"},
            new MessageFile {Id = 6, MessageId = 8, Description="File description", Path="nahbahksvbasuvbaybvh", Name = "Pasirašyta_Sutartis.pdf"},
            new MessageFile {Id = 7, MessageId = 10, Description="File description", Path="35vsrgd5ydfbsbsfbsgf", Name = "Besiskolinančiojo_Sutartis.pdf"},
            new MessageFile {Id = 8, MessageId = 11, Description="File description", Path="eud87dvnsd9vdv9dvns8", Name = "Pasirašyta_Sutartis.pdf"},
        };


        public static List<Borrowing> Borrowings = new List<Borrowing> {
            new Borrowing { Id = 1, UserId = 3, Status = BorrowingStatus.ACTIVE },
            new Borrowing { Id = 2, UserId = 3, Status = BorrowingStatus.PENDING },
            new Borrowing { Id = 3, UserId = 4, Status = BorrowingStatus.ACTIVE },
            new Borrowing { Id = 4, UserId = 4, Status = BorrowingStatus.PENDING }
        };

        public static List<Image> Images = new List<Image> {
            new Image { Id = 1, Path = "gvoktfyvobny0j2umvtt", Name = "1.jpeg", Description = "", ConsoleId = 1, UserConsoleId = null },
            new Image { Id = 2, Path = "owdqtg9fodxw8ubvmavs", Name = "2.jpeg", Description = "", ConsoleId = 1, UserConsoleId = null },
            new Image { Id = 3, Path = "d0sid8ixuhrgcx4melbs", Name = "3.jpeg", Description = "", ConsoleId = 1, UserConsoleId = null },
            new Image { Id = 4, Path = "tmhke7yuza1v9zhourmc", Name = "P5.webp", Description = "", ConsoleId = 2, UserConsoleId = null },
            new Image { Id = 5, Path = "hjzaamg3uuftq1vsgctt", Name = "P5.jpeg", Description = "", ConsoleId = 2, UserConsoleId = null },
            new Image { Id = 6, Path = "dnj7iggkdupgcl9wdide", Name = "P5.png", Description = "", ConsoleId = 2, UserConsoleId = null },
            new Image { Id = 7, Path = "eklt5qq9dci76xad67v7", Name = "SW1.jpeg", Description = "", ConsoleId = 3, UserConsoleId = null },
            new Image { Id = 8, Path = "o8m586cvirnmfxeqh4v6", Name = "SW2.jpeg", Description = "", ConsoleId = 3, UserConsoleId = null },
            new Image { Id = 9, Path = "arvmwbr83mqwshwyruqk", Name = "SW3.jpeg", Description = "", ConsoleId = 3, UserConsoleId = null },
            new Image { Id = 10, Path = "qumlht6wjyklm6htfh3y", Name = "1.jpg", Description = "", ConsoleId = null, UserConsoleId = 1 },
            new Image { Id = 11, Path = "qfx54nyuroewwoch473n", Name = "2.jpg", Description = "", ConsoleId = null, UserConsoleId = 1 },
            new Image { Id = 12, Path = "t7dse874m3o03syqkyix", Name = "1.jpg", Description = "", ConsoleId = null, UserConsoleId = 2 },
            new Image { Id = 13, Path = "ippxzihzs29akhhxd9xn", Name = "2.jpg", Description = "", ConsoleId = null, UserConsoleId = 2 },
            new Image { Id = 14, Path = "cbjujhwymkya4egvowlp", Name = "1.jpg", Description = "", ConsoleId = null, UserConsoleId = 3 },
            new Image { Id = 15, Path = "bnplshybz8d3vf3gr3no", Name = "2.jpg", Description = "", ConsoleId = null, UserConsoleId = 3 },
            new Image { Id = 16, Path = "wj88di2k7pmft2isdlh8", Name = "3.jpg", Description = "", ConsoleId = null, UserConsoleId = 4 },
            new Image { Id = 17, Path = "kevpxg8wzjv0jw6gkg4c", Name = "4.jpg", Description = "", ConsoleId = null, UserConsoleId = 4 },
            new Image { Id = 18, Path = "ffl3aw9hv5005dfdjfpz", Name = "5.jpg", Description = "", ConsoleId = null, UserConsoleId = 5 },
            new Image { Id = 19, Path = "lhmgyhueweqa7fkp1stl", Name = "6.jpg", Description = "", ConsoleId = null, UserConsoleId = 5 },
            new Image { Id = 20, Path = "imrldxkmtmr5yeztklsq", Name = "7.jpg", Description = "", ConsoleId = null, UserConsoleId = 6 },
            new Image { Id = 21, Path = "z3fxlitvox9pcnx6qifj", Name = "8.jpg", Description = "", ConsoleId = null, UserConsoleId = 6 },
            new Image { Id = 22, Path = "gsvfexdltber03kb0sbq", Name = "9.webp", Description = "", ConsoleId = null, UserConsoleId = 7 },
            new Image { Id = 23, Path = "zu9h4aokymxxgogz3rpk", Name = "10.webp", Description = "", ConsoleId = null, UserConsoleId = 7 },
            new Image { Id = 24, Path = "jqaiq8wmqqecnlzwojax", Name = "1.jpg", Description = "", ConsoleId = null, UserConsoleId = 8 },
            new Image { Id = 25, Path = "rgh3wb7beikhyt2emacl", Name = "2.jpg", Description = "", ConsoleId = null, UserConsoleId = 8 },
            new Image { Id = 26, Path = "nzc2jbogrqhimi2kinx7", Name = "3.png", Description = "", ConsoleId = null, UserConsoleId = 9 },
            new Image { Id = 27, Path = "oiwhljjpuankybss3fxf", Name = "4.jpg", Description = "", ConsoleId = null, UserConsoleId = 9 },
            new Image { Id = 28, Path = "michvakylcmespl2jpkp", Name = "5.jpg", Description = "", ConsoleId = null, UserConsoleId = 10 },
            new Image { Id = 29, Path = "yhmkdali6lct87ujbxtw", Name = "6.jpg", Description = "", ConsoleId = null, UserConsoleId = 10 },
            new Image { Id = 30, Path = "of7vv1zj6b3h4xcxoogo", Name = "7.jpg", Description = "", ConsoleId = null, UserConsoleId = 11 },
            new Image { Id = 31, Path = "busosz6xgrk1pl09satt", Name = "8.webp", Description = "", ConsoleId = null, UserConsoleId = 11 },
            new Image { Id = 32, Path = "wpclmzpnijnbejwcu7v2", Name = "9.webp", Description = "", ConsoleId = null, UserConsoleId = 12 },
            new Image { Id = 33, Path = "suowvu9mwqucv0lhp2y2", Name = "10.jpg", Description = "", ConsoleId = null, UserConsoleId = 12 },
            new Image { Id = 34, Path = "kymuodazpxcondhpc2kb", Name = "1.jpg", Description = "", ConsoleId = null, UserConsoleId = 13 },
            new Image { Id = 35, Path = "hwcpytri3igfalqovqbb", Name = "2.jpg", Description = "", ConsoleId = null, UserConsoleId = 13 },
            new Image { Id = 36, Path = "jryjdpjyhlfpxybhvgb2", Name = "3.webp", Description = "", ConsoleId = null, UserConsoleId = 14 },
            new Image { Id = 37, Path = "orvzpdw4izqin1eir2ro", Name = "4.jpg", Description = "", ConsoleId = null, UserConsoleId = 14 },
            new Image { Id = 38, Path = "ak1f7z5qm7gqtcapo3x6", Name = "5.webp", Description = "", ConsoleId = null, UserConsoleId = 15 },
            new Image { Id = 39, Path = "d1laps4gd9ybshzwr8mm", Name = "6.jpg", Description = "", ConsoleId = null, UserConsoleId = 15 },
            new Image { Id = 40, Path = "cn76jlujcjkrvlspid3s", Name = "7.jpg", Description = "", ConsoleId = null, UserConsoleId = 16 },
            new Image { Id = 41, Path = "n3xkh0olersoqm8pvyxk", Name = "8.jpg", Description = "", ConsoleId = null, UserConsoleId = 16 },
            new Image { Id = 42, Path = "xy9djpejp8dqt013iwqc", Name = "9.webp", Description = "", ConsoleId = null, UserConsoleId = 17 },
            new Image { Id = 43, Path = "mgrat1qyaks74xvuqfxl", Name = "10.jpg", Description = "", ConsoleId = null, UserConsoleId = 17 }
        };

        public static List<Data.Models.Console> Consoles = new List<Data.Models.Console> {
            new Data.Models.Console { Id = 1, Name = "Xbox One", Description = "Microsoft Xbox One", DailyPrice = 8, Images = Images.Where(x=>x.ConsoleId == 1).ToList() },
            new Data.Models.Console { Id = 2, Name = "Playstation 5", Description = "Sony Playstation 5", DailyPrice = 9, Images = Images.Where(x=>x.ConsoleId == 2).ToList() },
            new Data.Models.Console { Id = 3, Name = "Switch", Description = "Nintendo Switch", DailyPrice = 7, Images = Images.Where(x=>x.ConsoleId == 3).ToList() },
            new Data.Models.Console { Id = 4, Name = "PSP", Description = "Playstation Portable", DailyPrice = 5, Images = Images.Where(x=>x.ConsoleId == 4).ToList()}
        };

        public static List<UserConsole> UserConsoles = new List<UserConsole> {
            new UserConsole { Id = 1, UserId = 2, ConsoleId = 1, Amount = 1, Accessories = "1 controller", ConsoleStatus = UserConsoleStatus.UNCONFIRMED, Images = Images.Where(x=>x.UserConsoleId == 1).ToList(),},
            new UserConsole { Id = 2, UserId = 2, ConsoleId = 2, Amount = 1, Accessories = "2 controllers", ConsoleStatus = UserConsoleStatus.AT_PLATFORM, Images = Images.Where(x=>x.UserConsoleId == 2).ToList() },
            new UserConsole { Id = 3, UserId = 2, ConsoleId = 3, Amount = 3, Accessories = "Switch valdikliai", ConsoleStatus = UserConsoleStatus.AT_PLATFORM, Images = Images.Where(x=>x.UserConsoleId == 3).ToList() },
            new UserConsole { Id = 4, UserId = 2, ConsoleId = 3, Amount = 2, Accessories = "Switch valdikliai", BorrowingId = 1, ConsoleStatus = UserConsoleStatus.AT_LENDER, Images = Images.Where(x=>x.UserConsoleId == 4).ToList() },
            new UserConsole { Id = 5, UserId = 2, ConsoleId = 3, Amount = 1, Accessories = "Switch valdikliai", BorrowingId = 1, ConsoleStatus = UserConsoleStatus.AWAITING_TERMINATION_BY_LENDER, Images = Images.Where(x=>x.UserConsoleId == 5).ToList() },
            new UserConsole { Id = 6, UserId = 2, ConsoleId = 3, Amount = 3, Accessories = "Switch valdikliai", BorrowingId = 1, ConsoleStatus = UserConsoleStatus.UNCONFIRMED, Images = Images.Where(x=>x.UserConsoleId == 6).ToList() },
            new UserConsole { Id = 7, UserId = 2, ConsoleId = 3, Amount = 1, Accessories = "Switch valdikliai", ConsoleStatus = UserConsoleStatus.UNCONFIRMED, Images = Images.Where(x=>x.UserConsoleId == 7).ToList() },
            new UserConsole { Id = 8, UserId = 2, ConsoleId = 1, Amount = 1, Accessories = "2 pulteliai", ConsoleStatus = UserConsoleStatus.AT_PLATFORM, Images = Images.Where(x=>x.UserConsoleId == 8).ToList() },
            new UserConsole { Id = 9, UserId = 2, ConsoleId = 1, Amount = 1, Accessories = "3 pulteliai", BorrowingId = 2, ConsoleStatus = UserConsoleStatus.RESERVED, Images = Images.Where(x=>x.UserConsoleId == 9).ToList() },
            new UserConsole { Id = 10, UserId = 2, ConsoleId = 1, Amount = 2, Accessories = "6 pulteliai", BorrowingId = 2, ConsoleStatus = UserConsoleStatus.RESERVED, Images = Images.Where(x=>x.UserConsoleId == 10).ToList() },
            new UserConsole { Id = 11, UserId = 2, ConsoleId = 1, Amount = 3, Accessories = "9 pulteliai", BorrowingId = 2, ConsoleStatus = UserConsoleStatus.RESERVED, Images = Images.Where(x=>x.UserConsoleId == 11).ToList() },
            new UserConsole { Id = 12, UserId = 2, ConsoleId = 1, Amount = 1, Accessories = "4 pulteliai", ConsoleStatus = UserConsoleStatus.AT_PLATFORM, Images = Images.Where(x=>x.UserConsoleId == 12).ToList() },
            new UserConsole { Id = 13, UserId = 2, ConsoleId = 2, Amount = 1, Accessories = "2 pulteliai", ConsoleStatus = UserConsoleStatus.UNCONFIRMED, Images = Images.Where(x=>x.UserConsoleId == 13).ToList() },
            new UserConsole { Id = 14, UserId = 2, ConsoleId = 2, Amount = 2, Accessories = "4 pulteliai", ConsoleStatus = UserConsoleStatus.AT_PLATFORM, Images = Images.Where(x=>x.UserConsoleId == 14).ToList() },
            new UserConsole { Id = 15, UserId = 2, ConsoleId = 2, Amount = 3, Accessories = "6 pulteliai", BorrowingId = 3, ConsoleStatus = UserConsoleStatus.AT_LENDER, Images = Images.Where(x=>x.UserConsoleId == 15).ToList() },
            new UserConsole { Id = 16, UserId = 2, ConsoleId = 2, Amount = 2, Accessories = "6 pulteliai", BorrowingId = 3, ConsoleStatus = UserConsoleStatus.AWAITING_TERMINATION_BY_BORROWER, Images = Images.Where(x=>x.UserConsoleId == 16).ToList() },
            new UserConsole { Id = 17, UserId = 2, ConsoleId = 2, Amount = 1, Accessories = "3 pulteliai", BorrowingId = 3, ConsoleStatus = UserConsoleStatus.AT_LENDER, Images = Images.Where(x=>x.UserConsoleId == 17).ToList() }
        };
    }
}
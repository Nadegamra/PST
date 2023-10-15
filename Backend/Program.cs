using Backend.Data;
using Backend.Data.Models;
using Backend.Handlers;
using Backend.Properties;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

{
    var services = builder.Services;

    services.AddControllers();
    services.AddCors(options =>
    {
        options.AddDefaultPolicy(policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
    });

    // Add swagger
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    // Add authentication
    services.AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    });
    services.AddIdentity<User, IdentityRole<int>>()
        .AddEntityFrameworkStores<AppDbContext>()
        .AddTokenProvider<DataProtectorTokenProvider<User>>(TokenOptions.DefaultProvider);
    services.Configure<IdentityOptions>(options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireUppercase = true;
        options.Password.RequiredLength = 8;

        options.User.AllowedUserNameCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^*_-~.";
        options.User.RequireUniqueEmail = true;
    });

    builder.Services.ConfigureApplicationCookie(options =>
    {
        options.Cookie.SameSite = SameSiteMode.None;
    });

    // Add database
    string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    services.AddDbContext<AppDbContext>(options =>
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

    // Add Configuration
    services.Configure<CloudinaryConfig>(builder.Configuration.GetSection("ImageStorage"));
    services.Configure<SmtpConfig>(builder.Configuration.GetSection("Smtp"));

    // Add services to the DI container.
    services.AddTransient<UserManager<User>>();
    services.AddTransient<RoleManager<IdentityRole<int>>>();
    services.AddTransient<AuthHandler>();
    services.AddTransient<ConsolesHandler>();
    services.AddTransient<UsersHandler>();
    services.AddTransient<FilesHandler>();
    services.AddTransient<UserConsolesHandler>();
    services.AddTransient<BorrowingsHandler>();
    services.AddTransient<ChatsHandler>();
    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

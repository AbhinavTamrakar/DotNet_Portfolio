using Microsoft.AspNetCore.Identity;
using Portfolio.Infrastructure;
using Portfolio.Infrastructure.Identity;
using Portfolio.Infrastructure.Persistence;
using Portfolio.Infrastructure.Persistence.Seed;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException(
        "Connection string 'DefaultConnection' is not configured. Set it with: " +
        "dotnet user-secrets set \"ConnectionStrings:DefaultConnection\" \"<connection-string>\" " +
        "--project backend/Portfolio.Api");
}

// EF Core (MySQL) + Identity persistence.
builder.Services.AddInfrastructure(connectionString);

builder.Services
    .AddIdentityCore<ApplicationUser>(options =>
    {
        options.Password.RequiredLength = 12;
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = true;

        options.Lockout.AllowedForNewUsers = true;
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);

        options.User.RequireUniqueEmail = true;
    })
    .AddEntityFrameworkStores<PortfolioDbContext>()
    .AddDefaultTokenProviders();

builder.Services
    .AddAuthentication(IdentityConstants.ApplicationScheme)
    .AddIdentityCookies();

builder.Services.AddAuthorization();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Creates the administrator only when Admin:Email and Admin:Password are configured.
// Credentials are supplied through user-secrets or environment variables, never source code.
using (var scope = app.Services.CreateScope())
{
    await AdminSeeder.SeedAsync(scope.ServiceProvider, app.Configuration, app.Logger);
}

app.Run();

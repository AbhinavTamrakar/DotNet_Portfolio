using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Portfolio.Infrastructure.Identity;

namespace Portfolio.Infrastructure.Persistence.Seed;

/// <summary>
/// Creates the single administrator account from configuration. Credentials are never
/// hardcoded: they must come from user-secrets (local development) or environment
/// variables / the host secret store (deployment).
/// </summary>
public static class AdminSeeder
{
    public const string EmailConfigurationKey = "Admin:Email";

    public const string PasswordConfigurationKey = "Admin:Password";

    public static async Task SeedAsync(
        IServiceProvider services,
        IConfiguration configuration,
        ILogger logger)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);
        ArgumentNullException.ThrowIfNull(logger);

        var email = configuration[EmailConfigurationKey]?.Trim();
        var password = configuration[PasswordConfigurationKey];

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            logger.LogInformation(
                "Administrator seeding skipped. Set {EmailKey} and {PasswordKey} to create the admin account.",
                EmailConfigurationKey,
                PasswordConfigurationKey);
            return;
        }

        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

        var existing = await userManager.FindByEmailAsync(email);
        if (existing is not null)
        {
            logger.LogInformation("Administrator account {Email} already exists; nothing to seed.", email);
            return;
        }

        var user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            var errors = string.Join(
                "; ",
                result.Errors.Select(error => $"{error.Code}: {error.Description}"));

            throw new InvalidOperationException(
                $"Failed to create the administrator account '{email}'. {errors}");
        }

        logger.LogInformation("Administrator account {Email} created.", email);
    }
}

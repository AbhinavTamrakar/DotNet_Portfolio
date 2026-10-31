using Microsoft.AspNetCore.Identity;

namespace Portfolio.Infrastructure.Identity;

/// <summary>
/// The portfolio administrator account. Extends the standard ASP.NET Core Identity user
/// so that additional owner-specific properties can be added without changing the schema
/// strategy later.
/// </summary>
public class ApplicationUser : IdentityUser
{
}

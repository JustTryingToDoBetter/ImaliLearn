using Microsoft.AspNetCore.Identity;

namespace ImaliLearn.Infrastructure.Identity;

/// <summary>
/// Application user entity extending ASP.NET Identity.
/// Domain-specific user properties can be added here.
/// </summary>
public class ApplicationUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastLoginAt { get; set; }
}

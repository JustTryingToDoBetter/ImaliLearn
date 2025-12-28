using System.ComponentModel.DataAnnotations;

namespace ImaliLearn.Application.DTOs.Auth;

public record RegisterRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; init; } = string.Empty;

    [Required]
    [MinLength(8)]
    public string Password { get; init; } = string.Empty;

    [Required]
    [Compare(nameof(Password))]
    public string ConfirmPassword { get; init; } = string.Empty;

    public string? FirstName { get; init; }
    public string? LastName { get; init; }
}

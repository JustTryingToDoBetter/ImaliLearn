namespace ImaliLearn.Domain.DTOs.Auth;

public record AuthResponse
{
    public bool Succeeded { get; init; }
    public string? Token { get; init; }
    public string? RefreshToken { get; init; }
    public DateTime? ExpiresAt { get; init; }
    public string? Email { get; init; }
    public IEnumerable<string> Errors { get; init; } = [];
}

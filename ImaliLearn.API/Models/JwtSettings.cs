namespace ImaliLearn.API.Models;

public class JwtSettings
{
    public string Issuer { get; set; } = string.Empty; // the token issuer
    public string Audience { get; set; } = string.Empty; // the token audience
    public string SecretKey { get; set; } = string.Empty; // the secret key used for signing
    public int ExpiryMinutes { get; set; } // token expiry time in minutes
}
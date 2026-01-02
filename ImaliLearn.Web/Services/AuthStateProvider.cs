using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace ImaliLearn.Web.Services;

public class AuthStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorage;
    private readonly ClaimsPrincipal _anonymous = new(new ClaimsIdentity());
    private const string TokenKey = "authToken";

    public AuthStateProvider(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var token = await _localStorage.GetItemAsync<string>(TokenKey);

            if (string.IsNullOrWhiteSpace(token))
                return new AuthenticationState(_anonymous);

            var claims = ParseClaimsFromJwt(token);
            
            if (claims == null || IsTokenExpired(claims))
            {
                await _localStorage.RemoveItemAsync(TokenKey);
                return new AuthenticationState(_anonymous);
            }

            var identity = new ClaimsIdentity(claims, "jwt");
            var user = new ClaimsPrincipal(identity);
            
            return new AuthenticationState(user);
        }
        catch
        {
            return new AuthenticationState(_anonymous);
        }
    }

    public void NotifyUserAuthentication(string token)
    {
        var claims = ParseClaimsFromJwt(token);
        var identity = new ClaimsIdentity(claims, "jwt");
        var user = new ClaimsPrincipal(identity);
        
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    public void NotifyUserLogout()
    {
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
    }

    private static IEnumerable<Claim>? ParseClaimsFromJwt(string token)
    {
        try
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            return jwtToken.Claims;
        }
        catch
        {
            return null;
        }
    }

    private static bool IsTokenExpired(IEnumerable<Claim> claims)
    {
        var expClaim = claims.FirstOrDefault(c => c.Type == "exp");
        
        if (expClaim == null || !long.TryParse(expClaim.Value, out var expUnix))
            return true;

        var expDate = DateTimeOffset.FromUnixTimeSeconds(expUnix);
        return expDate <= DateTimeOffset.UtcNow;
    }
}

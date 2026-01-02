using System.Net.Http.Json;
using Blazored.LocalStorage;

namespace ImaliLearn.Web.Services;

public class AuthService
{
    private readonly HttpClient _http;
    private readonly ILocalStorageService _localStorage;
    private readonly AuthStateProvider _authStateProvider;

    private const string TokenKey = "authToken";
    private const string RefreshTokenKey = "refreshToken";

    public AuthService(
        HttpClient http,
        ILocalStorageService localStorage,
        AuthStateProvider authStateProvider)
    {
        _http = http;
        _localStorage = localStorage;
        _authStateProvider = authStateProvider;
    }

    public async Task<(bool Success, string? Error)> LoginAsync(string email, string password)
    {
        var response = await _http.PostAsJsonAsync("api/auth/login", new { email, password });

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadFromJsonAsync<ErrorResponse>();
            return (false, error?.Error ?? "Login failed");
        }

        var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
        
        if (result?.AccessToken != null)
        {
            await _localStorage.SetItemAsync(TokenKey, result.AccessToken);
            
            if (result.RefreshToken != null)
                await _localStorage.SetItemAsync(RefreshTokenKey, result.RefreshToken);
            
            _authStateProvider.NotifyUserAuthentication(result.AccessToken);
            return (true, null);
        }

        return (false, "Invalid response from server");
    }

    public async Task<(bool Success, string? Error)> RegisterAsync(string email, string password)
    {
        var response = await _http.PostAsJsonAsync("api/auth/register", new { email, password });

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadFromJsonAsync<ErrorResponse>();
            return (false, error?.Error ?? "Registration failed");
        }

        return (true, null);
    }

    public async Task LogoutAsync()
    {
        var refreshToken = await _localStorage.GetItemAsync<string>(RefreshTokenKey);
        
        if (!string.IsNullOrEmpty(refreshToken))
        {
            try
            {
                await _http.PostAsJsonAsync("api/auth/logout", refreshToken);
            }
            catch
            {
                // Ignore logout API errors
            }
        }

        await _localStorage.RemoveItemAsync(TokenKey);
        await _localStorage.RemoveItemAsync(RefreshTokenKey);
        _authStateProvider.NotifyUserLogout();
    }

    public async Task<string?> GetTokenAsync()
    {
        return await _localStorage.GetItemAsync<string>(TokenKey);
    }

    private record LoginResponse(string? AccessToken, string? RefreshToken);
    private record ErrorResponse(string? Error);
}

using ImaliLearn.API.Models;
using ImaliLearn.Application.Auth;
using Microsoft.AspNetCore.Mvc;

namespace ImaliLearn.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly RegisterUserService _register;
    private readonly LoginUserService _login;

    public AuthController(
        RegisterUserService register,
        LoginUserService login)
    {
        _register = register;
        _login = login;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var result = await _register.HandleAsync(request.Email, request.Password);
        return result.IsSuccess
            ? Ok(new { userId = result.Value })
            : Conflict(new { error = result.Error });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var result = await _login.HandleAsync(request.Email, request.Password);
        return result.IsSuccess
            ? Ok(new { accessToken = result.Value })
            : Unauthorized(new { error = result.Error });
    }
}
using ImaliLearn.API.Models;
using ImaliLearn.Application.Auth;
using Microsoft.AspNetCore.Mvc;
using ImaliLearn.API.Security;
using Microsoft.AspNetCore.Authorization;


namespace ImaliLearn.API.Controllers;

[ApiExplorerSettings(GroupName = "v1")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly RegisterUserService _register;
    private readonly LoginUserService _login;

    private readonly RefreshTokenService _refreshService;
    private readonly LogoutService _logout;
    private readonly LogoutAllSessionsService _logoutAll;

    public AuthController(
        RegisterUserService register,
        LoginUserService login,
        RefreshTokenService refreshService,
        LogoutService logout,
        LogoutAllSessionsService logoutAll)
    {
        _register = register;
        _login = login;
        _refreshService = refreshService;
        _logout = logout;
        _logoutAll = logoutAll;
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

    [HttpPost("refresh")]
public async Task<IActionResult> Refresh([FromBody] string refreshToken)
{
    var result = await _refreshService.HandleAsync(refreshToken);

    return result.IsSuccess
        ? Ok(new { accessToken = result.Value })
        : Unauthorized(new { error = result.Error });
}

[Authorize]
[HttpPost("logout")]
public async Task<IActionResult> Logout([FromBody] string refreshToken)
{
    await _logout.HandleAsync(refreshToken);
    return NoContent();
}

[Authorize]
[HttpPost("logout-all")]
public async Task<IActionResult> LogoutAll()
{
    var userId = User.GetUserId();
    await _logoutAll.HandleAsync(userId);
    return NoContent();
}
}
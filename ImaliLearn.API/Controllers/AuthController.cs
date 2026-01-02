// ==========================================
// 3️⃣ AUTH CONTROLLER
// ==========================================

// 📍 API/Controllers/AuthController.cs

using ImaliLearn.Application.Auth;
using Microsoft.AspNetCore.Mvc;

namespace ImaliLearn.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly JwtTokenService _jwtService;

    public AuthController(JwtTokenService jwtService)
    {
        _jwtService = jwtService;
    }

    // TEMP login endpoint (replace with real auth later)
    [HttpPost("login")]
    public IActionResult Login()
    {
        var userId = Guid.NewGuid(); // placeholder
        var email = "user@example.com";

        var token = _jwtService.GenerateToken(userId, email);

        return Ok(new { accessToken = token });
    }
}

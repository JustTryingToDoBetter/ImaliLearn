using ImaliLearn.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImaliLearn.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = Roles.Admin)]
public class AdminController : ControllerBase
{
    /// <summary>
    /// Admin-only dashboard.
    /// Requires Admin role.
    /// </summary>
    [HttpGet("dashboard")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public IActionResult GetAdminDashboard()
    {
        return Ok(new
        {
            Message = "Welcome to the Admin Dashboard!",
            Timestamp = DateTime.UtcNow,
            Features = new[]
            {
                "User Management",
                "Content Management",
                "Analytics"
            }
        });
    }

    /// <summary>
    /// Get all users (Admin only).
    /// </summary>
    [HttpGet("users")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public IActionResult GetUsers()
    {
        // Placeholder - would normally fetch from database
        return Ok(new
        {
            Message = "User list endpoint - Admin only",
            Note = "Implement user listing service"
        });
    }
}

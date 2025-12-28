using ImaliLearn.Application.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImaliLearn.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = Roles.Educator)]
public class EducatorController : ControllerBase
{
    /// <summary>
    /// Educator dashboard.
    /// Requires Educator role.
    /// </summary>
    [HttpGet("dashboard")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public IActionResult GetEducatorDashboard()
    {
        return Ok(new
        {
            Message = "Welcome to the Educator Dashboard!",
            Timestamp = DateTime.UtcNow,
            Features = new[]
            {
                "Create Lessons",
                "Manage Content",
                "View Student Progress"
            }
        });
    }

    /// <summary>
    /// Create a new lesson (Educator only).
    /// </summary>
    [HttpPost("lessons")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public IActionResult CreateLesson()
    {
        // Placeholder - would normally create a lesson
        return Created("", new
        {
            Message = "Lesson creation endpoint - Educator only",
            Note = "Implement lesson creation service"
        });
    }
}

using Microsoft.AspNetCore.Mvc;
/*
 Why this matters

Verifies pipeline, hosting, and routing

Used by load balancers in production
 */
namespace ImaliLearn.API.Controllers;
[ApiController]
[Route("api/health")]
public class  HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult GetHealthStatus()
    {
        return Ok(new { status = "Healthy", timestamp = DateTime.UtcNow });
    }
}


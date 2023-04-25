using Microsoft.AspNetCore.Mvc;

namespace WarrenSoftware.TodoApp.Modules.Health.Http;

[ApiController]
public class HealthController : ControllerBase
{
    [HttpGet("api/health")]
    public async Task<IActionResult> Get()
    {
        return Ok($"{DateTimeOffset.Now} - 😎");
    }
}

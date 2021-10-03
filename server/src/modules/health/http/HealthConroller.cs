using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WarrenSoftware.TodoApp.Core.Infrastructure;

namespace WarrenSoftware.TodoApp.Modules.Health.Http
{
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet("api/health")]
        public async Task<IActionResult> Get()
        {
            return Ok($"{DateTimeOffset.Now} - 😎");
        }
    }
}
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WarrenSoftware.TodoApp.Core.Infrastructure;

namespace WarrenSoftware.TodoApp.Modules.Health.Http
{
    [ApiController]
    public class HealthController : ControllerBase
    {
        private readonly HiLoGenerator _hilo;

        public HealthController(HiLoGenerator hilo)
        {
            _hilo = hilo;
        }

        [HttpGet("api/health")]
        public async Task<IActionResult> Get()
        {
            var low = await _hilo.NextAsync(CancellationToken.None);
            return Ok(low);
        }
    }
}
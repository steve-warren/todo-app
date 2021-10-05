using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WarrenSoftware.TodoApp.Core.Infrastructure;

namespace WarrenSoftware.TodoApp.Modules.Users.Http
{
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("api/user/session")]
        [AllowAnonymous]
        public async Task AuthenticateUserAsync([FromBody] AuthenticateUserModel model)
        {
            var authenticateCommand = new AuthenticateCommand
            {
                UserName = model.UserName,
                PlaintextPassword = model.Password
            };

            var userId = await _mediator.Send(authenticateCommand);

            if (userId != -1)
            {
                var userProfileQuery = new GetUserProfileQuery
                {
                    UserId = userId,
                    OutputStream = Response.BodyWriter.AsStream()
                };

                Response.Headers["Location"] = "/app.html#/tasks";
                Response.ContentType = "application/json";
                await _mediator.Send(userProfileQuery);
            }

            else
                Response.StatusCode = StatusCodes.Status401Unauthorized;
        }

        [HttpDelete("api/user/session")]
        [AllowAnonymous]
        public async Task SignOutUserAsync()
        {
            var command = new LogOutUserCommand
            {
                UserId = User.GetUserId()
            };

            await _mediator.Send(command);

            Response.Headers["Location"] = "/";
        }

        [HttpGet("api/user/profile")]
        public async Task GetUserProfileAsync()
        {
            var userProfileQuery = new GetUserProfileQuery
            {
                UserId = User.GetUserId(),
                OutputStream = Response.BodyWriter.AsStream()
            };

            await _mediator.Send(userProfileQuery);
        }

        [HttpPost("api/user/session/token")]
        public async Task<IActionResult> SetCsrfTokenAsync()
        {
            var command = new GetAndSetCsrfToken
            {
                UserId = User.GetUserId()
            };

            var token = await _mediator.Send(command);

            return Ok(new { Token = token });
        }
    }
}
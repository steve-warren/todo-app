using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WarrenSoftware.TodoApp.Core.Infrastructure;

namespace WarrenSoftware.TodoApp.Modules.Users.Http
{
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPut("api/user/auth")]
        public async Task AuthenticateUser([FromBody] AuthenticateUserModel model)
        {
            var authenticateCommand = new AuthenticateCommand
            {
                Email = model.Email,
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

                Response.ContentType = "application/json";
                await _mediator.Send(userProfileQuery);
            }

            else
                Response.StatusCode = StatusCodes.Status401Unauthorized;
        }

        [HttpGet("api/user/profile")]
        public async Task GetUserProfileAsync()
        {
            var userProfileQuery = new GetUserProfileQuery
            {
                UserId = 500,
                OutputStream = Response.BodyWriter.AsStream()
            };

            await _mediator.Send(userProfileQuery);
        }
    }
}
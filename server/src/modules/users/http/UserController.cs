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

            var authenticationSuccessful = await _mediator.Send(authenticateCommand);

            if (authenticationSuccessful)
            {
                var getProfileQuery = new GetUserProfileQuery
                {
                    Email = model.Email
                };

                Response.StatusCode = StatusCodes.Status200OK;

                await _mediator.Send(getProfileQuery);
            }

            else
            {
                Response.StatusCode = StatusCodes.Status401Unauthorized;
            }
        }
    }
}
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarrenSoftware.TodoApp.Core.Infrastructure;

namespace WarrenSoftware.TodoApp.Modules.Todo.Http
{
    [ApiController]

    public class TodoListController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TodoListController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("api/todo/lists")]
        public async Task GetAllTodoListsAsync()
        {
            Response.ContentType = "application/json";

            var query = new GetAllListsQuery
            {
                OwnerId = 1,//User.GetUserId(),
                OutputStream = Response.Body
            };

            await _mediator.Send(query);
        }

        [HttpPost("api/todo/lists")]
        public async Task<IActionResult> CreateListAsync()
        {
            var command = new CreateListCommand
            {
                Name = "foo",
                OwnerId = 1
            };

            var id = await _mediator.Send(command);

            return Ok(id);
        }
    }
}
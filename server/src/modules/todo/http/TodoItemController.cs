using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarrenSoftware.TodoApp.Core.Infrastructure;

namespace WarrenSoftware.TodoApp.Modules.Todo.Http
{
    [ApiController]
    [Authorize]
    public class TodoItemController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TodoItemController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("api/todo/items")]
        public async Task GetAllTodoItemsAsync([FromQuery] int listId)
        {
            Response.ContentType = "application/json";

            var query = new GetItemsQuery
            {
                OwnerId = User.GetUserId(),
                ListId = listId,
                OutputStream = Response.Body
            };

            await _mediator.Send(query);
        }
    }
}
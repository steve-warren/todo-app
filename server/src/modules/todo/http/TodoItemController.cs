using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarrenSoftware.TodoApp.Core.Infrastructure;

namespace WarrenSoftware.TodoApp.Modules.Todo.Http
{
    [ApiController]
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
                OwnerId = 1,//User.GetUserId(),
                ListId = listId,
                OutputStream = Response.Body
            };

            await _mediator.Send(query);
        }

        [HttpPost("api/todo/items")]
        public async Task<IActionResult> AddTodoItemAsync([FromBody] AddTodoItemViewModel model)
        {
            var command = new AddItemCommand
            {
                ListId = model.ListId,
                Name = model.Name,
                Notes = model.Notes,
                Priority = model.Priority,
                Reminder = model.Reminder,
                OwnerId = 1
            };

            var id = await _mediator.Send(command);

            return Ok(id);
        }
    }
}
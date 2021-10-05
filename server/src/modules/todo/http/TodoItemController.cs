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
        public async Task GetAllTodoItemsAsync([FromQuery] int? listId)
        {
            Response.ContentType = "application/json";

            IRequest query;
            
            if (listId.HasValue)
            {
                query = new GetItemsForListQuery
                {
                    OwnerId = User.GetUserId(),
                    ListId = listId.Value,
                    OutputStream = Response.Body
                };
            }

            else
            {
                query = new GetAllItemsQuery
                {
                    OwnerId = User.GetUserId(),
                    OutputStream = Response.Body
                };
            }

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
                OwnerId = User.GetUserId()
            };

            var id = await _mediator.Send(command);

            return Ok(id);
        }
    }
}
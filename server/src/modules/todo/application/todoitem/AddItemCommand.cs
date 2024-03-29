using MediatR;
using WarrenSoftware.TodoApp.Core.Domain;
using WarrenSoftware.TodoApp.Modules.Todo.Domain;
using WarrenSoftware.TodoApp.Modules.Todo.Infrastructure;

namespace WarrenSoftware.TodoApp.Modules.Todo;

public class AddItemCommand : IRequest<int>
{
    public int ListId { get; init; }
    public int OwnerId { get; init; }
    public string Name { get; init; } = "";
    public string Priority { get; init; } = "";
    public DateTimeOffset? Reminder { get; init; }
    public string Notes { get; init; } = "";
}

public class AddItemHandler : IRequestHandler<AddItemCommand, int>
{
    private readonly ITodoListRepository _lists;
    private readonly ITodoItemRepository _items;
    private readonly IIdentityService _identityService;
    private readonly ITodoUnitOfWork _uow;

    public AddItemHandler(ITodoListRepository lists, ITodoItemRepository items, IIdentityService identityService, ITodoUnitOfWork uow)
    {
        _lists = lists;
        _items = items;
        _identityService = identityService;
        _uow = uow;
    }

    public async Task<int> Handle(AddItemCommand request, CancellationToken cancellationToken)
    {
        var list = await _lists.FindByIdAsync(request.ListId);

        if (list is null) return -1;

        var id = await _identityService.NextIdAsync(cancellationToken);
        var item = new TodoItem(name: request.Name, ownerId: list.OwnerId, listId: request.ListId, priority: TodoItemPriority.Parse(request.Priority), id: id, notes: request.Notes, reminder: request.Reminder);

        _items.Add(item);

        await _uow.SaveChangesAsync(cancellationToken);

        return item.Id;
    }
}
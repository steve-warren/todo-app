using MediatR;
using WarrenSoftware.TodoApp.Core.Domain;
using WarrenSoftware.TodoApp.Modules.Todo.Domain;
using WarrenSoftware.TodoApp.Modules.Todo.Infrastructure;

namespace WarrenSoftware.TodoApp.Modules.Todo;
public class EditItemCommand : IRequest
{
    public int ItemId { get; init; }
    public int ListId { get; init; }
    public string Name { get; init; } = "";
    public string Priority { get; init; } = "";
    public DateTimeOffset? Reminder { get; init; }
    public string Notes { get; init; } = "";
}

public class EditItemHandler : IRequestHandler<EditItemCommand>
{
    private readonly ITodoItemRepository _items;
    private readonly ITodoListRepository _lists;
    private readonly ITodoUnitOfWork _uow;

    public EditItemHandler(ITodoItemRepository items, ITodoListRepository lists, ITodoUnitOfWork uow)
    {
        _items = items;
        _lists = lists;
        _uow = uow;
    }

    public async Task Handle(EditItemCommand request, CancellationToken cancellationToken)
    {
        var list = await _lists.FindByIdAsync(request.ListId);

        if (list is null) return;

        var item = await _items.FindByIdAsync(request.ItemId);

        if (item is null) return;

        item.Rename(request.Name);
        item.WriteNotes(request.Notes);
        item.ChangePriority(TodoItemPriority.Parse(request.Priority));
        item.SetReminder(request.Reminder);
        item.Relocate(request.ListId);

        await _uow.SaveChangesAsync(cancellationToken);

        return;
    }
}

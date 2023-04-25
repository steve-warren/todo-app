using MediatR;
using WarrenSoftware.TodoApp.Modules.Todo.Domain;
using WarrenSoftware.TodoApp.Modules.Todo.Infrastructure;

namespace WarrenSoftware.TodoApp.Modules.Todo;

public class RenameListCommand : IRequest
{
    public int ListId { get; init; }
    public string NewName { get; init; } = "";
}

public class RenameListHandler : IRequestHandler<RenameListCommand>
{
    private readonly ITodoListRepository _repository;
    private readonly ITodoUnitOfWork _uow;

    public RenameListHandler(ITodoListRepository repository, ITodoUnitOfWork uow)
    {
        _repository = repository;
        _uow = uow;
    }

    public async Task Handle(RenameListCommand request, CancellationToken cancellationToken)
    {
        var list = await _repository.FindByIdAsync(request.ListId);

        if (list is null) return;

        list.Rename(request.NewName);

        await _uow.SaveChangesAsync(cancellationToken);
    }
}

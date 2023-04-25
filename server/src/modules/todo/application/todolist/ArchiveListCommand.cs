using MediatR;
using WarrenSoftware.TodoApp.Modules.Todo.Domain;
using WarrenSoftware.TodoApp.Modules.Todo.Infrastructure;

namespace WarrenSoftware.TodoApp.Modules.Todo;

public class ArchiveListCommand : IRequest
{
    public int OwnerId { get; init; }
    public int ListId { get; init; }
}

public class ArchiveListHandler : IRequestHandler<ArchiveListCommand>
{
    private readonly ITodoListRepository _repository;
    private readonly ITodoUnitOfWork _uow;

    public ArchiveListHandler(ITodoListRepository repository, ITodoUnitOfWork uow)
    {
        _repository = repository;
        _uow = uow;
    }

    public async Task Handle(ArchiveListCommand request, CancellationToken cancellationToken)
    {
        var list = await _repository.FindByIdAsync(request.ListId);

        if (list is null) return;

        list.Archive();

        await _uow.SaveChangesAsync(cancellationToken);
    }
}

using MediatR;
using WarrenSoftware.TodoApp.Core.Domain;
using WarrenSoftware.TodoApp.Modules.Todo.Domain;
using WarrenSoftware.TodoApp.Modules.Todo.Infrastructure;

namespace WarrenSoftware.TodoApp.Modules.Todo;

public class CreateListCommand : IRequest<int>
{
    public int OwnerId { get; init; }
    public string Name { get; init; } = "";
}

public class CreateListHandler : IRequestHandler<CreateListCommand, int>
{
    private readonly ITodoListRepository _repository;
    private readonly IIdentityService _identityService;
    private readonly ITodoUnitOfWork _uow;

    public CreateListHandler(ITodoListRepository repository, IIdentityService identityService, ITodoUnitOfWork uow)
    {
        _repository = repository;
        _identityService = identityService;
        _uow = uow;
    }

    public async Task<int> Handle(CreateListCommand request, CancellationToken cancellationToken)
    {
        var id = await _identityService.NextIdAsync(cancellationToken);
        var list = new TodoList(request.Name, id, request.OwnerId);
        _repository.Add(list);

        await _uow.SaveChangesAsync(cancellationToken);

        return list.Id;
    }
}

using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WarrenSoftware.TodoApp.Core.Domain;
using WarrenSoftware.TodoApp.Modules.Todo.Domain;

namespace WarrenSoftware.TodoApp.Modules.Todo
{
    public class CreateListCommand : IRequest<int>
    {
        public string Name { get; init; } = "";
    }

    public class CreateListHandler : IRequestHandler<CreateListCommand, int>
    {
        private readonly IActiveTodoListRepository _repository;
        private readonly ITodoListIdentityService _identityService;
        private readonly IUnitOfWork _uow;

        public CreateListHandler(IActiveTodoListRepository repository, ITodoListIdentityService identityService,  IUnitOfWork uow)
        {
            _repository = repository;
            _identityService = identityService;
            _uow = uow;
        }

        public async Task<int> Handle(CreateListCommand request, CancellationToken cancellationToken)
        {
            var id = await _identityService.NextIdAsync(cancellationToken);
            var list = new ActiveTodoList(request.Name, id);
            _repository.Add(list);

            await _uow.SaveChangesAsync(cancellationToken);

            return list.Id;
        }
    }
}
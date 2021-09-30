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
        private readonly IUnitOfWork _uow;

        public CreateListHandler(IActiveTodoListRepository repository, IUnitOfWork uow)
        {
            _repository = repository;
            _uow = uow;
        }

        public async Task<int> Handle(CreateListCommand request, CancellationToken cancellationToken)
        {
            var list = new ActiveTodoList(request.Name);
            await _repository.AddAsync(list);
            await _uow.SaveChangesAsync(cancellationToken);

            return list.Id;
        }
    }
}
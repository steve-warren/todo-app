using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WarrenSoftware.TodoApp.Core.Domain;
using WarrenSoftware.TodoApp.Modules.Todo.Domain;

namespace WarrenSoftware.TodoApp.Modules.Todo
{
    public class RenameListCommand : IRequest
    {
        public int ListId { get; init; }
        public string NewName { get; init; } = "";
    }

    public class RenameListHandler : AsyncRequestHandler<RenameListCommand>
    {
        private readonly IActiveTodoListRepository _repository;
        private readonly IUnitOfWork _uow;

        public RenameListHandler(IActiveTodoListRepository repository, IUnitOfWork uow)
        {
            _repository = repository;
            _uow = uow;
        }

        protected override async Task Handle(RenameListCommand request, CancellationToken cancellationToken)
        {
            var list = await _repository.FindByIdAsync(request.ListId);

            if (list is null) return;
            
            list.Rename(request.NewName);

            await _uow.SaveChangesAsync(cancellationToken);
        }
    }
}
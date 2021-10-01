using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WarrenSoftware.TodoApp.Core.Domain;
using WarrenSoftware.TodoApp.Modules.Todo.Domain;

namespace WarrenSoftware.TodoApp.Modules.Todo
{
    public class RestoreItemCommand : IRequest
    {
        public int ItemId { get; init; }
    }

    public class RestoreItemHandler : AsyncRequestHandler<RestoreItemCommand>
    {
        private readonly IArchivedTodoItemRepository _repository;
        private readonly IUnitOfWork _uow;

        public RestoreItemHandler(IArchivedTodoItemRepository repository, IUnitOfWork uow)
        {
            _repository = repository;
            _uow = uow;
        }

        protected override async Task Handle(RestoreItemCommand request, CancellationToken cancellationToken)
        {
            var item = await _repository.FindByIdAsync(request.ItemId);

            if (item is null) return;
                        
            item.Restore();

            await _uow.SaveChangesAsync(cancellationToken);
        }
    }
}
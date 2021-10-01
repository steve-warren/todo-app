using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WarrenSoftware.TodoApp.Core.Domain;
using WarrenSoftware.TodoApp.Modules.Todo.Domain;

namespace WarrenSoftware.TodoApp.Modules.Todo
{
    public class ArchiveItemCommand : IRequest
    {
        public int ItemId { get; init; }
    }

    public class ArchiveItemHandler : AsyncRequestHandler<ArchiveItemCommand>
    {
        private readonly INotArchivedTodoItemRepository _repository;
        private readonly IUnitOfWork _uow;

        public ArchiveItemHandler(INotArchivedTodoItemRepository repository, IUnitOfWork uow)
        {
            _repository = repository;
            _uow = uow;
        }

        protected override async Task Handle(ArchiveItemCommand request, CancellationToken cancellationToken)
        {
            var item = await _repository.FindByIdAsync(request.ItemId);

            if (item is null) return;
                        
            item.Archive();

            await _uow.SaveChangesAsync(cancellationToken);
        }
    }
}
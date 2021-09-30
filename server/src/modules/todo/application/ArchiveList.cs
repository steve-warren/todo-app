using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WarrenSoftware.TodoApp.Core.Domain;
using WarrenSoftware.TodoApp.Modules.Todo.Domain;

namespace WarrenSoftware.TodoApp.Modules.Todo
{
    public class ArchiveListCommand : IRequest
    {
        public int ListId { get; init; }
    }

    public class ArchiveListHandler : AsyncRequestHandler<ArchiveListCommand>
    {
        private readonly IActiveTodoListRepository _repository;
        private readonly IUnitOfWork _uow;

        public ArchiveListHandler(IActiveTodoListRepository repository, IUnitOfWork uow)
        {
            _repository = repository;
            _uow = uow;
        }

        protected override async Task Handle(ArchiveListCommand request, CancellationToken cancellationToken)
        {
            var list = await _repository.FindByIdAsync(request.ListId);

            if (list is null) return;
                        
            list.Archive();

            await _uow.SaveChangesAsync(cancellationToken);
        }
    }
}
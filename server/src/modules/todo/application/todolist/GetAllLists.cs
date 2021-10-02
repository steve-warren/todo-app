using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Data.SqlClient;
using WarrenSoftware.TodoApp.Core.Infrastructure;

namespace WarrenSoftware.TodoApp.Modules.Todo
{
    public class GetAllListsQuery : IRequest
    {
        public int OwnerId { get; init; }
        public Stream Stream { get; init; } = Stream.Null;
    }

    public class GetAllListsHandler : IRequestHandler<GetAllListsQuery>
    {
        private readonly SqlConnection _connection;

        public GetAllListsHandler(SqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<Unit> Handle(GetAllListsQuery request, CancellationToken cancellationToken)
        {
            using var command = new SqlCommand(@"
                SELECT
                    Id,
                    Name,
                    Items
                FROM
                    TodoLists
                WHERE
                    OwnerId = @OwnerId
                ORDER BY
                    Name ASC
            ", _connection);

            command.Parameters.AddWithValue("@OwnerId", request.OwnerId);

            await _connection.OpenAsync(cancellationToken);
            await command.StreamUtf8TextAsync(request.Stream, cancellationToken);

            return Unit.Value;
        }
    }
}
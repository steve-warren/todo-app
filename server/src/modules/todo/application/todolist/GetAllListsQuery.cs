using MediatR;
using Microsoft.Data.SqlClient;
using WarrenSoftware.TodoApp.Core.Domain;
using WarrenSoftware.TodoApp.Core.Infrastructure;

namespace WarrenSoftware.TodoApp.Modules.Todo;

public class GetAllListsQuery : IRequest
{
    public int OwnerId { get; init; }
    public Stream OutputStream { get; init; } = Stream.Null;
}

public class GetAllListsHandler : IRequestHandler<GetAllListsQuery>
{
    private readonly SqlConnection _connection;

    public GetAllListsHandler(SqlConnection connection)
    {
        _connection = connection;
    }

    public async Task Handle(GetAllListsQuery request, CancellationToken cancellationToken)
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
                        AND
                    ArchiveState = @ArchiveState
                ORDER BY
                    Name ASC
                FOR JSON PATH
            ", _connection);

        command.Parameters.AddWithValue("@OwnerId", request.OwnerId);
        command.Parameters.AddWithValue("@ArchiveState", ArchiveState.NotArchived.Name);

        await _connection.OpenAsync(cancellationToken);
        await command.StreamUtf8TextAsync(request.OutputStream, cancellationToken);
    }
}

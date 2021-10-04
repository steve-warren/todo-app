using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Data.SqlClient;
using WarrenSoftware.TodoApp.Core.Domain;
using WarrenSoftware.TodoApp.Core.Infrastructure;

namespace WarrenSoftware.TodoApp.Modules.Users
{
    public class GetUserProfileQuery : IRequest
    {
        public int UserId { get; init; }
        public Stream OutputStream { get; init; } = Stream.Null;
    }

    public class GetUserProfileHandler : IRequestHandler<GetUserProfileQuery>
    {
        private readonly SqlConnection _connection;

        public GetUserProfileHandler(SqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<Unit> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            using var command = new SqlCommand(@"
                SELECT
                    UserName,
                    Email,
                    FirstName,
                    LastName
                FROM
                    Users
                WHERE
                    Id = @UserId
                FOR JSON AUTO, WITHOUT_ARRAY_WRAPPER
            ", _connection);

            command.Parameters.AddWithValue("@UserId", request.UserId);

            await _connection.OpenAsync(cancellationToken);
            await command.StreamUtf8TextAsync(request.OutputStream, cancellationToken);

            return Unit.Value;
        }
    }
}
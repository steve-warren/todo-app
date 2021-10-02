
using System.Buffers;
using System.Data;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace WarrenSoftware.TodoApp.Core.Infrastructure
{
    public static class SqlClientExtensions
    {
        // SQL Server chunk size
        private const int BUFFER_SIZE = 2033;

        public static async Task StreamUtf8TextAsync(this SqlCommand command, Stream stream, CancellationToken cancellationToken = default)
        {
            using var reader = command.ExecuteReader(CommandBehavior.SequentialAccess);

            await using var writer = new StreamWriter(stream: stream, encoding: Encoding.UTF8, bufferSize: BUFFER_SIZE, leaveOpen: true);

            var buffer = ArrayPool<char>.Shared.Rent(BUFFER_SIZE);

            try
            {
                while (await reader.ReadAsync(cancellationToken))
                {
                    using var textReader = reader.GetTextReader(0);
                    var charsRead = 0;

                    while(true)
                    {
                        charsRead = await textReader.ReadAsync(buffer, 0, BUFFER_SIZE);

                        if (charsRead == 0) break;

                        await writer.WriteAsync(buffer, 0, charsRead);
                    }
                }
            }

            finally
            {
                ArrayPool<char>.Shared.Return(buffer);
            }
        }
    }
}
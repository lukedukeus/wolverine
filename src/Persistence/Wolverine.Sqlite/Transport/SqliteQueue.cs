using Microsoft.Extensions.Logging;

using Wolverine.Configuration;
using Wolverine.Runtime;
using Wolverine.Transports;
using Wolverine.Transports.Sending;

namespace Wolverine.Sqlite.Transport;

public class SqliteQueue : Endpoint, IBrokerQueue, IDatabaseBackedEndpoint
{
    public SqliteQueue(string name, SqliteTransport parent, EndpointRole role = EndpointRole.Application) : base(new Uri($"{SqliteTransport.ProtocolName}://{name}"), role)
    {

    }

    public override ValueTask<IListener> BuildListenerAsync(IWolverineRuntime runtime, IReceiver receiver)
    {
        throw new NotImplementedException();
    }

    public ValueTask<bool> CheckAsync()
    {
        throw new NotImplementedException();
    }

    public ValueTask<Dictionary<string, string>> GetAttributesAsync()
    {
        throw new NotImplementedException();
    }

    public ValueTask PurgeAsync(ILogger logger)
    {
        throw new NotImplementedException();
    }

    public Task ScheduleRetryAsync(Envelope envelope, CancellationToken cancellation)
    {
        throw new NotImplementedException();
    }

    public ValueTask SetupAsync(ILogger logger)
    {
        throw new NotImplementedException();
    }

    public ValueTask TeardownAsync(ILogger logger)
    {
        throw new NotImplementedException();
    }

    protected override ISender CreateSender(IWolverineRuntime runtime)
    {
        throw new NotImplementedException();
    }
}
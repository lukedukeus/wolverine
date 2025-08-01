using JasperFx.Core;

using Wolverine.Runtime;
using Wolverine.Transports;

namespace Wolverine.Sqlite.Transport;

public class SqliteTransport : BrokerTransport<SqliteQueue>
{
    public const string ProtocolName = "sqlite";

    public SqliteTransport() : base(ProtocolName, "Sqlite Transport")
    {
    }

    public override Uri ResourceUri => new Uri("sqlite-transport://");

    public LightweightCache<string, SqliteQueue> Queues { get; } = new LightweightCache<string, SqliteQueue>();

    public override ValueTask ConnectAsync(IWolverineRuntime runtime)
    {
        throw new NotImplementedException();
    }

    public override IEnumerable<PropertyColumn> DiagnosticColumns()
    {
        throw new NotImplementedException();
    }

    protected override IEnumerable<SqliteQueue> endpoints()
    {
        throw new NotImplementedException();
    }

    protected override SqliteQueue findEndpointByUri(Uri uri)
    {
        throw new NotImplementedException();
    }
}
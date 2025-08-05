using JasperFx;
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
        AutoProvision = AutoProvision || runtime.Options.AutoBuildMessageStorageOnStartup != AutoCreate.None;

        var storage = runtime.Storage as SqliteMessageStore;

        Storage = storage ?? throw new InvalidOperationException(
            "The Sql Server Transport can only be used if the message persistence is also Sql Server backed");

        Settings = storage.Settings;

        // This is de facto a little environment test
        await using var conn = new SqlConnection(Settings.ConnectionString);
        await conn.OpenAsync();
        await conn.CloseAsync();
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
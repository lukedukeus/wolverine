using JasperFx;
using JasperFx.Core;
using JasperFx.MultiTenancy;

using Wolverine.Sqlite.Transport;

namespace Wolverine.Sqlite;

public interface ISqliteBackedPersistence
{
    /// <summary>
    /// Enable and configure the Sqlite backed messaging transport
    /// </summary>
    /// <param name="configure">Optional configuration of the Sqlite backed messaging transport</param>
    /// <returns></returns>
    ISqliteBackedPersistence EnableMessageTransport(Action<SqlitePersistenceExpression>? configure = null);

    /// <summary>
    /// By default, Wolverine takes the AutoCreate settings from JasperFxOptions, but
    /// you can override the application default for just the Sqlite backed queues
    /// and envelope storage tables
    /// </summary>
    /// <param name="autoCreate"></param>
    /// <returns></returns>
    ISqliteBackedPersistence OverrideAutoCreateResources(AutoCreate autoCreate);

    /// <summary>
    /// Override the database advisory lock number that Wolverine uses to grant temporary, exclusive
    /// access to execute scheduled messages for this application. This is normally done by using a deterministic
    /// hash of the Wolverine envelope schema name, but you *might* need to disambiguate different applications
    /// accessing the exact same Sqlite database
    /// </summary>
    /// <param name="lockId"></param>
    /// <returns></returns>
    ISqliteBackedPersistence OverrideScheduledJobLockId(int lockId);

    /// <summary>
    /// Should Wolverine provision Sqlite command queues for this Wolverine application? The default is true,
    /// but these queues are unnecessary if using an external broker for Wolverine command queues -- and the Wolverine team does recommend
    /// using external brokers for command queues when that's possible
    /// </summary>
    /// <param name="enabled"></param>
    /// <returns></returns>
    ISqliteBackedPersistence EnableCommandQueues(bool enabled);
}

/// <summary>
///     Activates the Sql Server backed message persistence
/// </summary>
internal class SqliteBackedPersistence : IWolverineExtension, ISqliteBackedPersistence
{
    public string? ConnectionString { get; set; }

    // This needs to be an override, and we use JasperFxOptions first!
    public AutoCreate AutoCreate { get; set; } = AutoCreate.CreateOrUpdate;

    /// <summary>
    ///     Is this database exposing command queues?
    /// </summary>
    public bool CommandQueuesEnabled { get; set; } = true;

    private int _scheduledJobLockId = 0;

    public ITenantedSource<string>? ConnectionStringTenancy { get; set; }

    // This would be an override
    public int ScheduledJobLockId
    {
        get
        {
            if (_scheduledJobLockId > 0) return _scheduledJobLockId;

            return $"scheduled-jobs".GetDeterministicHashCode();
        }
        set
        {
            _scheduledJobLockId = value;
        }
    }

    public void Configure(WolverineOptions options)
    {
        if (ConnectionString.IsEmpty())
        {
            throw new InvalidOperationException("The Sqlite backed persistence needs to at least have either a connection string");
        }

        // TODO: not sure what else we need to do here
    }

    public ISqliteBackedPersistence EnableCommandQueues(bool enabled)
    {
        CommandQueuesEnabled = enabled;
        return this;
    }

    public ISqliteBackedPersistence EnableMessageTransport(Action<SqlitePersistenceExpression>? configure = null)
    {
        throw new NotImplementedException();
    }

    public ISqliteBackedPersistence OverrideAutoCreateResources(AutoCreate autoCreate)
    {
        AutoCreate = autoCreate;
        return this;
    }

    public ISqliteBackedPersistence OverrideScheduledJobLockId(int lockId)
    {
        _scheduledJobLockId = lockId;
        return this;
    }
}
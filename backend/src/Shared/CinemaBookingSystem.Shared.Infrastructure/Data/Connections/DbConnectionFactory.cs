using System.Data.Common;
using CinemaBookingSystem.Shared.Application.Abstractions.Data;
using Npgsql;

namespace CinemaBookingSystem.Shared.Infrastructure.Data.Connections;

internal sealed class DbConnectionFactory(NpgsqlDataSource dataSource) : IDbConnectionFactory
{
    public async ValueTask<DbConnection> OpenConnectionAsync()
    {
        return await dataSource.OpenConnectionAsync();
    }

    public DbConnection CreateConnection()
    {
        return dataSource.CreateConnection();
    }
}

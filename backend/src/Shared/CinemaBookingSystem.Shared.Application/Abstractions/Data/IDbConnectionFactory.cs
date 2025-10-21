using System.Data.Common;

namespace CinemaBookingSystem.Shared.Application.Abstractions.Data;

public interface IDbConnectionFactory
{
    ValueTask<DbConnection> OpenConnectionAsync();
    DbConnection CreateConnection();
}

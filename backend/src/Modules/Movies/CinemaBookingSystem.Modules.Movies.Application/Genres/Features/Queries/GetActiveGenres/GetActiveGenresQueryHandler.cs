using CinemaBookingSystem.Modules.Movies.Application.Genres.Common;
using CinemaBookingSystem.Shared.Application.Abstractions.Data;
using CinemaBookingSystem.Shared.Application.Messaging;
using CinemaBookingSystem.Shared.Domain.Common;
using Dapper;

namespace CinemaBookingSystem.Modules.Movies.Application.Features.Genres.Queries.GetActiveGenres;

public class GetActiveGenresQueryHandler : IQueryHandler<GetActiveGenresQuery, IReadOnlyList<GenreDto>>
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public GetActiveGenresQueryHandler(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<Result<IReadOnlyList<GenreDto>>> Handle(GetActiveGenresQuery request, CancellationToken cancellationToken)
    {
        var connection = await _dbConnectionFactory.OpenConnectionAsync();

        const string sql = $"""
            SELECT
                [g].[Id] AS [{nameof(GenreDto.Id)}],
                [g].[Name] AS [{nameof(GenreDto.Name)}],
                [g].[IsActive] AS [{nameof(GenreDto.IsActive)}],
                [g].[CreatedAt] AS [{nameof(GenreDto.CreatedAt)}],
                [g].[UpdatedAt] AS [{nameof(GenreDto.UpdatedAt)}]
            FROM [movies].[Genres] AS [g]
            WHERE [g].[IsActive] = 1
            ORDER BY [g].[Name]
            """;
            
        var genres = await connection.QueryAsync<GenreDto>(sql);

        return Result<IReadOnlyList<GenreDto>>.Success(genres.AsList());
    }
}

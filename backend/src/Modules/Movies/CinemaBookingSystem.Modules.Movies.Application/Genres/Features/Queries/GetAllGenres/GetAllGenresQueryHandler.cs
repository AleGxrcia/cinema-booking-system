using CinemaBookingSystem.Modules.Movies.Application.Genres.Common;
using CinemaBookingSystem.Shared.Application.Abstractions.Data;
using CinemaBookingSystem.Shared.Application.Common;
using CinemaBookingSystem.Shared.Application.Messaging;
using CinemaBookingSystem.Shared.Domain.Common;
using Dapper;

namespace CinemaBookingSystem.Modules.Movies.Application.Features.Genres.Queries.GetAllGenres;

public sealed class GetAllGenresQueryHandler : IQueryHandler<GetAllGenresQuery, PagedResult<GenreDto>>
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public GetAllGenresQueryHandler(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<Result<PagedResult<GenreDto>>> Handle(GetAllGenresQuery request, CancellationToken cancellationToken)
    {
        var connection = await _dbConnectionFactory.OpenConnectionAsync();

        const string countSql = """
            SELECT COUNT(*)
            FROM [movies].[Genres]
            """;

        var totalCount = await connection.ExecuteScalarAsync<int>(countSql);

        if (totalCount == 0)
        {
            return PagedResult<GenreDto>.Empty(request.PageNumber, request.PageSize);
        }

        const string sql = $"""
            SELECT
                [g].[Id] AS [{nameof(GenreDto.Id)}],
                [g].[Name] AS [{nameof(GenreDto.Name)}],
                [g].[IsActive] AS [{nameof(GenreDto.IsActive)}],
                [g].[CreatedAt] AS [{nameof(GenreDto.CreatedAt)}],
                [g].[UpdatedAt] AS [{nameof(GenreDto.UpdatedAt)}]
            FROM [movies].[Genres] AS [g]
            ORDER BY [g].[Name]
            OFFSET @Skip ROWS
            FETCH NEXT @Take ROWS ONLY
            """;

        var genres = await connection.QueryAsync<GenreDto>(
            sql,
            new
            {
                Skip = request.Skip,
                Take = request.Take
            });

        return PagedResult<GenreDto>.Success(
            genres.AsList(),
            request.PageNumber,
            request.PageSize,
            totalCount
        );
    }
}

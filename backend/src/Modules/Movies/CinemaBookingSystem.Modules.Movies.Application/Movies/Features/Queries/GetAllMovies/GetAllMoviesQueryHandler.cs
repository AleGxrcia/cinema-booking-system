using CinemaBookingSystem.Modules.Movies.Application.Movies.Dtos;
using CinemaBookingSystem.Shared.Application.Abstractions.Data;
using CinemaBookingSystem.Shared.Application.Common;
using CinemaBookingSystem.Shared.Application.Messaging;
using CinemaBookingSystem.Shared.Domain.Common;
using Dapper;

namespace CinemaBookingSystem.Modules.Movies.Application.Movies.Features.Queries.GetAllMovies;

public class GetAllMoviesQueryHandler : IQueryHandler<GetAllMoviesQuery, PagedResult<MovieDto>>
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public GetAllMoviesQueryHandler(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<Result<PagedResult<MovieDto>>> Handle(GetAllMoviesQuery query, CancellationToken cancellationToken)
    {
        var connection = await _dbConnectionFactory.OpenConnectionAsync();

        const string countSql = """
            SELECT COUNT(*)
            FROM [movies].[Movies]
            """;

        var totalCount = await connection.ExecuteScalarAsync<int>(countSql);

        if (totalCount == 0)
        {
            return PagedResult<MovieDto>.Empty(query.PageNumber, query.PageSize);
        }

        const string sql = $"""
            SELECT
                [m].[Id] AS [{nameof(MovieDto.Id)}],
                [m].[Title] AS [{nameof(MovieDto.Title)}],
                [m].[Description] AS [{nameof(MovieDto.Description)}],
                [m].[DurationInMinutes] AS [{nameof(MovieDto.DurationInMinutes)}],
                [m].[DurationFormatted] AS [{nameof(MovieDto.DurationFormatted)}],
                [m].[ReleaseYear] AS [{nameof(MovieDto.ReleaseYear)}],
                [m].[PosterUrl] AS [{nameof(MovieDto.PosterUrl)}],
                [m].[TrailerUrl] AS [{nameof(MovieDto.TrailerUrl)}],
                [m].[AgeRating] AS [{nameof(MovieDto.AgeRating)}],
                [m].[Status] AS [{nameof(MovieDto.Status)}],
                [m].[LanguageCode] AS [{nameof(MovieDto.LanguageCode)}],
                [m].[LanguageName] AS [{nameof(MovieDto.LanguageName)}],
                [m].[Country] AS [{nameof(MovieDto.Country)}],
                [m].[CreatedAt] AS [{nameof(MovieDto.CreatedAt)}],
                [m].[UpdatedAt] AS [{nameof(MovieDto.UpdatedAt)}]
            FROM [movies].[Movies] AS [m]
            ORDER BY [m].[Title]
            OFFSET @Skip ROWS
            FETCH NEXT @Take ROWS ONLY
            """;

        var movies = await connection.QueryAsync<MovieDto>(
            sql,
            new
            {
                Skip = query.Skip,
                Take = query.Take
            });

        return PagedResult<MovieDto>.Success(
            movies.AsList(),
            query.PageNumber,
            query.PageSize,
            totalCount);
    }
}

using CinemaBookingSystem.Modules.Movies.Application.Movies.Dtos;
using CinemaBookingSystem.Modules.Movies.Domain.Enums;
using CinemaBookingSystem.Shared.Application.Abstractions.Data;
using CinemaBookingSystem.Shared.Application.Common;
using CinemaBookingSystem.Shared.Domain.Common;
using Dapper;

namespace CinemaBookingSystem.Modules.Movies.Application.Movies.Features.Queries.GetMoviesComingSoon;

public class GetMoviesComingSoonQueryHandler
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public GetMoviesComingSoonQueryHandler(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<Result<PagedResult<MovieDto>>> Handle(GetMoviesComingSoonQuery query, CancellationToken cancellationToken)
    {
        var connection = await _dbConnectionFactory.OpenConnectionAsync();

        var currentYear = DateTime.UtcNow.Year;

        const string countSql = """
            SELECT COUNT(*)
            FROM [movies].[Movies] AS [m]
            WHERE [m].[Status] = @ComingSoonStatus
                AND [m].[ReleaseYear] >= @CurrentYear
            """;

        var totalCount = await connection.ExecuteScalarAsync<int>(
            countSql,
            new
            {
                ComingSoonStatus = (int)MovieStatus.ComingSoon,
                CurrentYear = currentYear
            });

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
            WHERE [m].[Status] = @ComingSoonStatus
                AND [m].[ReleaseYear] >= @CurrentYear
            ORDER BY [m].[ReleaseYear], [m].[Title]
            OFFSET @Skip ROWS
            FETCH NEXT @Take ROWS ONLY
            """;

        var movies = await connection.QueryAsync<MovieDto>(
            sql,
            new
            {
                ComingSoonStatus = (int)MovieStatus.ComingSoon,
                CurrentYear = currentYear,
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
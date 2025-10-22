using CinemaBookingSystem.Modules.Movies.Application.Movies.Dtos;
using CinemaBookingSystem.Shared.Application.Abstractions.Data;
using CinemaBookingSystem.Shared.Application.Common;
using CinemaBookingSystem.Shared.Domain.Common;
using Dapper;

namespace CinemaBookingSystem.Modules.Movies.Application.Movies.Features.Queries.SearchMovies;

public class SearchMoviesQueryHandler
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public SearchMoviesQueryHandler(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<Result<PagedResult<MovieDto>>> Handle(SearchMoviesQuery query, CancellationToken cancellationToken)
    {
        var connection = await _dbConnectionFactory.OpenConnectionAsync();

        var whereConditions = new List<string>();
        var parameters = new DynamicParameters();

        if (!string.IsNullOrWhiteSpace(query.SearchTerm))
        {
            whereConditions.Add("([m].[Title] LIKE @SearchTerm OR [m].[Description] LIKE @SearchTerm)");
            parameters.Add("SearchTerm", $"%{query.SearchTerm}%");
        }

        if (query.ReleaseYear.HasValue)
        {
            whereConditions.Add("[m].[ReleaseYear] = @ReleaseYear");
            parameters.Add("ReleaseYear", query.ReleaseYear.Value);
        }

        if (query.Status.HasValue)
        {
            whereConditions.Add("[m].[Status] = @Status");
            parameters.Add("Status", (int)query.Status.Value);
        }

        if (query.AgeRating.HasValue)
        {
            whereConditions.Add("[m].[AgeRating] = @AgeRating");
            parameters.Add("AgeRating", (int)query.AgeRating.Value);
        }

        if (!string.IsNullOrWhiteSpace(query.Country))
        {
            whereConditions.Add("[m].[Country] LIKE @Country");
            parameters.Add("Country", $"%{query.Country}%");
        }

        if (query.GenreId.HasValue)
        {
            whereConditions.Add("""
                EXISTS (
                    SELECT 1 
                    FROM [movies].[MovieGenres] AS [mg]
                    WHERE [mg].[MovieId] = [m].[Id] 
                    AND [mg].[GenreId] = @GenreId
                )
                """);
            parameters.Add("GenreId", query.GenreId.Value);
        }

        var whereClause = whereConditions.Any()
            ? $"WHERE {string.Join(" AND ", whereConditions)}"
            : string.Empty;

        var countSql = $"""
            SELECT COUNT(*)
            FROM [movies].[Movies] AS [m]
            {whereClause}
            """;

        var totalCount = await connection.ExecuteScalarAsync<int>(countSql, parameters);

        if (totalCount == 0)
        {
            return PagedResult<MovieDto>.Empty(query.PageNumber, query.PageSize);
        }

        parameters.Add("Skip", query.Skip);
        parameters.Add("Take", query.Take);

        var sql = $"""
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
            {whereClause}
            ORDER BY [m].[Title]
            OFFSET @Skip ROWS
            FETCH NEXT @Take ROWS ONLY
            """;

        var movies = await connection.QueryAsync<MovieDto>(sql, parameters);

        return PagedResult<MovieDto>.Success(
            movies.AsList(),
            query.PageNumber,
            query.PageSize,
            totalCount);
    }
}

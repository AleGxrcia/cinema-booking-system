using CinemaBookingSystem.Modules.Movies.Application.Movies.Dtos;
using CinemaBookingSystem.Modules.Movies.Domain.Errors;
using CinemaBookingSystem.Shared.Application.Abstractions.Data;
using CinemaBookingSystem.Shared.Domain.Common;
using Dapper;

namespace CinemaBookingSystem.Modules.Movies.Application.Movies.Features.Queries.GetMovieById;

public class GetMovieByIdQueryHandler
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public GetMovieByIdQueryHandler(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<Result<MovieDto>> Handle(GetMovieByIdQuery query, CancellationToken cancellationToken)
    {
        var connection = await _dbConnectionFactory.OpenConnectionAsync();

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
            WHERE [m].[Id] = @MovieId
            """;

        var movie = await connection.QuerySingleOrDefaultAsync<MovieDto>(
            sql,
            new { query.MovieId });

        return movie is null
            ? MovieErrors.NotFound(query.MovieId)
            : Result<MovieDto>.Success(movie);
    }
}

using CinemaBookingSystem.Modules.Movies.Domain.Errors;
using CinemaBookingSystem.Shared.Application.Abstractions.Data;
using CinemaBookingSystem.Shared.Domain.Common;
using Dapper;

namespace CinemaBookingSystem.Modules.Movies.Application.Movies.Features.Queries.GetMovieDetails;

public class GetMovieDetailsQueryHandler
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public GetMovieDetailsQueryHandler(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<Result<MovieDetailsDto>> Handle(GetMovieDetailsQuery query, CancellationToken cancellationToken)
    {
        var connection = await _dbConnectionFactory.OpenConnectionAsync();

        const string movieSql = $"""
            SELECT
                [m].[Id] AS [{nameof(MovieDetailsDto.Id)}],
                [m].[Title] AS [{nameof(MovieDetailsDto.Title)}],
                [m].[Description] AS [{nameof(MovieDetailsDto.Description)}],
                [m].[DurationInMinutes] AS [{nameof(MovieDetailsDto.DurationInMinutes)}],
                [m].[DurationFormatted] AS [{nameof(MovieDetailsDto.DurationFormatted)}],
                [m].[ReleaseYear] AS [{nameof(MovieDetailsDto.ReleaseYear)}],
                [m].[PosterUrl] AS [{nameof(MovieDetailsDto.PosterUrl)}],
                [m].[TrailerUrl] AS [{nameof(MovieDetailsDto.TrailerUrl)}],
                [m].[AgeRating] AS [{nameof(MovieDetailsDto.AgeRating)}],
                [m].[Status] AS [{nameof(MovieDetailsDto.Status)}],
                [m].[LanguageCode] AS [{nameof(MovieDetailsDto.LanguageCode)}],
                [m].[LanguageName] AS [{nameof(MovieDetailsDto.LanguageName)}],
                [m].[Country] AS [{nameof(MovieDetailsDto.Country)}],
                [m].[CreatedAt] AS [{nameof(MovieDetailsDto.CreatedAt)}],
                [m].[UpdatedAt] AS [{nameof(MovieDetailsDto.UpdatedAt)}]
            FROM [movies].[Movies] AS [m]
            WHERE [m].[Id] = @MovieId
            """;

        const string genresSql = $"""
            SELECT
                [g].[Id] AS [{nameof(MovieGenreDto.GenreId)}],
                [g].[Name] AS [{nameof(MovieGenreDto.GenreName)}]
            FROM [movies].[MovieGenres] AS [mg]
            INNER JOIN [movies].[Genres] AS [g] ON [mg].[GenreId] = [g].[Id]
            WHERE [mg].[MovieId] = @MovieId
            ORDER BY [g].[Name]
            """;

        const string castSql = $"""
            SELECT
                [mc].[Id] AS [{nameof(MovieCastDto.CastId)}],
                [mc].[PersonName] AS [{nameof(MovieCastDto.PersonName)}],
                [mc].[Role] AS [{nameof(MovieCastDto.Role)}],
                [mc].[Order] AS [{nameof(MovieCastDto.Order)}]
            FROM [movies].[MovieCast] AS [mc]
            WHERE [mc].[MovieId] = @MovieId
            ORDER BY [mc].[Order], [mc].[PersonName]
            """;

        var movie = await connection.QuerySingleOrDefaultAsync<MovieDetailsDto>(
            movieSql,
            new { query.MovieId });

        if (movie is null)
        {
            return MovieErrors.NotFound(query.MovieId);
        }

        var genres = await connection.QueryAsync<MovieGenreDto>(
            genresSql,
            new { query.MovieId });

        var cast = await connection.QueryAsync<MovieCastDto>(
            castSql,
            new { query.MovieId });

        var movieDetails = movie with
        {
            Genres = genres.AsList(),
            Cast = cast.AsList()
        };

        return Result<MovieDetailsDto>.Success(movieDetails);
    }
}

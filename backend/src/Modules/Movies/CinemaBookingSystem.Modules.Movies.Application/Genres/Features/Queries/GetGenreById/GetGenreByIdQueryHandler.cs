using CinemaBookingSystem.Modules.Movies.Application.Genres.Dtos;
using CinemaBookingSystem.Modules.Movies.Domain.Errors;
using CinemaBookingSystem.Shared.Application.Abstractions.Data;
using CinemaBookingSystem.Shared.Application.Messaging;
using CinemaBookingSystem.Shared.Domain.Common;
using Dapper;

namespace CinemaBookingSystem.Modules.Movies.Application.Genres.Features.Queries.GetGenreById;

public sealed class GetGenreByIdQueryHandler : IQueryHandler<GetGenreByIdQuery, GenreDto>
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public GetGenreByIdQueryHandler(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<Result<GenreDto>> Handle(GetGenreByIdQuery request, CancellationToken cancellationToken)
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
            WHERE [g].[Id] = @GenreId
            """;

        var genre = await connection.QueryFirstOrDefaultAsync<GenreDto>(
            sql,
            new { request.GenreId });

        return genre is null
            ? GenreErrors.NotFound(request.GenreId)
            : Result<GenreDto>.Success(genre);
    }
}

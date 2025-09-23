using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaBookingSystem.Modules.Movies.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "movies");

            migrationBuilder.CreateTable(
                name: "genres",
                schema: "movies",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_genres", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "movies",
                schema: "movies",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    release_year = table.Column<int>(type: "integer", nullable: false),
                    poster_url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    trailer_url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    age_rating = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    country = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    duration_minutes = table.Column<int>(type: "integer", nullable: false),
                    language_code = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    language_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    normalized_title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_movies", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "movie_casts",
                schema: "movies",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    movie_id = table.Column<Guid>(type: "uuid", nullable: false),
                    person_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    role = table.Column<int>(type: "integer", nullable: false),
                    order = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_movie_casts", x => x.id);
                    table.ForeignKey(
                        name: "fk_movie_casts_movies_movie_id",
                        column: x => x.movie_id,
                        principalSchema: "movies",
                        principalTable: "movies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "movie_genres",
                schema: "movies",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    movie_id = table.Column<Guid>(type: "uuid", nullable: false),
                    genre_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_movie_genres", x => x.id);
                    table.ForeignKey(
                        name: "fk_movie_genres_genres_genre_id",
                        column: x => x.genre_id,
                        principalSchema: "movies",
                        principalTable: "genres",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_movie_genres_movies_movie_id",
                        column: x => x.movie_id,
                        principalSchema: "movies",
                        principalTable: "movies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_genres_is_active",
                schema: "movies",
                table: "genres",
                column: "is_active");

            migrationBuilder.CreateIndex(
                name: "IX_genres_name_unique",
                schema: "movies",
                table: "genres",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_movie_casts_movie_id",
                schema: "movies",
                table: "movie_casts",
                column: "movie_id");

            migrationBuilder.CreateIndex(
                name: "IX_movie_casts_movie_order",
                schema: "movies",
                table: "movie_casts",
                columns: new[] { "movie_id", "order" });

            migrationBuilder.CreateIndex(
                name: "IX_movie_casts_person_name",
                schema: "movies",
                table: "movie_casts",
                column: "person_name");

            migrationBuilder.CreateIndex(
                name: "IX_movie_casts_role",
                schema: "movies",
                table: "movie_casts",
                column: "role");

            migrationBuilder.CreateIndex(
                name: "IX_movie_genres_genre_id",
                schema: "movies",
                table: "movie_genres",
                column: "genre_id");

            migrationBuilder.CreateIndex(
                name: "IX_movie_genres_movie_genre_unique",
                schema: "movies",
                table: "movie_genres",
                columns: new[] { "movie_id", "genre_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_movie_genres_movie_id",
                schema: "movies",
                table: "movie_genres",
                column: "movie_id");

            migrationBuilder.CreateIndex(
                name: "IX_movies_created_at",
                schema: "movies",
                table: "movies",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "IX_movies_release_year",
                schema: "movies",
                table: "movies",
                column: "release_year");

            migrationBuilder.CreateIndex(
                name: "IX_movies_status",
                schema: "movies",
                table: "movies",
                column: "status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "movie_casts",
                schema: "movies");

            migrationBuilder.DropTable(
                name: "movie_genres",
                schema: "movies");

            migrationBuilder.DropTable(
                name: "genres",
                schema: "movies");

            migrationBuilder.DropTable(
                name: "movies",
                schema: "movies");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hourly.GitService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "git_repository",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    ext_repository_id = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    @namespace = table.Column<string>(name: "namespace", type: "character varying(255)", maxLength: 255, nullable: false),
                    web_url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_git_repository", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user_read_model",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    git_email = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    git_username = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    git_access_token = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_read_model", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "git_commits",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    git_repository_id = table.Column<Guid>(type: "uuid", nullable: false),
                    ext_commit_id = table.Column<string>(type: "text", nullable: false),
                    ext_commit_short_id = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    comment = table.Column<string>(type: "text", nullable: true),
                    author_id = table.Column<Guid>(type: "uuid", nullable: false),
                    authored_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    web_url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    RepositoryId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_git_commits", x => x.id);
                    table.ForeignKey(
                        name: "FK_git_commits_git_repository_git_repository_id",
                        column: x => x.git_repository_id,
                        principalTable: "git_repository",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_git_commits_user_read_model_author_id",
                        column: x => x.author_id,
                        principalTable: "user_read_model",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_git_commits_author_id",
                table: "git_commits",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "IX_git_commits_git_repository_id",
                table: "git_commits",
                column: "git_repository_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "git_commits");

            migrationBuilder.DropTable(
                name: "git_repository");

            migrationBuilder.DropTable(
                name: "user_read_model");
        }
    }
}

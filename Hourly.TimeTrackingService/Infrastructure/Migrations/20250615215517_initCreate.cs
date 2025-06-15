using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hourly.TimeTrackingService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "git_repository_read_model",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    ext_repository_id = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    web_url = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_git_repository_read_model", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user_read_model",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    tvt_hour_balance = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_read_model", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "git_commit_read_model",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    author_id = table.Column<Guid>(type: "uuid", nullable: false),
                    authored_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    git_repository_id = table.Column<Guid>(type: "uuid", nullable: false),
                    ext_commit_id = table.Column<string>(type: "text", nullable: false),
                    ext_commit_short_id = table.Column<string>(type: "text", nullable: false),
                    title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    web_url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_git_commit_read_model", x => x.id);
                    table.ForeignKey(
                        name: "fk_git_commit_author",
                        column: x => x.author_id,
                        principalTable: "user_read_model",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_git_commit_repository",
                        column: x => x.git_repository_id,
                        principalTable: "git_repository_read_model",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_contract_read_model",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_contract_read_model", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_contract_read_model_user_read_model_user_id",
                        column: x => x.user_id,
                        principalTable: "user_read_model",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "work_session",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_contract_id = table.Column<Guid>(type: "uuid", nullable: false),
                    task_description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    start_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    break_time = table.Column<float>(type: "real", nullable: false),
                    factor = table.Column<float>(type: "real", nullable: false),
                    tvt_accrued_hours = table.Column<float>(type: "real", nullable: false),
                    tvt_used_hours = table.Column<float>(type: "real", nullable: false),
                    wbso = table.Column<bool>(type: "boolean", nullable: false),
                    locked = table.Column<bool>(type: "boolean", nullable: false),
                    other_remarks = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_work_session", x => x.id);
                    table.ForeignKey(
                        name: "FK_work_session_user_contract_read_model_user_contract_id",
                        column: x => x.user_contract_id,
                        principalTable: "user_contract_read_model",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "git_commit_work_sessions",
                columns: table => new
                {
                    git_commit_id = table.Column<Guid>(type: "uuid", nullable: false),
                    work_session_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_git_commit_work_sessions", x => new { x.git_commit_id, x.work_session_id });
                    table.ForeignKey(
                        name: "fk_wsgc_git_commit",
                        column: x => x.git_commit_id,
                        principalTable: "git_commit_read_model",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_wsgc_work_session",
                        column: x => x.work_session_id,
                        principalTable: "work_session",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_git_commit_read_model_author_id",
                table: "git_commit_read_model",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "IX_git_commit_read_model_git_repository_id",
                table: "git_commit_read_model",
                column: "git_repository_id");

            migrationBuilder.CreateIndex(
                name: "IX_git_commit_work_sessions_work_session_id",
                table: "git_commit_work_sessions",
                column: "work_session_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_contract_read_model_user_id",
                table: "user_contract_read_model",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_work_session_user_contract_id",
                table: "work_session",
                column: "user_contract_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "git_commit_work_sessions");

            migrationBuilder.DropTable(
                name: "git_commit_read_model");

            migrationBuilder.DropTable(
                name: "work_session");

            migrationBuilder.DropTable(
                name: "git_repository_read_model");

            migrationBuilder.DropTable(
                name: "user_contract_read_model");

            migrationBuilder.DropTable(
                name: "user_read_model");
        }
    }
}

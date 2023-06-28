using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FollowUp.Infra.Migrations
{
    /// <inheritdoc />
    public partial class _initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "followUp");

            migrationBuilder.CreateTable(
                name: "FollowUps",
                schema: "followUp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssistanceId = table.Column<int>(type: "int", nullable: false),
                    From = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    To = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(2500)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    OccuredAt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FollowUps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TagDAL",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagDAL", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FollowUpDALTagDAL",
                columns: table => new
                {
                    FollowUpsId = table.Column<int>(type: "int", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FollowUpDALTagDAL", x => new { x.FollowUpsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_FollowUpDALTagDAL_FollowUps_FollowUpsId",
                        column: x => x.FollowUpsId,
                        principalSchema: "followUp",
                        principalTable: "FollowUps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FollowUpDALTagDAL_TagDAL_TagsId",
                        column: x => x.TagsId,
                        principalTable: "TagDAL",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FollowUpDALTagDAL_TagsId",
                table: "FollowUpDALTagDAL",
                column: "TagsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FollowUpDALTagDAL");

            migrationBuilder.DropTable(
                name: "FollowUps",
                schema: "followUp");

            migrationBuilder.DropTable(
                name: "TagDAL");
        }
    }
}

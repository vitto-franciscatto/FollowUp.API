using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FollowUp.API.Features.DataBases.Migrations
{
    /// <inheritdoc />
    public partial class add_followupTags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FollowUpDALTagDAL");

            migrationBuilder.CreateTable(
                name: "FollowUpTag",
                schema: "followUp",
                columns: table => new
                {
                    FollowUpId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FollowUpTag", x => new { x.FollowUpId, x.TagId });
                    table.ForeignKey(
                        name: "FK_FollowUps_Tag",
                        column: x => x.TagId,
                        principalSchema: "tag",
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tags_FollowUp",
                        column: x => x.FollowUpId,
                        principalSchema: "followUp",
                        principalTable: "FollowUps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FollowUpTag_TagId",
                schema: "followUp",
                table: "FollowUpTag",
                column: "TagId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FollowUpTag",
                schema: "followUp");

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
                        name: "FK_FollowUpDALTagDAL_Tags_TagsId",
                        column: x => x.TagsId,
                        principalSchema: "tag",
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FollowUpDALTagDAL_TagsId",
                table: "FollowUpDALTagDAL",
                column: "TagsId");
        }
    }
}

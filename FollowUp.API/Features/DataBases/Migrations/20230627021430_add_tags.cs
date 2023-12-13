using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FollowUp.API.Features.DataBases.Migrations
{
    /// <inheritdoc />
    public partial class add_tags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FollowUpDALTagDAL_TagDAL_TagsId",
                table: "FollowUpDALTagDAL");

            migrationBuilder.DropTable(
                name: "TagDAL");

            migrationBuilder.EnsureSchema(
                name: "tag");

            migrationBuilder.CreateTable(
                name: "Tags",
                schema: "tag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_FollowUpDALTagDAL_Tags_TagsId",
                table: "FollowUpDALTagDAL",
                column: "TagsId",
                principalSchema: "tag",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FollowUpDALTagDAL_Tags_TagsId",
                table: "FollowUpDALTagDAL");

            migrationBuilder.DropTable(
                name: "Tags",
                schema: "tag");

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

            migrationBuilder.AddForeignKey(
                name: "FK_FollowUpDALTagDAL_TagDAL_TagsId",
                table: "FollowUpDALTagDAL",
                column: "TagsId",
                principalTable: "TagDAL",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

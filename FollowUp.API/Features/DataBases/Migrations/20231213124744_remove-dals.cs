using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FollowUp.API.Features.DataBases.Migrations
{
    /// <inheritdoc />
    public partial class removedals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FollowUps_Tag",
                schema: "followup",
                table: "FollowUpTag");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_FollowUp",
                schema: "followup",
                table: "FollowUpTag");

            migrationBuilder.RenameColumn(
                name: "TagId",
                schema: "followup",
                table: "FollowUpTag",
                newName: "TagsId");

            migrationBuilder.RenameColumn(
                name: "FollowUpId",
                schema: "followup",
                table: "FollowUpTag",
                newName: "FollowUpsId");

            migrationBuilder.RenameIndex(
                name: "IX_FollowUpTag_TagId",
                schema: "followup",
                table: "FollowUpTag",
                newName: "IX_FollowUpTag_TagsId");

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                schema: "followup",
                table: "FollowUps",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_FollowUpTag_FollowUps_FollowUpsId",
                schema: "followup",
                table: "FollowUpTag",
                column: "FollowUpsId",
                principalSchema: "followup",
                principalTable: "FollowUps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FollowUpTag_Tags_TagsId",
                schema: "followup",
                table: "FollowUpTag",
                column: "TagsId",
                principalSchema: "followup",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FollowUpTag_FollowUps_FollowUpsId",
                schema: "followup",
                table: "FollowUpTag");

            migrationBuilder.DropForeignKey(
                name: "FK_FollowUpTag_Tags_TagsId",
                schema: "followup",
                table: "FollowUpTag");

            migrationBuilder.RenameColumn(
                name: "TagsId",
                schema: "followup",
                table: "FollowUpTag",
                newName: "TagId");

            migrationBuilder.RenameColumn(
                name: "FollowUpsId",
                schema: "followup",
                table: "FollowUpTag",
                newName: "FollowUpId");

            migrationBuilder.RenameIndex(
                name: "IX_FollowUpTag_TagsId",
                schema: "followup",
                table: "FollowUpTag",
                newName: "IX_FollowUpTag_TagId");

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                schema: "followup",
                table: "FollowUps",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FollowUps_Tag",
                schema: "followup",
                table: "FollowUpTag",
                column: "TagId",
                principalSchema: "followup",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_FollowUp",
                schema: "followup",
                table: "FollowUpTag",
                column: "FollowUpId",
                principalSchema: "followup",
                principalTable: "FollowUps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

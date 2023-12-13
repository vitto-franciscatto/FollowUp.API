using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FollowUp.API.Features.DataBases.Migrations
{
    /// <inheritdoc />
    public partial class add_author_and_contact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "From",
                schema: "followUp",
                table: "FollowUps");

            migrationBuilder.RenameColumn(
                name: "To",
                schema: "followUp",
                table: "FollowUps",
                newName: "ContactJob");

            migrationBuilder.AddColumn<string>(
                name: "AuthorExtension",
                schema: "followUp",
                table: "FollowUps",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                schema: "followUp",
                table: "FollowUps",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ContactName",
                schema: "followUp",
                table: "FollowUps",
                type: "nvarchar(500)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactPhoneNumber",
                schema: "followUp",
                table: "FollowUps",
                type: "nvarchar(15)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorExtension",
                schema: "followUp",
                table: "FollowUps");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                schema: "followUp",
                table: "FollowUps");

            migrationBuilder.DropColumn(
                name: "ContactName",
                schema: "followUp",
                table: "FollowUps");

            migrationBuilder.DropColumn(
                name: "ContactPhoneNumber",
                schema: "followUp",
                table: "FollowUps");

            migrationBuilder.RenameColumn(
                name: "ContactJob",
                schema: "followUp",
                table: "FollowUps",
                newName: "To");

            migrationBuilder.AddColumn<string>(
                name: "From",
                schema: "followUp",
                table: "FollowUps",
                type: "nvarchar(255)",
                nullable: true);
        }
    }
}

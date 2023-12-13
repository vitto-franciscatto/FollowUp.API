using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FollowUp.API.Features.DataBases.Migrations
{
    /// <inheritdoc />
    public partial class usesingleschema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "followup");

            migrationBuilder.RenameTable(
                name: "FollowUpTag",
                schema: "followUp",
                newName: "FollowUpTag",
                newSchema: "followup");

            migrationBuilder.RenameTable(
                name: "FollowUps",
                schema: "followUp",
                newName: "FollowUps",
                newSchema: "followup");

            migrationBuilder.RenameTable(
                name: "Tags",
                schema: "tag",
                newName: "Tags",
                newSchema: "followup");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "followUp");

            migrationBuilder.EnsureSchema(
                name: "tag");

            migrationBuilder.RenameTable(
                name: "FollowUpTag",
                schema: "followup",
                newName: "FollowUpTag",
                newSchema: "followUp");

            migrationBuilder.RenameTable(
                name: "FollowUps",
                schema: "followup",
                newName: "FollowUps",
                newSchema: "followUp");

            migrationBuilder.RenameTable(
                name: "Tags",
                schema: "followup",
                newName: "Tags",
                newSchema: "tag");
        }
    }
}

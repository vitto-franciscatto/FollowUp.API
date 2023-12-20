using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FollowUp.API.Features.DataBases.Migrations
{
    /// <inheritdoc />
    public partial class followup_update_remove_assistanceid_add_identifierkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdentifierKey",
                schema: "followup",
                table: "FollowUps",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");
            
            migrationBuilder.Sql(
                @"
                    UPDATE FU
                    SET [IdentifierKey] = CONCAT('SIGA.CECOE-AssistanceId-', CAST([AssistanceId] AS nvarchar))
                    FROM [followup].[FollowUps] FU
                ");
            
            migrationBuilder.DropColumn(
                name: "AssistanceId",
                schema: "followup",
                table: "FollowUps");

            migrationBuilder.CreateIndex(
                "IX_IdentifierKey", 
                "FollowUps", 
                "IdentifierKey", 
                "followup", 
                false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                "IX_IdentifierKey", 
                "FollowUps", 
                "IdentifierKey");
            
            migrationBuilder.AddColumn<int>(
                name: "AssistanceId",
                schema: "followup",
                table: "FollowUps",
                type: "int",
                nullable: false,
                defaultValue: 0);
            
            migrationBuilder.Sql(
                @"
                    UPDATE FU
                    SET [AssistanceId] = CAST(SUBSTRING([IdentifierKey], 25, LEN([IdentifierKey]) - 24) AS int)
                    FROM [followup].[FollowUps] FU
                ");
            
            migrationBuilder.DropColumn(
                name: "IdentifierKey",
                schema: "followup",
                table: "FollowUps");
            
        }
    }
}

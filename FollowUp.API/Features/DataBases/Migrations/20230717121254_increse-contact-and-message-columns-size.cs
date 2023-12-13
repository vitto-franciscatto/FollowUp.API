using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FollowUp.API.Features.DataBases.Migrations
{
    /// <inheritdoc />
    public partial class incresecontactandmessagecolumnssize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Message",
                schema: "followUp",
                table: "FollowUps",
                type: "nvarchar(4000)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(2500)");

            migrationBuilder.AlterColumn<string>(
                name: "ContactPhoneNumber",
                schema: "followUp",
                table: "FollowUps",
                type: "nvarchar(19)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Message",
                schema: "followUp",
                table: "FollowUps",
                type: "nvarchar(2500)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(4000)");

            migrationBuilder.AlterColumn<string>(
                name: "ContactPhoneNumber",
                schema: "followUp",
                table: "FollowUps",
                type: "nvarchar(15)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(19)",
                oldNullable: true);
        }
    }
}

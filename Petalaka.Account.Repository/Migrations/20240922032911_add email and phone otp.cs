using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Petalaka.Account.Repository.Migrations
{
    /// <inheritdoc />
    public partial class addemailandphoneotp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmailOtp",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EmailOtpExpiration",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneOtp",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneOtpExpiration",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailOtp",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EmailOtpExpiration",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PhoneOtp",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PhoneOtpExpiration",
                table: "Users");
        }
    }
}

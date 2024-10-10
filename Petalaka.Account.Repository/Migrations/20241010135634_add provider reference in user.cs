using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Petalaka.Account.Repository.Migrations
{
    /// <inheritdoc />
    public partial class addproviderreferenceinuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Providers_UserId",
                table: "Providers");

            migrationBuilder.AddColumn<Guid>(
                name: "ProviderId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("89fca251-f021-425b-de62-08dcdfcdb851"),
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 10, 20, 56, 34, 175, DateTimeKind.Unspecified).AddTicks(7624), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 10, 20, 56, 34, 175, DateTimeKind.Unspecified).AddTicks(7625), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("b74c0a77-a451-4f16-de61-08dcdfcdb851"),
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 10, 20, 56, 34, 175, DateTimeKind.Unspecified).AddTicks(7619), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 10, 20, 56, 34, 175, DateTimeKind.Unspecified).AddTicks(7619), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("d14e804f-1132-4923-de63-08dcdfcdb851"),
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 10, 20, 56, 34, 175, DateTimeKind.Unspecified).AddTicks(7629), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 10, 20, 56, 34, 175, DateTimeKind.Unspecified).AddTicks(7630), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("b74c0a77-a451-4f16-de61-08dcdfcdb851"), new Guid("094de1df-60b1-4a58-878c-dc6909f7350b") },
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 10, 20, 56, 34, 175, DateTimeKind.Unspecified).AddTicks(7669), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 10, 20, 56, 34, 175, DateTimeKind.Unspecified).AddTicks(7669), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("d14e804f-1132-4923-de63-08dcdfcdb851"), new Guid("a3ee2988-67b2-4017-b63b-a0dae4708359") },
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 10, 20, 56, 34, 175, DateTimeKind.Unspecified).AddTicks(7676), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 10, 20, 56, 34, 175, DateTimeKind.Unspecified).AddTicks(7676), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("094de1df-60b1-4a58-878c-dc6909f7350b"),
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "LastUpdatedTime", "ProviderId" },
                values: new object[] { "2c1485d1-15d3-4d16-91b2-0f2433386d77", new DateTimeOffset(new DateTime(2024, 10, 10, 20, 56, 34, 175, DateTimeKind.Unspecified).AddTicks(7500), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 10, 20, 56, 34, 175, DateTimeKind.Unspecified).AddTicks(7500), new TimeSpan(0, 7, 0, 0, 0)), null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a3ee2988-67b2-4017-b63b-a0dae4708359"),
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "LastUpdatedTime", "ProviderId" },
                values: new object[] { "16a3cdb8-bf2b-4ecd-bfce-7bdc94b19700", new DateTimeOffset(new DateTime(2024, 10, 10, 20, 56, 34, 175, DateTimeKind.Unspecified).AddTicks(7562), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 10, 20, 56, 34, 175, DateTimeKind.Unspecified).AddTicks(7562), new TimeSpan(0, 7, 0, 0, 0)), null });

            migrationBuilder.CreateIndex(
                name: "IX_Providers_UserId",
                table: "Providers",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Providers_UserId",
                table: "Providers");

            migrationBuilder.DropColumn(
                name: "ProviderId",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("89fca251-f021-425b-de62-08dcdfcdb851"),
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 10, 20, 43, 12, 486, DateTimeKind.Unspecified).AddTicks(9507), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 10, 20, 43, 12, 486, DateTimeKind.Unspecified).AddTicks(9508), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("b74c0a77-a451-4f16-de61-08dcdfcdb851"),
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 10, 20, 43, 12, 486, DateTimeKind.Unspecified).AddTicks(9501), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 10, 20, 43, 12, 486, DateTimeKind.Unspecified).AddTicks(9502), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("d14e804f-1132-4923-de63-08dcdfcdb851"),
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 10, 20, 43, 12, 486, DateTimeKind.Unspecified).AddTicks(9512), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 10, 20, 43, 12, 486, DateTimeKind.Unspecified).AddTicks(9513), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("b74c0a77-a451-4f16-de61-08dcdfcdb851"), new Guid("094de1df-60b1-4a58-878c-dc6909f7350b") },
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 10, 20, 43, 12, 486, DateTimeKind.Unspecified).AddTicks(9548), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 10, 20, 43, 12, 486, DateTimeKind.Unspecified).AddTicks(9548), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("d14e804f-1132-4923-de63-08dcdfcdb851"), new Guid("a3ee2988-67b2-4017-b63b-a0dae4708359") },
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 10, 20, 43, 12, 486, DateTimeKind.Unspecified).AddTicks(9556), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 10, 20, 43, 12, 486, DateTimeKind.Unspecified).AddTicks(9556), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("094de1df-60b1-4a58-878c-dc6909f7350b"),
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "LastUpdatedTime" },
                values: new object[] { "0525e797-41ce-47a7-aaf8-348f55991abe", new DateTimeOffset(new DateTime(2024, 10, 10, 20, 43, 12, 486, DateTimeKind.Unspecified).AddTicks(9400), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 10, 20, 43, 12, 486, DateTimeKind.Unspecified).AddTicks(9400), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a3ee2988-67b2-4017-b63b-a0dae4708359"),
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "LastUpdatedTime" },
                values: new object[] { "1a1e405f-3f80-4e22-a5b5-71ce5f42d054", new DateTimeOffset(new DateTime(2024, 10, 10, 20, 43, 12, 486, DateTimeKind.Unspecified).AddTicks(9448), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 10, 20, 43, 12, 486, DateTimeKind.Unspecified).AddTicks(9448), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.CreateIndex(
                name: "IX_Providers_UserId",
                table: "Providers",
                column: "UserId");
        }
    }
}

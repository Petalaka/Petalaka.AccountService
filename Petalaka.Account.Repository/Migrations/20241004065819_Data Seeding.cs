using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Petalaka.Account.Repository.Migrations
{
    /// <inheritdoc />
    public partial class DataSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "LastUpdatedBy", "LastUpdatedTime", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("89fca251-f021-425b-de62-08dcdfcdb851"), "A6WZZDMSOY6XEPH4VJRSRVTAXICX34US", "System", new DateTimeOffset(new DateTime(2024, 10, 4, 13, 58, 18, 623, DateTimeKind.Unspecified).AddTicks(7055), new TimeSpan(0, 7, 0, 0, 0)), null, null, "System", new DateTimeOffset(new DateTime(2024, 10, 4, 13, 58, 18, 623, DateTimeKind.Unspecified).AddTicks(7056), new TimeSpan(0, 7, 0, 0, 0)), "USER", "USER" },
                    { new Guid("b74c0a77-a451-4f16-de61-08dcdfcdb851"), "A6WZZDMSOY6XEPH4VJRSRVTAXICX34US", "System", new DateTimeOffset(new DateTime(2024, 10, 4, 13, 58, 18, 623, DateTimeKind.Unspecified).AddTicks(7048), new TimeSpan(0, 7, 0, 0, 0)), null, null, "System", new DateTimeOffset(new DateTime(2024, 10, 4, 13, 58, 18, 623, DateTimeKind.Unspecified).AddTicks(7050), new TimeSpan(0, 7, 0, 0, 0)), "ADMIN", "ADMIN" },
                    { new Guid("d14e804f-1132-4923-de63-08dcdfcdb851"), "A6WZZDMSOY6XEPH4VJRSRVTAXICX34US", "System", new DateTimeOffset(new DateTime(2024, 10, 4, 13, 58, 18, 623, DateTimeKind.Unspecified).AddTicks(7059), new TimeSpan(0, 7, 0, 0, 0)), null, null, "System", new DateTimeOffset(new DateTime(2024, 10, 4, 13, 58, 18, 623, DateTimeKind.Unspecified).AddTicks(7060), new TimeSpan(0, 7, 0, 0, 0)), "PROVIDER", "PROVIDER" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "CreatedBy", "CreatedTime", "DateOfBirth", "DeletedBy", "DeletedTime", "Email", "EmailConfirmed", "EmailOtp", "EmailOtpExpiration", "FullName", "Gender", "LastUpdatedBy", "LastUpdatedTime", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PhoneOtp", "PhoneOtpExpiration", "Salt", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("094de1df-60b1-4a58-878c-dc6909f7350b"), 0, null, "dc0c9be6-ac4c-455a-9cb1-ecff856601bb", null, new DateTimeOffset(new DateTime(2024, 10, 4, 13, 58, 18, 623, DateTimeKind.Unspecified).AddTicks(6943), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, "admin@gmail.com", true, null, null, "admin", null, null, new DateTimeOffset(new DateTime(2024, 10, 4, 13, 58, 18, 623, DateTimeKind.Unspecified).AddTicks(6943), new TimeSpan(0, 7, 0, 0, 0)), false, null, "ADMIN@GMAIL.COM", "admin", "AQAAAAIAAYagAAAAEKlMNvvuvDkRs2XwysLan5iHCJP9ImDgi6iw39nygXtE1ant3Kv5n2oi6hZCqwDybA==", null, false, null, null, "UQquiGRiRIG1g/4gdm/sfMY7Kk0qqcV8iAYaY8eRmAo=", "A6WZZDMSOY6XEPH4VJRSRVTAXICX34US", false, "admin@gmail.com" },
                    { new Guid("a3ee2988-67b2-4017-b63b-a0dae4708359"), 0, null, "dc6d04e5-64ed-4dc0-856d-9f0ea9198d76", null, new DateTimeOffset(new DateTime(2024, 10, 4, 13, 58, 18, 623, DateTimeKind.Unspecified).AddTicks(6998), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, "provider@gmail.com", true, null, null, "provider", null, null, new DateTimeOffset(new DateTime(2024, 10, 4, 13, 58, 18, 623, DateTimeKind.Unspecified).AddTicks(6998), new TimeSpan(0, 7, 0, 0, 0)), false, null, "PROVIDER@GMAIL.COM", "provider", "AQAAAAIAAYagAAAAEKlMNvvuvDkRs2XwysLan5iHCJP9ImDgi6iw39nygXtE1ant3Kv5n2oi6hZCqwDybA==", null, false, null, null, "UQquiGRiRIG1g/4gdm/sfMY7Kk0qqcV8iAYaY8eRmAo=", "A6WZZDMSOY6XEPH4VJRSRVTAXICX34US", false, "provider@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "LastUpdatedBy", "LastUpdatedTime" },
                values: new object[,]
                {
                    { new Guid("b74c0a77-a451-4f16-de61-08dcdfcdb851"), new Guid("094de1df-60b1-4a58-878c-dc6909f7350b"), null, new DateTimeOffset(new DateTime(2024, 10, 4, 13, 58, 18, 623, DateTimeKind.Unspecified).AddTicks(7090), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2024, 10, 4, 13, 58, 18, 623, DateTimeKind.Unspecified).AddTicks(7090), new TimeSpan(0, 7, 0, 0, 0)) },
                    { new Guid("d14e804f-1132-4923-de63-08dcdfcdb851"), new Guid("a3ee2988-67b2-4017-b63b-a0dae4708359"), null, new DateTimeOffset(new DateTime(2024, 10, 4, 13, 58, 18, 623, DateTimeKind.Unspecified).AddTicks(7095), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2024, 10, 4, 13, 58, 18, 623, DateTimeKind.Unspecified).AddTicks(7095), new TimeSpan(0, 7, 0, 0, 0)) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("89fca251-f021-425b-de62-08dcdfcdb851"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("b74c0a77-a451-4f16-de61-08dcdfcdb851"), new Guid("094de1df-60b1-4a58-878c-dc6909f7350b") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("d14e804f-1132-4923-de63-08dcdfcdb851"), new Guid("a3ee2988-67b2-4017-b63b-a0dae4708359") });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("b74c0a77-a451-4f16-de61-08dcdfcdb851"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("d14e804f-1132-4923-de63-08dcdfcdb851"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("094de1df-60b1-4a58-878c-dc6909f7350b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a3ee2988-67b2-4017-b63b-a0dae4708359"));
        }
    }
}

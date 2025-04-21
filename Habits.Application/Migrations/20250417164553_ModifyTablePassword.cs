using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Habits.Application.Migrations
{
    /// <inheritdoc />
    public partial class ModifyTablePassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "PasswordHash",
                value: "$2a$11$GZ.ursK2W8E3HD357j4hFOoC8SdU/saarJaKsBR6dJm/K6a8l5FfK");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "PasswordHash",
                value: "somehashedpassword");
        }
    }
}

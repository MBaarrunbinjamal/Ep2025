using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_project2025.Migrations
{
    /// <inheritdoc />
    public partial class uyt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "SeminarRegistrations",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "conductedon",
                table: "seminar",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_SeminarRegistrations_SeminarId",
                table: "SeminarRegistrations",
                column: "SeminarId");

            migrationBuilder.CreateIndex(
                name: "IX_SeminarRegistrations_UserId",
                table: "SeminarRegistrations",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SeminarRegistrations_AspNetUsers_UserId",
                table: "SeminarRegistrations",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SeminarRegistrations_seminar_SeminarId",
                table: "SeminarRegistrations",
                column: "SeminarId",
                principalTable: "seminar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SeminarRegistrations_AspNetUsers_UserId",
                table: "SeminarRegistrations");

            migrationBuilder.DropForeignKey(
                name: "FK_SeminarRegistrations_seminar_SeminarId",
                table: "SeminarRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_SeminarRegistrations_SeminarId",
                table: "SeminarRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_SeminarRegistrations_UserId",
                table: "SeminarRegistrations");

            migrationBuilder.DropColumn(
                name: "conductedon",
                table: "seminar");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "SeminarRegistrations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}

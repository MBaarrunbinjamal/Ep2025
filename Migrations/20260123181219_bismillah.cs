using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_project2025.Migrations
{
    /// <inheritdoc />
    public partial class bismillah : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_seminar_AspNetUsers_UserId",
                table: "seminar");

            migrationBuilder.DropIndex(
                name: "IX_seminar_UserId",
                table: "seminar");

            migrationBuilder.RenameColumn(
                name: "venue",
                table: "seminar",
                newName: "Venue");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "seminar",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "seminar",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "image",
                table: "seminar",
                newName: "ImageUrl");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "seminar",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateTable(
                name: "SeminarRegistrations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeminarId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegisteredOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeminarRegistrations", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SeminarRegistrations");

            migrationBuilder.RenameColumn(
                name: "Venue",
                table: "seminar",
                newName: "venue");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "seminar",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "seminar",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "seminar",
                newName: "image");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "seminar",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_seminar_UserId",
                table: "seminar",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_seminar_AspNetUsers_UserId",
                table: "seminar",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

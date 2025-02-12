using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class dev_v1_250212_JC2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SysRole_User_UserId",
                table: "SysRole");

            migrationBuilder.DropIndex(
                name: "IX_SysRole_UserId",
                table: "SysRole");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SysRole");

            migrationBuilder.CreateIndex(
                name: "IX_SysRole_Name",
                table: "SysRole",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SysRole_Name",
                table: "SysRole");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "SysRole",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "SysRole",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "SysRole",
                keyColumn: "Id",
                keyValue: 2,
                column: "UserId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_SysRole_UserId",
                table: "SysRole",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SysRole_User_UserId",
                table: "SysRole",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class VolunteerId_addedTo_Users : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VolunteerId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_VolunteerId",
                table: "Users",
                column: "VolunteerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Volunteers_VolunteerId",
                table: "Users",
                column: "VolunteerId",
                principalTable: "Volunteers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Volunteers_VolunteerId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_VolunteerId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "VolunteerId",
                table: "Users");
        }
    }
}

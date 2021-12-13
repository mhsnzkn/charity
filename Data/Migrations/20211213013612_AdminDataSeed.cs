using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class AdminDataSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Job", "Name", "PasswordHash", "PasswordSalt", "Role", "Status" },
                values: new object[] { 1, "admin@heart4refugees.org", null, "Admin", new byte[] { 40, 148, 12, 59, 112, 190, 150, 11, 242, 31, 127, 40, 62, 157, 43, 187, 19, 139, 177, 16, 125, 155, 26, 169, 179, 255, 251, 248, 77, 112, 236, 148, 48, 101, 226, 52, 140, 136, 186, 67, 144, 130, 138, 11, 2, 177, 60, 191, 209, 106, 213, 112, 78, 239, 211, 30, 248, 232, 98, 211, 160, 30, 229, 164 }, new byte[] { 112, 254, 12, 25, 91, 72, 57, 43, 177, 240, 227, 253, 18, 175, 11, 81, 57, 168, 6, 162, 221, 73, 23, 24, 38, 179, 170, 140, 124, 188, 198, 194, 54, 61, 191, 29, 151, 57, 89, 46, 226, 154, 233, 80, 11, 183, 251, 69, 106, 110, 108, 90, 209, 93, 199, 89, 98, 190, 64, 143, 131, 205, 112, 164, 208, 193, 91, 93, 210, 203, 112, 80, 186, 118, 86, 138, 83, 121, 149, 73, 35, 30, 131, 54, 110, 10, 199, 207, 213, 47, 43, 171, 239, 55, 17, 183, 143, 51, 49, 254, 71, 10, 87, 27, 46, 14, 245, 186, 150, 176, 117, 173, 7, 103, 217, 212, 166, 133, 68, 56, 196, 63, 255, 34, 203, 94, 3, 233 }, "admin", 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}

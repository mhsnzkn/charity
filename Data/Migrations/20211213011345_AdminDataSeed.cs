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
                values: new object[] { 1, "admin@heart4refugees.org", null, "Admin", new byte[] { 180, 191, 129, 114, 124, 155, 15, 216, 116, 172, 34, 202, 36, 65, 71, 176, 170, 40, 127, 124, 101, 61, 186, 170, 254, 13, 72, 157, 135, 89, 60, 117, 49, 219, 0, 219, 148, 233, 203, 145, 50, 71, 109, 48, 131, 60, 208, 81, 193, 43, 230, 122, 101, 66, 97, 83, 101, 108, 208, 110, 164, 172, 220, 254 }, new byte[] { 181, 221, 207, 61, 198, 80, 31, 178, 243, 135, 203, 223, 68, 23, 153, 110, 120, 25, 13, 48, 121, 174, 6, 154, 110, 174, 220, 95, 155, 177, 231, 196, 198, 17, 145, 80, 7, 77, 210, 189, 223, 240, 239, 238, 146, 27, 75, 127, 11, 50, 104, 193, 43, 137, 203, 235, 176, 85, 244, 240, 219, 4, 210, 224, 92, 221, 219, 15, 51, 192, 59, 7, 91, 5, 215, 105, 240, 25, 133, 119, 170, 227, 207, 61, 255, 19, 167, 191, 8, 131, 7, 7, 107, 129, 72, 151, 132, 20, 250, 132, 112, 86, 72, 5, 71, 195, 93, 228, 202, 23, 22, 90, 62, 32, 138, 131, 170, 163, 30, 21, 194, 204, 35, 189, 19, 18, 91, 118 }, "admin", 0 });
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

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class Expense_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VolunteerId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ModeOfTransport = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TotalMileage = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    Claim = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CancellationReason = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CommonFileId = table.Column<int>(type: "int", nullable: true),
                    CrtDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UptDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expenses_CommonFiles_CommonFileId",
                        column: x => x.CommonFileId,
                        principalTable: "CommonFiles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Expenses_Volunteers_VolunteerId",
                        column: x => x.VolunteerId,
                        principalTable: "Volunteers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_CommonFileId",
                table: "Expenses",
                column: "CommonFileId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_VolunteerId",
                table: "Expenses",
                column: "VolunteerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Expenses");
        }
    }
}

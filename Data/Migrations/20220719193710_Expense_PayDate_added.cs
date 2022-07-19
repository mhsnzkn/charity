using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class Expense_PayDate_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CancellationReason",
                table: "Expenses",
                newName: "Description");

            migrationBuilder.AddColumn<DateTime>(
                name: "PayDate",
                table: "Expenses",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PayDate",
                table: "Expenses");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Expenses",
                newName: "CancellationReason");
        }
    }
}

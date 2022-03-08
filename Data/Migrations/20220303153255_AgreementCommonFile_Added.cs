using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class AgreementCommonFile_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agreements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false),
                    CrtDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UptDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agreements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CommonFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Path = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CrtDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UptDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommonFiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VolunteerAgreements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VolunteerId = table.Column<int>(type: "int", nullable: false),
                    AgreementId = table.Column<int>(type: "int", nullable: false),
                    CrtDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UptDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VolunteerAgreements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VolunteerAgreements_Agreements_AgreementId",
                        column: x => x.AgreementId,
                        principalTable: "Agreements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VolunteerAgreements_Volunteers_VolunteerId",
                        column: x => x.VolunteerId,
                        principalTable: "Volunteers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VolunteerFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VolunteerId = table.Column<int>(type: "int", nullable: false),
                    CommonFileId = table.Column<int>(type: "int", nullable: false),
                    CrtDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UptDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VolunteerFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VolunteerFiles_CommonFiles_CommonFileId",
                        column: x => x.CommonFileId,
                        principalTable: "CommonFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VolunteerFiles_Volunteers_VolunteerId",
                        column: x => x.VolunteerId,
                        principalTable: "Volunteers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerAgreements_AgreementId",
                table: "VolunteerAgreements",
                column: "AgreementId");

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerAgreements_VolunteerId",
                table: "VolunteerAgreements",
                column: "VolunteerId");

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerFiles_CommonFileId",
                table: "VolunteerFiles",
                column: "CommonFileId");

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerFiles_VolunteerId",
                table: "VolunteerFiles",
                column: "VolunteerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VolunteerAgreements");

            migrationBuilder.DropTable(
                name: "VolunteerFiles");

            migrationBuilder.DropTable(
                name: "Agreements");

            migrationBuilder.DropTable(
                name: "CommonFiles");
        }
    }
}

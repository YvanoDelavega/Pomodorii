using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pomodorii.Api.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Tomates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nom = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ImageUrl = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tomates", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Semis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TomateId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Semis_Tomates_TomateId",
                        column: x => x.TomateId,
                        principalTable: "Tomates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Tomates",
                columns: new[] { "Id", "Description", "ImageUrl", "Nom" },
                values: new object[,]
                {
                    { 1, "très bon", "img/tomates/ALAMBRA.gif", "Alambra" },
                    { 2, "très bon", "img/tomates/Andine Cornue.gif", "Andine Cornue" },
                    { 3, "très bon", "img/tomates/COBRA.gif", "Cobra" },
                    { 4, "très bon", "img/tomates/coeur-de-boeuf.gif", "Coeur-de-boeuf" },
                    { 5, "très bon", "img/tomates/noire-de-crimee.gif", "noire-de-crimee" },
                    { 6, "très bon", "img/tomates/russe-rouge.gif", "russe-rouge" },
                    { 7, "très bon", "img/tomates/tomito-f1.gif", "tomito-f1" }
                });

            migrationBuilder.InsertData(
                table: "Semis",
                columns: new[] { "Id", "Date", "TomateId" },
                values: new object[] { 8, new DateTime(2022, 7, 16, 0, 0, 0, 0, DateTimeKind.Local), 1 });

            migrationBuilder.InsertData(
                table: "Semis",
                columns: new[] { "Id", "Date", "TomateId" },
                values: new object[] { 9, new DateTime(2022, 7, 16, 0, 0, 0, 0, DateTimeKind.Local), 2 });

            migrationBuilder.InsertData(
                table: "Semis",
                columns: new[] { "Id", "Date", "TomateId" },
                values: new object[] { 10, new DateTime(2022, 7, 14, 0, 0, 0, 0, DateTimeKind.Local), 2 });

            migrationBuilder.CreateIndex(
                name: "IX_Semis_TomateId",
                table: "Semis",
                column: "TomateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Semis");

            migrationBuilder.DropTable(
                name: "Tomates");
        }
    }
}

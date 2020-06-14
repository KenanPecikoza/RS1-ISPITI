using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RS1_Ispit_asp.net_core.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ispit",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AngazovanId = table.Column<int>(nullable: false),
                    Datum = table.Column<DateTime>(nullable: false),
                    Napomena = table.Column<string>(nullable: true),
                    Zaključano = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ispit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ispit_Angazovan_AngazovanId",
                        column: x => x.AngazovanId,
                        principalTable: "Angazovan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "IspitStudent",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ocjena = table.Column<int>(nullable: false),
                    Prijava = table.Column<bool>(nullable: false),
                    Pristupio = table.Column<bool>(nullable: false),
                    SlusaPredmetId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IspitStudent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IspitStudent_SlusaPredmet_SlusaPredmetId",
                        column: x => x.SlusaPredmetId,
                        principalTable: "SlusaPredmet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ispit_AngazovanId",
                table: "Ispit",
                column: "AngazovanId");

            migrationBuilder.CreateIndex(
                name: "IX_IspitStudent_SlusaPredmetId",
                table: "IspitStudent",
                column: "SlusaPredmetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ispit");

            migrationBuilder.DropTable(
                name: "IspitStudent");
        }
    }
}

using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RS1_Ispit_asp.net_core.Migrations
{
    public partial class _3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OdrzanaCasStavka");

            migrationBuilder.CreateTable(
                name: "OdrzaniCasStavka",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ocjena = table.Column<int>(nullable: false),
                    OdjeljenjeStavkaId = table.Column<int>(nullable: false),
                    OdrzaniCasId = table.Column<int>(nullable: false),
                    Opravdano = table.Column<bool>(nullable: false),
                    Prisutan = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OdrzaniCasStavka", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OdrzaniCasStavka_OdjeljenjeStavka_OdjeljenjeStavkaId",
                        column: x => x.OdjeljenjeStavkaId,
                        principalTable: "OdjeljenjeStavka",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_OdrzaniCasStavka_OdrzaniCas_OdrzaniCasId",
                        column: x => x.OdrzaniCasId,
                        principalTable: "OdrzaniCas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OdrzaniCasStavka_OdjeljenjeStavkaId",
                table: "OdrzaniCasStavka",
                column: "OdjeljenjeStavkaId");

            migrationBuilder.CreateIndex(
                name: "IX_OdrzaniCasStavka_OdrzaniCasId",
                table: "OdrzaniCasStavka",
                column: "OdrzaniCasId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OdrzaniCasStavka");

            migrationBuilder.CreateTable(
                name: "OdrzanaCasStavka",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ocjena = table.Column<int>(nullable: false),
                    OdjeljenjeStavkaId = table.Column<int>(nullable: false),
                    OdrzaniCasId = table.Column<int>(nullable: false),
                    Opravdano = table.Column<bool>(nullable: false),
                    Prisutan = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OdrzanaCasStavka", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OdrzanaCasStavka_OdjeljenjeStavka_OdjeljenjeStavkaId",
                        column: x => x.OdjeljenjeStavkaId,
                        principalTable: "OdjeljenjeStavka",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_OdrzanaCasStavka_OdrzaniCas_OdrzaniCasId",
                        column: x => x.OdrzaniCasId,
                        principalTable: "OdrzaniCas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OdrzanaCasStavka_OdjeljenjeStavkaId",
                table: "OdrzanaCasStavka",
                column: "OdjeljenjeStavkaId");

            migrationBuilder.CreateIndex(
                name: "IX_OdrzanaCasStavka_OdrzaniCasId",
                table: "OdrzanaCasStavka",
                column: "OdrzaniCasId");
        }
    }
}

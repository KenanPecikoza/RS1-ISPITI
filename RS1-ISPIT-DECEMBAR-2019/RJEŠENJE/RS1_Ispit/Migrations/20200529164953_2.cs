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
                name: "PopravniIspit",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClanKomisije1Id = table.Column<int>(nullable: false),
                    ClanKomisije2Id = table.Column<int>(nullable: false),
                    ClanKomisije3ID = table.Column<int>(nullable: false),
                    Datum = table.Column<DateTime>(nullable: false),
                    NastavnikId = table.Column<int>(nullable: false),
                    PredmetId = table.Column<int>(nullable: false),
                    SkolskaGodinaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PopravniIspit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PopravniIspit_Nastavnik_ClanKomisije1Id",
                        column: x => x.ClanKomisije1Id,
                        principalTable: "Nastavnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PopravniIspit_Nastavnik_ClanKomisije2Id",
                        column: x => x.ClanKomisije2Id,
                        principalTable: "Nastavnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PopravniIspit_Nastavnik_ClanKomisije3ID",
                        column: x => x.ClanKomisije3ID,
                        principalTable: "Nastavnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PopravniIspit_Nastavnik_NastavnikId",
                        column: x => x.NastavnikId,
                        principalTable: "Nastavnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PopravniIspit_Predmet_PredmetId",
                        column: x => x.PredmetId,
                        principalTable: "Predmet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PopravniIspit_SkolskaGodina_SkolskaGodinaId",
                        column: x => x.SkolskaGodinaId,
                        principalTable: "SkolskaGodina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "PopravniIspitStavka",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Bodovi = table.Column<double>(nullable: false),
                    OdjeljenjeStavkaId = table.Column<int>(nullable: false),
                    PopravniIspitId = table.Column<int>(nullable: false),
                    Pristupio = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PopravniIspitStavka", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PopravniIspitStavka_OdjeljenjeStavka_OdjeljenjeStavkaId",
                        column: x => x.OdjeljenjeStavkaId,
                        principalTable: "OdjeljenjeStavka",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PopravniIspitStavka_PopravniIspit_PopravniIspitId",
                        column: x => x.PopravniIspitId,
                        principalTable: "PopravniIspit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PopravniIspit_ClanKomisije1Id",
                table: "PopravniIspit",
                column: "ClanKomisije1Id");

            migrationBuilder.CreateIndex(
                name: "IX_PopravniIspit_ClanKomisije2Id",
                table: "PopravniIspit",
                column: "ClanKomisije2Id");

            migrationBuilder.CreateIndex(
                name: "IX_PopravniIspit_ClanKomisije3ID",
                table: "PopravniIspit",
                column: "ClanKomisije3ID");

            migrationBuilder.CreateIndex(
                name: "IX_PopravniIspit_NastavnikId",
                table: "PopravniIspit",
                column: "NastavnikId");

            migrationBuilder.CreateIndex(
                name: "IX_PopravniIspit_PredmetId",
                table: "PopravniIspit",
                column: "PredmetId");

            migrationBuilder.CreateIndex(
                name: "IX_PopravniIspit_SkolskaGodinaId",
                table: "PopravniIspit",
                column: "SkolskaGodinaId");

            migrationBuilder.CreateIndex(
                name: "IX_PopravniIspitStavka_OdjeljenjeStavkaId",
                table: "PopravniIspitStavka",
                column: "OdjeljenjeStavkaId");

            migrationBuilder.CreateIndex(
                name: "IX_PopravniIspitStavka_PopravniIspitId",
                table: "PopravniIspitStavka",
                column: "PopravniIspitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PopravniIspitStavka");

            migrationBuilder.DropTable(
                name: "PopravniIspit");
        }
    }
}

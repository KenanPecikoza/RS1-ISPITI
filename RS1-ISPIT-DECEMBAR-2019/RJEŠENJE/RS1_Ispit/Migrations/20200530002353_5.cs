using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RS1_Ispit_asp.net_core.Migrations
{
    public partial class _5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PopravniIspit_Nastavnik_ClanKomisije3ID",
                table: "PopravniIspit");

            migrationBuilder.RenameColumn(
                name: "ClanKomisije3ID",
                table: "PopravniIspit",
                newName: "ClanKomisije3Id");

            migrationBuilder.RenameIndex(
                name: "IX_PopravniIspit_ClanKomisije3ID",
                table: "PopravniIspit",
                newName: "IX_PopravniIspit_ClanKomisije3Id");

            migrationBuilder.AddColumn<int>(
                name: "SkolaId",
                table: "PopravniIspit",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PopravniIspit_SkolaId",
                table: "PopravniIspit",
                column: "SkolaId");

            migrationBuilder.AddForeignKey(
                name: "FK_PopravniIspit_Nastavnik_ClanKomisije3Id",
                table: "PopravniIspit",
                column: "ClanKomisije3Id",
                principalTable: "Nastavnik",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_PopravniIspit_Skola_SkolaId",
                table: "PopravniIspit",
                column: "SkolaId",
                principalTable: "Skola",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PopravniIspit_Nastavnik_ClanKomisije3Id",
                table: "PopravniIspit");

            migrationBuilder.DropForeignKey(
                name: "FK_PopravniIspit_Skola_SkolaId",
                table: "PopravniIspit");

            migrationBuilder.DropIndex(
                name: "IX_PopravniIspit_SkolaId",
                table: "PopravniIspit");

            migrationBuilder.DropColumn(
                name: "SkolaId",
                table: "PopravniIspit");

            migrationBuilder.RenameColumn(
                name: "ClanKomisije3Id",
                table: "PopravniIspit",
                newName: "ClanKomisije3ID");

            migrationBuilder.RenameIndex(
                name: "IX_PopravniIspit_ClanKomisije3Id",
                table: "PopravniIspit",
                newName: "IX_PopravniIspit_ClanKomisije3ID");

            migrationBuilder.AddForeignKey(
                name: "FK_PopravniIspit_Nastavnik_ClanKomisije3ID",
                table: "PopravniIspit",
                column: "ClanKomisije3ID",
                principalTable: "Nastavnik",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}

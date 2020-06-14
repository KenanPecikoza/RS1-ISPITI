using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RS1_Ispit_asp.net_core.Migrations
{
    public partial class _3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PopravniIspit_Nastavnik_NastavnikId",
                table: "PopravniIspit");

            migrationBuilder.DropForeignKey(
                name: "FK_PopravniIspitStavka_OdjeljenjeStavka_OdjeljenjeStavkaId",
                table: "PopravniIspitStavka");

            migrationBuilder.DropIndex(
                name: "IX_PopravniIspit_NastavnikId",
                table: "PopravniIspit");

            migrationBuilder.DropColumn(
                name: "NastavnikId",
                table: "PopravniIspit");

            migrationBuilder.RenameColumn(
                name: "OdjeljenjeStavkaId",
                table: "PopravniIspitStavka",
                newName: "DodjeljenPredmetId");

            migrationBuilder.RenameIndex(
                name: "IX_PopravniIspitStavka_OdjeljenjeStavkaId",
                table: "PopravniIspitStavka",
                newName: "IX_PopravniIspitStavka_DodjeljenPredmetId");

            migrationBuilder.AddColumn<int>(
                name: "BrojUDnevniku",
                table: "PopravniIspitStavka",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Izlazak",
                table: "PopravniIspitStavka",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_PopravniIspitStavka_DodjeljenPredmet_DodjeljenPredmetId",
                table: "PopravniIspitStavka",
                column: "DodjeljenPredmetId",
                principalTable: "DodjeljenPredmet",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PopravniIspitStavka_DodjeljenPredmet_DodjeljenPredmetId",
                table: "PopravniIspitStavka");

            migrationBuilder.DropColumn(
                name: "BrojUDnevniku",
                table: "PopravniIspitStavka");

            migrationBuilder.DropColumn(
                name: "Izlazak",
                table: "PopravniIspitStavka");

            migrationBuilder.RenameColumn(
                name: "DodjeljenPredmetId",
                table: "PopravniIspitStavka",
                newName: "OdjeljenjeStavkaId");

            migrationBuilder.RenameIndex(
                name: "IX_PopravniIspitStavka_DodjeljenPredmetId",
                table: "PopravniIspitStavka",
                newName: "IX_PopravniIspitStavka_OdjeljenjeStavkaId");

            migrationBuilder.AddColumn<int>(
                name: "NastavnikId",
                table: "PopravniIspit",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PopravniIspit_NastavnikId",
                table: "PopravniIspit",
                column: "NastavnikId");

            migrationBuilder.AddForeignKey(
                name: "FK_PopravniIspit_Nastavnik_NastavnikId",
                table: "PopravniIspit",
                column: "NastavnikId",
                principalTable: "Nastavnik",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_PopravniIspitStavka_OdjeljenjeStavka_OdjeljenjeStavkaId",
                table: "PopravniIspitStavka",
                column: "OdjeljenjeStavkaId",
                principalTable: "OdjeljenjeStavka",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RS1_Ispit_asp.net_core.Migrations
{
    public partial class _4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PopravniIspitStavka_DodjeljenPredmet_DodjeljenPredmetId",
                table: "PopravniIspitStavka");

            migrationBuilder.RenameColumn(
                name: "DodjeljenPredmetId",
                table: "PopravniIspitStavka",
                newName: "OdjeljenjeStavkaId");

            migrationBuilder.RenameIndex(
                name: "IX_PopravniIspitStavka_DodjeljenPredmetId",
                table: "PopravniIspitStavka",
                newName: "IX_PopravniIspitStavka_OdjeljenjeStavkaId");

            migrationBuilder.AddForeignKey(
                name: "FK_PopravniIspitStavka_OdjeljenjeStavka_OdjeljenjeStavkaId",
                table: "PopravniIspitStavka",
                column: "OdjeljenjeStavkaId",
                principalTable: "OdjeljenjeStavka",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PopravniIspitStavka_OdjeljenjeStavka_OdjeljenjeStavkaId",
                table: "PopravniIspitStavka");

            migrationBuilder.RenameColumn(
                name: "OdjeljenjeStavkaId",
                table: "PopravniIspitStavka",
                newName: "DodjeljenPredmetId");

            migrationBuilder.RenameIndex(
                name: "IX_PopravniIspitStavka_OdjeljenjeStavkaId",
                table: "PopravniIspitStavka",
                newName: "IX_PopravniIspitStavka_DodjeljenPredmetId");

            migrationBuilder.AddForeignKey(
                name: "FK_PopravniIspitStavka_DodjeljenPredmet_DodjeljenPredmetId",
                table: "PopravniIspitStavka",
                column: "DodjeljenPredmetId",
                principalTable: "DodjeljenPredmet",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}

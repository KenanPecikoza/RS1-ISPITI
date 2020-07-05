using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RS1_PrakticniDioIspita_2017_01_24.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UpisUOdjeljenje_Odjeljenje_OdjeljenjeIdId",
                table: "UpisUOdjeljenje");

            migrationBuilder.DropIndex(
                name: "IX_UpisUOdjeljenje_OdjeljenjeIdId",
                table: "UpisUOdjeljenje");

            migrationBuilder.DropColumn(
                name: "OdjeljenjeIdId",
                table: "UpisUOdjeljenje");

            migrationBuilder.AddColumn<int>(
                name: "OdjeljenjeId",
                table: "UpisUOdjeljenje",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UpisUOdjeljenje_OdjeljenjeId",
                table: "UpisUOdjeljenje",
                column: "OdjeljenjeId");

            migrationBuilder.AddForeignKey(
                name: "FK_UpisUOdjeljenje_Odjeljenje_OdjeljenjeId",
                table: "UpisUOdjeljenje",
                column: "OdjeljenjeId",
                principalTable: "Odjeljenje",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UpisUOdjeljenje_Odjeljenje_OdjeljenjeId",
                table: "UpisUOdjeljenje");

            migrationBuilder.DropIndex(
                name: "IX_UpisUOdjeljenje_OdjeljenjeId",
                table: "UpisUOdjeljenje");

            migrationBuilder.DropColumn(
                name: "OdjeljenjeId",
                table: "UpisUOdjeljenje");

            migrationBuilder.AddColumn<int>(
                name: "OdjeljenjeIdId",
                table: "UpisUOdjeljenje",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UpisUOdjeljenje_OdjeljenjeIdId",
                table: "UpisUOdjeljenje",
                column: "OdjeljenjeIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_UpisUOdjeljenje_Odjeljenje_OdjeljenjeIdId",
                table: "UpisUOdjeljenje",
                column: "OdjeljenjeIdId",
                principalTable: "Odjeljenje",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

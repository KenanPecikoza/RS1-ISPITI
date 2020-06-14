using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RS1_Ispit_asp.net_core.Migrations
{
    public partial class _3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IspitId",
                table: "IspitStudent",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_IspitStudent_IspitId",
                table: "IspitStudent",
                column: "IspitId");

            migrationBuilder.AddForeignKey(
                name: "FK_IspitStudent_Ispit_IspitId",
                table: "IspitStudent",
                column: "IspitId",
                principalTable: "Ispit",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IspitStudent_Ispit_IspitId",
                table: "IspitStudent");

            migrationBuilder.DropIndex(
                name: "IX_IspitStudent_IspitId",
                table: "IspitStudent");

            migrationBuilder.DropColumn(
                name: "IspitId",
                table: "IspitStudent");
        }
    }
}

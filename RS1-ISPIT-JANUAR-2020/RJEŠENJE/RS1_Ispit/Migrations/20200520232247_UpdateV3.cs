using Microsoft.EntityFrameworkCore.Migrations;

namespace RS1_Ispit_asp.net_core.Migrations
{
    public partial class UpdateV3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Takmicenje_SkolaId",
                table: "Takmicenje",
                column: "SkolaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Takmicenje_Skola_SkolaId",
                table: "Takmicenje",
                column: "SkolaId",
                principalTable: "Skola",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Takmicenje_Skola_SkolaId",
                table: "Takmicenje");

            migrationBuilder.DropIndex(
                name: "IX_Takmicenje_SkolaId",
                table: "Takmicenje");
        }
    }
}

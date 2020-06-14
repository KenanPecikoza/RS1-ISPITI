using Microsoft.EntityFrameworkCore.Migrations;

namespace RS1_Ispit_asp.net_core.Migrations
{
    public partial class Updatev2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Takmicenje_PredajePredmet_PredajePredmetId",
                table: "Takmicenje");

            migrationBuilder.DropIndex(
                name: "IX_Takmicenje_PredajePredmetId",
                table: "Takmicenje");

            migrationBuilder.RenameColumn(
                name: "PredajePredmetId",
                table: "Takmicenje",
                newName: "SkolaId");

            migrationBuilder.AddColumn<int>(
                name: "PredmetId",
                table: "Takmicenje",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Razred",
                table: "Takmicenje",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Takmicenje_PredmetId",
                table: "Takmicenje",
                column: "PredmetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Takmicenje_Predmet_PredmetId",
                table: "Takmicenje",
                column: "PredmetId",
                principalTable: "Predmet",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Takmicenje_Predmet_PredmetId",
                table: "Takmicenje");

            migrationBuilder.DropIndex(
                name: "IX_Takmicenje_PredmetId",
                table: "Takmicenje");

            migrationBuilder.DropColumn(
                name: "PredmetId",
                table: "Takmicenje");

            migrationBuilder.DropColumn(
                name: "Razred",
                table: "Takmicenje");

            migrationBuilder.RenameColumn(
                name: "SkolaId",
                table: "Takmicenje",
                newName: "PredajePredmetId");

            migrationBuilder.CreateIndex(
                name: "IX_Takmicenje_PredajePredmetId",
                table: "Takmicenje",
                column: "PredajePredmetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Takmicenje_PredajePredmet_PredajePredmetId",
                table: "Takmicenje",
                column: "PredajePredmetId",
                principalTable: "PredajePredmet",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}

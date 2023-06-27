using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace User_Mgmt_Api.Migrations
{
    public partial class WeaponAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "weaponId",
                table: "Characters",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Weapons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Damage = table.Column<int>(type: "int", nullable: false),
                    characterId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weapons", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Characters_weaponId",
                table: "Characters",
                column: "weaponId",
                unique: true,
                filter: "[weaponId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Weapons_weaponId",
                table: "Characters",
                column: "weaponId",
                principalTable: "Weapons",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Weapons_weaponId",
                table: "Characters");

            migrationBuilder.DropTable(
                name: "Weapons");

            migrationBuilder.DropIndex(
                name: "IX_Characters_weaponId",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "weaponId",
                table: "Characters");
        }
    }
}

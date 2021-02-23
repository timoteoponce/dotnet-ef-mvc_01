using Microsoft.EntityFrameworkCore.Migrations;

namespace web_04_ef_sqlite.Migrations
{
    public partial class ManyToOneStudentsLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Students",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_LocationId",
                table: "Students",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Locations_LocationId",
                table: "Students",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Locations_LocationId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Students_LocationId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Students");
        }
    }
}

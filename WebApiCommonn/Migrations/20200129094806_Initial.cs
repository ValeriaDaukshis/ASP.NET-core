using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiCommon.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hives",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hives", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    HiveId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Hives_HiveId",
                        column: x => x.HiveId,
                        principalTable: "Hives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName", "Code", "HiveId" },
                values: new object[,]
                {
                    { 1, "Name 1", "12c25", null },
                    { 2, "Name 2", "4255kg", null },
                    { 3, "Name 3", "82ds5", null }
                });

            migrationBuilder.InsertData(
                table: "Hives",
                columns: new[] { "Id", "Address" },
                values: new object[,]
                {
                    { 1, "Address 1" },
                    { 2, "Address 2" },
                    { 3, "Address 3" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_HiveId",
                table: "Categories",
                column: "HiveId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Hives");
        }
    }
}

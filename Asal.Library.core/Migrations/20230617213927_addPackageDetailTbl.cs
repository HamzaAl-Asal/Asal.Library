using Microsoft.EntityFrameworkCore.Migrations;

namespace Asal.Library.core.Migrations
{
    public partial class addPackageDetailTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PackageDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NugetOrgLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PackageManagerCommand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReadMe = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageDetail", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PackageDetail");
        }
    }
}

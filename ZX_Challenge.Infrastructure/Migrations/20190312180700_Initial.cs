using GeoAPI.Geometries;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZX_Challenge.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ZX_Challenge");

            migrationBuilder.CreateTable(
                name: "PDV",
                schema: "ZX_Challenge",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TradingName = table.Column<string>(nullable: false),
                    OwnerName = table.Column<string>(nullable: false),
                    Document = table.Column<string>(nullable: false),
                    CoverageArea = table.Column<IMultiPolygon>(type: "geometry", nullable: false),
                    Address = table.Column<IPoint>(type: "geometry", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PDV", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PDV_Document",
                schema: "ZX_Challenge",
                table: "PDV",
                column: "Document",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PDV",
                schema: "ZX_Challenge");
        }
    }
}

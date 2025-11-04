using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoMvc.Migrations
{
    /// <inheritdoc />
    public partial class HeThongPhanPhoi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HeThongPhanPhoi",
                columns: table => new
                {
                    MaHTPP = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    TenHTPP = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeThongPhanPhoi", x => x.MaHTPP);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HeThongPhanPhoi");
        }
    }
}

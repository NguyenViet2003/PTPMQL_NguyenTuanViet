using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoMvc.Migrations
{
    /// <inheritdoc />
    public partial class AddDailyTableFinal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DaiLy",
                columns: table => new
                {
                    MaDaiLy = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    TenDaiLy = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    DiaChi = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    NguoiDaiDien = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    DienThoai = table.Column<string>(type: "TEXT", maxLength: 15, nullable: false),
                    MaHTPP = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DaiLy", x => x.MaDaiLy);
                    table.ForeignKey(
                        name: "FK_DaiLy_HeThongPhanPhoi_MaHTPP",
                        column: x => x.MaHTPP,
                        principalTable: "HeThongPhanPhoi",
                        principalColumn: "MaHTPP",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DaiLy_MaHTPP",
                table: "DaiLy",
                column: "MaHTPP");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DaiLy");
        }
    }
}

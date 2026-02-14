using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DMS.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NguoiDungs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VaiTro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhongBan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoiDungs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaiLieus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenTaiLieu = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Loai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhongBan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChuSoHuuId = table.Column<int>(type: "int", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DungLuong = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhienBan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaiLieus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaiLieus_NguoiDungs_ChuSoHuuId",
                        column: x => x.ChuSoHuuId,
                        principalTable: "NguoiDungs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThongBaos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TieuDe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TacGiaId = table.Column<int>(type: "int", nullable: false),
                    NgayDang = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChuyenMuc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MauSac = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThongBaos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThongBaos_NguoiDungs_TacGiaId",
                        column: x => x.TacGiaId,
                        principalTable: "NguoiDungs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TinNhans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NguoiGuiId = table.Column<int>(type: "int", nullable: false),
                    NguoiNhanId = table.Column<int>(type: "int", nullable: true),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThoiGian = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LaTinNhanNhom = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TinNhans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TinNhans_NguoiDungs_NguoiGuiId",
                        column: x => x.NguoiGuiId,
                        principalTable: "NguoiDungs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TinNhans_NguoiDungs_NguoiNhanId",
                        column: x => x.NguoiNhanId,
                        principalTable: "NguoiDungs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BinhLuans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ThongBaoId = table.Column<int>(type: "int", nullable: false),
                    TacGiaId = table.Column<int>(type: "int", nullable: false),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThoiGian = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BinhLuans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BinhLuans_NguoiDungs_TacGiaId",
                        column: x => x.TacGiaId,
                        principalTable: "NguoiDungs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BinhLuans_ThongBaos_ThongBaoId",
                        column: x => x.ThongBaoId,
                        principalTable: "ThongBaos",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "NguoiDungs",
                columns: new[] { "Id", "Email", "HoTen", "MatKhau", "PhongBan", "TrangThai", "VaiTro" },
                values: new object[,]
                {
                    { 1, "admin@company.com", "Administrator", "admin123", "IT", "active", "Admin" },
                    { 2, "a.nguyen@company.com", "Nguyen Van A", "user123", "Finance", "active", "User" },
                    { 3, "b.tran@company.com", "Tran Thi B", "user123", "HR", "active", "User" }
                });

            migrationBuilder.InsertData(
                table: "TaiLieus",
                columns: new[] { "Id", "ChuSoHuuId", "DungLuong", "Loai", "MoTa", "NgayTao", "PhienBan", "PhongBan", "TenTaiLieu", "TrangThai" },
                values: new object[,]
                {
                    { 1, 1, "2.5 MB", "PDF", "Finalized financial report for Q3 2025.", new DateTime(2026, 2, 8, 21, 17, 14, 43, DateTimeKind.Local).AddTicks(5053), "v2.1", "Finance", "Q3_Financial_Internal.pdf", "Published" },
                    { 2, 3, "1.2 MB", "DOCX", "Working draft for handbook update.", new DateTime(2026, 2, 8, 21, 17, 14, 43, DateTimeKind.Local).AddTicks(5071), "v0.8", "HR", "Employee_Handbook_Draft.docx", "Draft" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BinhLuans_TacGiaId",
                table: "BinhLuans",
                column: "TacGiaId");

            migrationBuilder.CreateIndex(
                name: "IX_BinhLuans_ThongBaoId",
                table: "BinhLuans",
                column: "ThongBaoId");

            migrationBuilder.CreateIndex(
                name: "IX_TaiLieus_ChuSoHuuId",
                table: "TaiLieus",
                column: "ChuSoHuuId");

            migrationBuilder.CreateIndex(
                name: "IX_ThongBaos_TacGiaId",
                table: "ThongBaos",
                column: "TacGiaId");

            migrationBuilder.CreateIndex(
                name: "IX_TinNhans_NguoiGuiId",
                table: "TinNhans",
                column: "NguoiGuiId");

            migrationBuilder.CreateIndex(
                name: "IX_TinNhans_NguoiNhanId",
                table: "TinNhans",
                column: "NguoiNhanId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BinhLuans");

            migrationBuilder.DropTable(
                name: "TaiLieus");

            migrationBuilder.DropTable(
                name: "TinNhans");

            migrationBuilder.DropTable(
                name: "ThongBaos");

            migrationBuilder.DropTable(
                name: "NguoiDungs");
        }
    }
}

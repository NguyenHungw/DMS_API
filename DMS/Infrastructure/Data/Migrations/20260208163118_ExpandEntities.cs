using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DMS.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ExpandEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TinNhans_NguoiDungs_NguoiGuiId",
                table: "TinNhans");

            migrationBuilder.DropForeignKey(
                name: "FK_TinNhans_NguoiDungs_NguoiNhanId",
                table: "TinNhans");

            migrationBuilder.DropIndex(
                name: "IX_TinNhans_NguoiNhanId",
                table: "TinNhans");

            migrationBuilder.DropColumn(
                name: "NguoiNhanId",
                table: "TinNhans");

            migrationBuilder.DropColumn(
                name: "ChuyenMuc",
                table: "ThongBaos");

            migrationBuilder.DropColumn(
                name: "MauSac",
                table: "ThongBaos");

            migrationBuilder.DropColumn(
                name: "Loai",
                table: "TaiLieus");

            migrationBuilder.DropColumn(
                name: "PhienBan",
                table: "TaiLieus");

            migrationBuilder.DropColumn(
                name: "PhongBan",
                table: "TaiLieus");

            migrationBuilder.DropColumn(
                name: "PhongBan",
                table: "NguoiDungs");

            migrationBuilder.DropColumn(
                name: "VaiTro",
                table: "NguoiDungs");

            migrationBuilder.RenameColumn(
                name: "LaTinNhanNhom",
                table: "TinNhans",
                newName: "DaDoc");

            migrationBuilder.AddColumn<int>(
                name: "CuocTroChuyenId",
                table: "TinNhans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ChuyenMucId",
                table: "ThongBaos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsPinned",
                table: "ThongBaos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "MoTa",
                table: "TaiLieus",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "LoaiTaiLieuId",
                table: "TaiLieus",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PhongBanId",
                table: "TaiLieus",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayTao",
                table: "NguoiDungs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "PhongBanId",
                table: "NguoiDungs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VaiTroId",
                table: "NguoiDungs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ChuyenMucs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenChuyenMuc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MauSac = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChuyenMucs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CuocTroChuyens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenCuocTroChuyen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LaNhom = table.Column<bool>(type: "bit", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CuocTroChuyens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoaiTaiLieus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLoai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiTaiLieus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NhatKyHoatDongs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NguoiDungId = table.Column<int>(type: "int", nullable: false),
                    HanhDong = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DoiTuong = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThoiGian = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChiTiet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IPAddress = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhatKyHoatDongs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NhatKyHoatDongs_NguoiDungs_NguoiDungId",
                        column: x => x.NguoiDungId,
                        principalTable: "NguoiDungs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhienBanTaiLieus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaiLieuId = table.Column<int>(type: "int", nullable: false),
                    SoPhienBan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DuongDanFile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiTaoId = table.Column<int>(type: "int", nullable: false),
                    GhiChuThayDoi = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhienBanTaiLieus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhienBanTaiLieus_NguoiDungs_NguoiTaoId",
                        column: x => x.NguoiTaoId,
                        principalTable: "NguoiDungs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PhienBanTaiLieus_TaiLieus_TaiLieuId",
                        column: x => x.TaiLieuId,
                        principalTable: "TaiLieus",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PhongBans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenPhongBan = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhongBans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VaiTros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenVaiTro = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaiTros", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ThanhVienCuocTroChuyens",
                columns: table => new
                {
                    CuocTroChuyenId = table.Column<int>(type: "int", nullable: false),
                    NguoiDungId = table.Column<int>(type: "int", nullable: false),
                    NgayThamGia = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThanhVienCuocTroChuyens", x => new { x.CuocTroChuyenId, x.NguoiDungId });
                    table.ForeignKey(
                        name: "FK_ThanhVienCuocTroChuyens_CuocTroChuyens_CuocTroChuyenId",
                        column: x => x.CuocTroChuyenId,
                        principalTable: "CuocTroChuyens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ThanhVienCuocTroChuyens_NguoiDungs_NguoiDungId",
                        column: x => x.NguoiDungId,
                        principalTable: "NguoiDungs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ChuyenMucs",
                columns: new[] { "Id", "MauSac", "MoTa", "TenChuyenMuc" },
                values: new object[,]
                {
                    { 1, "blue", null, "News" },
                    { 2, "orange", null, "Policy" }
                });

            migrationBuilder.InsertData(
                table: "LoaiTaiLieus",
                columns: new[] { "Id", "MoTa", "TenLoai" },
                values: new object[,]
                {
                    { 1, "Tài liệu Portable Document Format", "PDF" },
                    { 2, "Tài liệu Microsoft Word", "DOCX" }
                });

            migrationBuilder.UpdateData(
                table: "NguoiDungs",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "NgayTao", "PhongBanId", "TrangThai", "VaiTroId" },
                values: new object[] { new DateTime(2026, 2, 8, 23, 31, 18, 641, DateTimeKind.Local).AddTicks(3828), 1, "Active", 1 });

            migrationBuilder.UpdateData(
                table: "NguoiDungs",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "NgayTao", "PhongBanId", "TrangThai", "VaiTroId" },
                values: new object[] { new DateTime(2026, 2, 8, 23, 31, 18, 641, DateTimeKind.Local).AddTicks(3844), 2, "Active", 2 });

            migrationBuilder.UpdateData(
                table: "NguoiDungs",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "NgayTao", "PhongBanId", "TrangThai", "VaiTroId" },
                values: new object[] { new DateTime(2026, 2, 8, 23, 31, 18, 641, DateTimeKind.Local).AddTicks(3846), 3, "Active", 2 });

            migrationBuilder.InsertData(
                table: "PhongBans",
                columns: new[] { "Id", "MoTa", "TenPhongBan" },
                values: new object[,]
                {
                    { 1, "Phòng Công nghệ thông tin", "IT" },
                    { 2, "Phòng Tài chính", "Finance" },
                    { 3, "Phòng Nhân sự", "HR" }
                });

            migrationBuilder.UpdateData(
                table: "TaiLieus",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "LoaiTaiLieuId", "NgayTao", "PhongBanId" },
                values: new object[] { 1, new DateTime(2026, 2, 8, 23, 31, 18, 641, DateTimeKind.Local).AddTicks(3873), 2 });

            migrationBuilder.UpdateData(
                table: "TaiLieus",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "LoaiTaiLieuId", "NgayTao", "PhongBanId" },
                values: new object[] { 2, new DateTime(2026, 2, 8, 23, 31, 18, 641, DateTimeKind.Local).AddTicks(3877), 3 });

            migrationBuilder.InsertData(
                table: "VaiTros",
                columns: new[] { "Id", "MoTa", "TenVaiTro" },
                values: new object[,]
                {
                    { 1, "Quản trị hệ thống", "Admin" },
                    { 2, "Người dùng thông thường", "User" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TinNhans_CuocTroChuyenId",
                table: "TinNhans",
                column: "CuocTroChuyenId");

            migrationBuilder.CreateIndex(
                name: "IX_ThongBaos_ChuyenMucId",
                table: "ThongBaos",
                column: "ChuyenMucId");

            migrationBuilder.CreateIndex(
                name: "IX_TaiLieus_LoaiTaiLieuId",
                table: "TaiLieus",
                column: "LoaiTaiLieuId");

            migrationBuilder.CreateIndex(
                name: "IX_TaiLieus_PhongBanId",
                table: "TaiLieus",
                column: "PhongBanId");

            migrationBuilder.CreateIndex(
                name: "IX_NguoiDungs_PhongBanId",
                table: "NguoiDungs",
                column: "PhongBanId");

            migrationBuilder.CreateIndex(
                name: "IX_NguoiDungs_VaiTroId",
                table: "NguoiDungs",
                column: "VaiTroId");

            migrationBuilder.CreateIndex(
                name: "IX_NhatKyHoatDongs_NguoiDungId",
                table: "NhatKyHoatDongs",
                column: "NguoiDungId");

            migrationBuilder.CreateIndex(
                name: "IX_PhienBanTaiLieus_NguoiTaoId",
                table: "PhienBanTaiLieus",
                column: "NguoiTaoId");

            migrationBuilder.CreateIndex(
                name: "IX_PhienBanTaiLieus_TaiLieuId",
                table: "PhienBanTaiLieus",
                column: "TaiLieuId");

            migrationBuilder.CreateIndex(
                name: "IX_ThanhVienCuocTroChuyens_NguoiDungId",
                table: "ThanhVienCuocTroChuyens",
                column: "NguoiDungId");

            migrationBuilder.AddForeignKey(
                name: "FK_NguoiDungs_PhongBans_PhongBanId",
                table: "NguoiDungs",
                column: "PhongBanId",
                principalTable: "PhongBans",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NguoiDungs_VaiTros_VaiTroId",
                table: "NguoiDungs",
                column: "VaiTroId",
                principalTable: "VaiTros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaiLieus_LoaiTaiLieus_LoaiTaiLieuId",
                table: "TaiLieus",
                column: "LoaiTaiLieuId",
                principalTable: "LoaiTaiLieus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaiLieus_PhongBans_PhongBanId",
                table: "TaiLieus",
                column: "PhongBanId",
                principalTable: "PhongBans",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ThongBaos_ChuyenMucs_ChuyenMucId",
                table: "ThongBaos",
                column: "ChuyenMucId",
                principalTable: "ChuyenMucs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TinNhans_CuocTroChuyens_CuocTroChuyenId",
                table: "TinNhans",
                column: "CuocTroChuyenId",
                principalTable: "CuocTroChuyens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TinNhans_NguoiDungs_NguoiGuiId",
                table: "TinNhans",
                column: "NguoiGuiId",
                principalTable: "NguoiDungs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NguoiDungs_PhongBans_PhongBanId",
                table: "NguoiDungs");

            migrationBuilder.DropForeignKey(
                name: "FK_NguoiDungs_VaiTros_VaiTroId",
                table: "NguoiDungs");

            migrationBuilder.DropForeignKey(
                name: "FK_TaiLieus_LoaiTaiLieus_LoaiTaiLieuId",
                table: "TaiLieus");

            migrationBuilder.DropForeignKey(
                name: "FK_TaiLieus_PhongBans_PhongBanId",
                table: "TaiLieus");

            migrationBuilder.DropForeignKey(
                name: "FK_ThongBaos_ChuyenMucs_ChuyenMucId",
                table: "ThongBaos");

            migrationBuilder.DropForeignKey(
                name: "FK_TinNhans_CuocTroChuyens_CuocTroChuyenId",
                table: "TinNhans");

            migrationBuilder.DropForeignKey(
                name: "FK_TinNhans_NguoiDungs_NguoiGuiId",
                table: "TinNhans");

            migrationBuilder.DropTable(
                name: "ChuyenMucs");

            migrationBuilder.DropTable(
                name: "LoaiTaiLieus");

            migrationBuilder.DropTable(
                name: "NhatKyHoatDongs");

            migrationBuilder.DropTable(
                name: "PhienBanTaiLieus");

            migrationBuilder.DropTable(
                name: "PhongBans");

            migrationBuilder.DropTable(
                name: "ThanhVienCuocTroChuyens");

            migrationBuilder.DropTable(
                name: "VaiTros");

            migrationBuilder.DropTable(
                name: "CuocTroChuyens");

            migrationBuilder.DropIndex(
                name: "IX_TinNhans_CuocTroChuyenId",
                table: "TinNhans");

            migrationBuilder.DropIndex(
                name: "IX_ThongBaos_ChuyenMucId",
                table: "ThongBaos");

            migrationBuilder.DropIndex(
                name: "IX_TaiLieus_LoaiTaiLieuId",
                table: "TaiLieus");

            migrationBuilder.DropIndex(
                name: "IX_TaiLieus_PhongBanId",
                table: "TaiLieus");

            migrationBuilder.DropIndex(
                name: "IX_NguoiDungs_PhongBanId",
                table: "NguoiDungs");

            migrationBuilder.DropIndex(
                name: "IX_NguoiDungs_VaiTroId",
                table: "NguoiDungs");

            migrationBuilder.DropColumn(
                name: "CuocTroChuyenId",
                table: "TinNhans");

            migrationBuilder.DropColumn(
                name: "ChuyenMucId",
                table: "ThongBaos");

            migrationBuilder.DropColumn(
                name: "IsPinned",
                table: "ThongBaos");

            migrationBuilder.DropColumn(
                name: "LoaiTaiLieuId",
                table: "TaiLieus");

            migrationBuilder.DropColumn(
                name: "PhongBanId",
                table: "TaiLieus");

            migrationBuilder.DropColumn(
                name: "NgayTao",
                table: "NguoiDungs");

            migrationBuilder.DropColumn(
                name: "PhongBanId",
                table: "NguoiDungs");

            migrationBuilder.DropColumn(
                name: "VaiTroId",
                table: "NguoiDungs");

            migrationBuilder.RenameColumn(
                name: "DaDoc",
                table: "TinNhans",
                newName: "LaTinNhanNhom");

            migrationBuilder.AddColumn<int>(
                name: "NguoiNhanId",
                table: "TinNhans",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChuyenMuc",
                table: "ThongBaos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MauSac",
                table: "ThongBaos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "MoTa",
                table: "TaiLieus",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Loai",
                table: "TaiLieus",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhienBan",
                table: "TaiLieus",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhongBan",
                table: "TaiLieus",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhongBan",
                table: "NguoiDungs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VaiTro",
                table: "NguoiDungs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "NguoiDungs",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PhongBan", "TrangThai", "VaiTro" },
                values: new object[] { "IT", "active", "Admin" });

            migrationBuilder.UpdateData(
                table: "NguoiDungs",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PhongBan", "TrangThai", "VaiTro" },
                values: new object[] { "Finance", "active", "User" });

            migrationBuilder.UpdateData(
                table: "NguoiDungs",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "PhongBan", "TrangThai", "VaiTro" },
                values: new object[] { "HR", "active", "User" });

            migrationBuilder.UpdateData(
                table: "TaiLieus",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Loai", "NgayTao", "PhienBan", "PhongBan" },
                values: new object[] { "PDF", new DateTime(2026, 2, 8, 21, 17, 14, 43, DateTimeKind.Local).AddTicks(5053), "v2.1", "Finance" });

            migrationBuilder.UpdateData(
                table: "TaiLieus",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Loai", "NgayTao", "PhienBan", "PhongBan" },
                values: new object[] { "DOCX", new DateTime(2026, 2, 8, 21, 17, 14, 43, DateTimeKind.Local).AddTicks(5071), "v0.8", "HR" });

            migrationBuilder.CreateIndex(
                name: "IX_TinNhans_NguoiNhanId",
                table: "TinNhans",
                column: "NguoiNhanId");

            migrationBuilder.AddForeignKey(
                name: "FK_TinNhans_NguoiDungs_NguoiGuiId",
                table: "TinNhans",
                column: "NguoiGuiId",
                principalTable: "NguoiDungs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TinNhans_NguoiDungs_NguoiNhanId",
                table: "TinNhans",
                column: "NguoiNhanId",
                principalTable: "NguoiDungs",
                principalColumn: "Id");
        }
    }
}

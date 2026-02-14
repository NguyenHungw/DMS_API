using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DMS.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class FinalRequirementsSync : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccessLevel",
                table: "TaiLieus",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DanhMucId",
                table: "TaiLieus",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayCapNhat",
                table: "TaiLieus",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "ChiaSeTaiLieus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaiLieuId = table.Column<int>(type: "int", nullable: false),
                    NguoiDuocChiaSeId = table.Column<int>(type: "int", nullable: true),
                    PhongBanDuocChiaSeId = table.Column<int>(type: "int", nullable: true),
                    QuyenHan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayChiaSe = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiaSeTaiLieus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChiaSeTaiLieus_NguoiDungs_NguoiDuocChiaSeId",
                        column: x => x.NguoiDuocChiaSeId,
                        principalTable: "NguoiDungs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChiaSeTaiLieus_PhongBans_PhongBanDuocChiaSeId",
                        column: x => x.PhongBanDuocChiaSeId,
                        principalTable: "PhongBans",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChiaSeTaiLieus_TaiLieus_TaiLieuId",
                        column: x => x.TaiLieuId,
                        principalTable: "TaiLieus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DanhMucs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDanhMuc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DanhMucChaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhMucs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DanhMucs_DanhMucs_DanhMucChaId",
                        column: x => x.DanhMucChaId,
                        principalTable: "DanhMucs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuyenHans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenQuyenHan = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuyenHans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VaiTroQuyenHans",
                columns: table => new
                {
                    VaiTroId = table.Column<int>(type: "int", nullable: false),
                    QuyenHanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaiTroQuyenHans", x => new { x.VaiTroId, x.QuyenHanId });
                    table.ForeignKey(
                        name: "FK_VaiTroQuyenHans_QuyenHans_QuyenHanId",
                        column: x => x.QuyenHanId,
                        principalTable: "QuyenHans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VaiTroQuyenHans_VaiTros_VaiTroId",
                        column: x => x.VaiTroId,
                        principalTable: "VaiTros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "DanhMucs",
                columns: new[] { "Id", "DanhMucChaId", "MoTa", "TenDanhMuc" },
                values: new object[] { 1, null, "Thư mục gốc", "Công ty" });

            migrationBuilder.UpdateData(
                table: "VaiTros",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "MoTa", "TenVaiTro" },
                values: new object[] { "Toàn quyền hệ thống", "Administrator" });

            migrationBuilder.UpdateData(
                table: "VaiTros",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "MoTa", "TenVaiTro" },
                values: new object[] { "Quản lý tài liệu phòng ban", "Manager" });

            migrationBuilder.InsertData(
                table: "VaiTros",
                columns: new[] { "Id", "MoTa", "TenVaiTro" },
                values: new object[,]
                {
                    { 3, "Nhân viên", "Staff" },
                    { 4, "Chỉ xem", "Viewer" }
                });

            migrationBuilder.UpdateData(
                table: "NguoiDungs",
                keyColumn: "Id",
                keyValue: 1,
                column: "NgayTao",
                value: new DateTime(2026, 2, 9, 22, 54, 31, 517, DateTimeKind.Local).AddTicks(5154));

            migrationBuilder.UpdateData(
                table: "NguoiDungs",
                keyColumn: "Id",
                keyValue: 2,
                column: "NgayTao",
                value: new DateTime(2026, 2, 9, 22, 54, 31, 517, DateTimeKind.Local).AddTicks(5192));

            migrationBuilder.UpdateData(
                table: "NguoiDungs",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "NgayTao", "VaiTroId" },
                values: new object[] { new DateTime(2026, 2, 9, 22, 54, 31, 517, DateTimeKind.Local).AddTicks(5194), 3 });

            migrationBuilder.InsertData(
                table: "QuyenHans",
                columns: new[] { "Id", "TenQuyenHan" },
                values: new object[,]
                {
                    { 1, "View" },
                    { 2, "Upload" },
                    { 3, "Edit" },
                    { 4, "Delete" },
                    { 5, "Share" },
                    { 6, "Approve" }
                });

            migrationBuilder.UpdateData(
                table: "TaiLieus",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AccessLevel", "DanhMucId", "NgayCapNhat", "NgayTao", "TrangThai" },
                values: new object[] { "Internal", 3, new DateTime(2026, 2, 9, 22, 54, 31, 517, DateTimeKind.Local).AddTicks(5245), new DateTime(2026, 2, 9, 22, 54, 31, 517, DateTimeKind.Local).AddTicks(5244), "Approved" });

            migrationBuilder.UpdateData(
                table: "TaiLieus",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AccessLevel", "DanhMucId", "NgayCapNhat", "NgayTao" },
                values: new object[] { "Restricted", 2, new DateTime(2026, 2, 9, 22, 54, 31, 517, DateTimeKind.Local).AddTicks(5273), new DateTime(2026, 2, 9, 22, 54, 31, 517, DateTimeKind.Local).AddTicks(5272) });


            migrationBuilder.InsertData(
                table: "DanhMucs",
                columns: new[] { "Id", "DanhMucChaId", "MoTa", "TenDanhMuc" },
                values: new object[,]
                {
                    { 2, 1, "Quản lý hợp đồng", "Hợp đồng" },
                    { 3, 1, "Tài chính kế toán", "Báo cáo tài chính" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaiLieus_DanhMucId",
                table: "TaiLieus",
                column: "DanhMucId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiaSeTaiLieus_NguoiDuocChiaSeId",
                table: "ChiaSeTaiLieus",
                column: "NguoiDuocChiaSeId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiaSeTaiLieus_PhongBanDuocChiaSeId",
                table: "ChiaSeTaiLieus",
                column: "PhongBanDuocChiaSeId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiaSeTaiLieus_TaiLieuId",
                table: "ChiaSeTaiLieus",
                column: "TaiLieuId");

            migrationBuilder.CreateIndex(
                name: "IX_DanhMucs_DanhMucChaId",
                table: "DanhMucs",
                column: "DanhMucChaId");

            migrationBuilder.CreateIndex(
                name: "IX_VaiTroQuyenHans_QuyenHanId",
                table: "VaiTroQuyenHans",
                column: "QuyenHanId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaiLieus_DanhMucs_DanhMucId",
                table: "TaiLieus",
                column: "DanhMucId",
                principalTable: "DanhMucs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaiLieus_DanhMucs_DanhMucId",
                table: "TaiLieus");

            migrationBuilder.DropTable(
                name: "ChiaSeTaiLieus");

            migrationBuilder.DropTable(
                name: "DanhMucs");

            migrationBuilder.DropTable(
                name: "VaiTroQuyenHans");

            migrationBuilder.DropTable(
                name: "QuyenHans");

            migrationBuilder.DropIndex(
                name: "IX_TaiLieus_DanhMucId",
                table: "TaiLieus");

            migrationBuilder.DeleteData(
                table: "VaiTros",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "VaiTros",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "AccessLevel",
                table: "TaiLieus");

            migrationBuilder.DropColumn(
                name: "DanhMucId",
                table: "TaiLieus");

            migrationBuilder.DropColumn(
                name: "NgayCapNhat",
                table: "TaiLieus");

            migrationBuilder.UpdateData(
                table: "NguoiDungs",
                keyColumn: "Id",
                keyValue: 1,
                column: "NgayTao",
                value: new DateTime(2026, 2, 8, 23, 31, 18, 641, DateTimeKind.Local).AddTicks(3828));

            migrationBuilder.UpdateData(
                table: "NguoiDungs",
                keyColumn: "Id",
                keyValue: 2,
                column: "NgayTao",
                value: new DateTime(2026, 2, 8, 23, 31, 18, 641, DateTimeKind.Local).AddTicks(3844));

            migrationBuilder.UpdateData(
                table: "NguoiDungs",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "NgayTao", "VaiTroId" },
                values: new object[] { new DateTime(2026, 2, 8, 23, 31, 18, 641, DateTimeKind.Local).AddTicks(3846), 2 });

            migrationBuilder.UpdateData(
                table: "TaiLieus",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "NgayTao", "TrangThai" },
                values: new object[] { new DateTime(2026, 2, 8, 23, 31, 18, 641, DateTimeKind.Local).AddTicks(3873), "Published" });

            migrationBuilder.UpdateData(
                table: "TaiLieus",
                keyColumn: "Id",
                keyValue: 2,
                column: "NgayTao",
                value: new DateTime(2026, 2, 8, 23, 31, 18, 641, DateTimeKind.Local).AddTicks(3877));

            migrationBuilder.UpdateData(
                table: "VaiTros",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "MoTa", "TenVaiTro" },
                values: new object[] { "Quản trị hệ thống", "Admin" });

            migrationBuilder.UpdateData(
                table: "VaiTros",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "MoTa", "TenVaiTro" },
                values: new object[] { "Người dùng thông thường", "User" });
        }
    }
}

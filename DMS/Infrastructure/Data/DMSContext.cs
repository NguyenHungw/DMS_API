using Microsoft.EntityFrameworkCore;
using DMS.Domain.Entities;

namespace DMS.Infrastructure.Data
{
    public class DMSContext : DbContext
    {
        public DMSContext(DbContextOptions<DMSContext> options) : base(options) { }

        public DbSet<NguoiDung> NguoiDungs { get; set; }
        public DbSet<TaiLieu> TaiLieus { get; set; }
        public DbSet<ThongBao> ThongBaos { get; set; }
        public DbSet<BinhLuan> BinhLuans { get; set; }
        public DbSet<TinNhan> TinNhans { get; set; }
        public DbSet<ChuyenMuc> ChuyenMucs { get; set; }
        public DbSet<DanhMuc> DanhMucs { get; set; }
        public DbSet<CuocTroChuyen> CuocTroChuyens { get; set; }
        public DbSet<LoaiTaiLieu> LoaiTaiLieus { get; set; }
        public DbSet<NhatKyHoatDong> NhatKyHoatDongs { get; set; }
        public DbSet<PhienBanTaiLieu> PhienBanTaiLieus { get; set; }
        public DbSet<PhongBan> PhongBans { get; set; }
        public DbSet<ThanhVienCuocTroChuyen> ThanhVienCuocTroChuyens { get; set; }
        public DbSet<VaiTro> VaiTros { get; set; }
        public DbSet<QuyenHan> QuyenHans { get; set; }
        public DbSet<VaiTroQuyenHan> VaiTroQuyenHans { get; set; }
        public DbSet<ChiaSeTaiLieu> ChiaSeTaiLieus { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Cấu hình Composite Key cho ThanhVienCuocTroChuyen
            modelBuilder.Entity<ThanhVienCuocTroChuyen>()
                .HasKey(tv => new { tv.CuocTroChuyenId, tv.NguoiDungId });

            // Cấu hình Composite Key cho VaiTroQuyenHan
            modelBuilder.Entity<VaiTroQuyenHan>()
                .HasKey(vq => new { vq.VaiTroId, vq.QuyenHanId });

            // Cấu hình quan hệ phân cấp cho DanhMuc (Self-referencing)
            modelBuilder.Entity<DanhMuc>()
                .HasOne(d => d.DanhMucCha)
                .WithMany(d => d.DanhMucCon)
                .HasForeignKey(d => d.DanhMucChaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed data cho các bảng lookup
            modelBuilder.Entity<VaiTro>().HasData(
                new VaiTro { Id = 1, TenVaiTro = "Administrator", MoTa = "Toàn quyền hệ thống" },
                new VaiTro { Id = 2, TenVaiTro = "Manager", MoTa = "Quản lý tài liệu phòng ban" },
                new VaiTro { Id = 3, TenVaiTro = "Staff", MoTa = "Nhân viên" },
                new VaiTro { Id = 4, TenVaiTro = "Viewer", MoTa = "Chỉ xem" }
            );

            modelBuilder.Entity<QuyenHan>().HasData(
                new QuyenHan { Id = 1, TenQuyenHan = "View" },
                new QuyenHan { Id = 2, TenQuyenHan = "Upload" },
                new QuyenHan { Id = 3, TenQuyenHan = "Edit" },
                new QuyenHan { Id = 4, TenQuyenHan = "Delete" },
                new QuyenHan { Id = 5, TenQuyenHan = "Share" },
                new QuyenHan { Id = 6, TenQuyenHan = "Approve" }
            );

            modelBuilder.Entity<PhongBan>().HasData(
                new PhongBan { Id = 1, TenPhongBan = "IT", MoTa = "Phòng Công nghệ thông tin" },
                new PhongBan { Id = 2, TenPhongBan = "Finance", MoTa = "Phòng Tài chính" },
                new PhongBan { Id = 3, TenPhongBan = "HR", MoTa = "Phòng Nhân sự" }
            );

            modelBuilder.Entity<LoaiTaiLieu>().HasData(
                new LoaiTaiLieu { Id = 1, TenLoai = "PDF", MoTa = "Tài liệu Portable Document Format" },
                new LoaiTaiLieu { Id = 2, TenLoai = "DOCX", MoTa = "Tài liệu Microsoft Word" }
            );

            modelBuilder.Entity<ChuyenMuc>().HasData(
                new ChuyenMuc { Id = 1, TenChuyenMuc = "News", MauSac = "blue" },
                new ChuyenMuc { Id = 2, TenChuyenMuc = "Policy", MauSac = "orange" }
            );

            modelBuilder.Entity<DanhMuc>().HasData(
                new DanhMuc { Id = 1, TenDanhMuc = "Công ty", MoTa = "Thư mục gốc" },
                new DanhMuc { Id = 2, TenDanhMuc = "Hợp đồng", DanhMucChaId = 1, MoTa = "Quản lý hợp đồng" },
                new DanhMuc { Id = 3, TenDanhMuc = "Báo cáo tài chính", DanhMucChaId = 1, MoTa = "Tài chính kế toán" }
            );

            // Seed data cho NguoiDung với ID tham chiếu
            modelBuilder.Entity<NguoiDung>().HasData(
                new NguoiDung { Id = 1, HoTen = "Administrator", Email = "admin@company.com", MatKhau = "admin123", VaiTroId = 1, PhongBanId = 1, TrangThai = "Active" },
                new NguoiDung { Id = 2, HoTen = "Nguyen Van A", Email = "a.nguyen@company.com", MatKhau = "user123", VaiTroId = 2, PhongBanId = 2, TrangThai = "Active" },
                new NguoiDung { Id = 3, HoTen = "Tran Thi B", Email = "b.tran@company.com", MatKhau = "user123", VaiTroId = 3, PhongBanId = 3, TrangThai = "Active" }
            );

            // Seed data cho TaiLieu với ID tham chiếu
            modelBuilder.Entity<TaiLieu>().HasData(
                new TaiLieu { Id = 1, TenTaiLieu = "Q3_Financial_Internal.pdf", LoaiTaiLieuId = 1, PhongBanId = 2, ChuSoHuuId = 1, DanhMucId = 3, DungLuong = "2.5 MB", TrangThai = "Approved", AccessLevel = "Internal", MoTa = "Finalized financial report for Q3 2025." },
                new TaiLieu { Id = 2, TenTaiLieu = "Employee_Handbook_Draft.docx", LoaiTaiLieuId = 2, PhongBanId = 3, ChuSoHuuId = 3, DanhMucId = 2, DungLuong = "1.2 MB", TrangThai = "Draft", AccessLevel = "Restricted", MoTa = "Working draft for handbook update." }
            );

            // Xử lý vòng lặp cascade (multiple cascade paths)
            modelBuilder.Entity<BinhLuan>()
                .HasOne(b => b.ThongBao)
                .WithMany(t => t.DanhSachBinhLuan)
                .HasForeignKey(b => b.ThongBaoId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<BinhLuan>()
                .HasOne(b => b.TacGia)
                .WithMany()
                .HasForeignKey(b => b.TacGiaId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ThongBao>()
                .HasOne(t => t.TacGia)
                .WithMany()
                .HasForeignKey(t => t.TacGiaId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TinNhan>()
                .HasOne(m => m.NguoiGui)
                .WithMany()
                .HasForeignKey(m => m.NguoiGuiId)
                .OnDelete(DeleteBehavior.NoAction);

            // Cấu hình PhienBanTaiLieu để tránh vòng lặp cascade
            modelBuilder.Entity<PhienBanTaiLieu>()
                .HasOne(p => p.TaiLieu)
                .WithMany(t => t.DanhSachPhienBan)
                .HasForeignKey(p => p.TaiLieuId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PhienBanTaiLieu>()
                .HasOne(p => p.NguoiTao)
                .WithMany()
                .HasForeignKey(p => p.NguoiTaoId)
                .OnDelete(DeleteBehavior.NoAction);

            // Cấu hình ChiaSeTaiLieu để tránh vòng lặp cascade
            modelBuilder.Entity<ChiaSeTaiLieu>()
                .HasOne(s => s.TaiLieu)
                .WithMany(t => t.DanhSachChiaSe)
                .HasForeignKey(s => s.TaiLieuId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ChiaSeTaiLieu>()
                .HasOne(s => s.NguoiDuocChiaSe)
                .WithMany()
                .HasForeignKey(s => s.NguoiDuocChiaSeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ChiaSeTaiLieu>()
                .HasOne(s => s.PhongBanDuocChiaSe)
                .WithMany()
                .HasForeignKey(s => s.PhongBanDuocChiaSeId)
                .OnDelete(DeleteBehavior.NoAction);

            // Xử lý diamond dependency: TaiLieu -> PhongBan and TaiLieu -> NguoiDung -> PhongBan
            modelBuilder.Entity<TaiLieu>()
                .HasOne(t => t.PhongBan)
                .WithMany()
                .HasForeignKey(t => t.PhongBanId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<NguoiDung>()
                .HasOne(n => n.PhongBan)
                .WithMany(p => p.DanhSachNguoiDung)
                .HasForeignKey(n => n.PhongBanId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

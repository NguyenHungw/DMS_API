using DMS.Domain.Entities;
using DMS.API.DTOs;
using System.Linq;

namespace DMS.Application.Mappings
{
    public static class MappingExtensions
    {
        public static DanhMucDto ToDto(this DanhMuc entity)
        {
            if (entity == null) return null!;
            return new DanhMucDto
            {
                Id = entity.Id,
                TenDanhMuc = entity.TenDanhMuc,
                MoTa = entity.MoTa,
                DanhMucChaId = entity.DanhMucChaId,
                TenDanhMucCha = entity.DanhMucCha?.TenDanhMuc
            };
        }

        public static TaiLieuDto ToDto(this TaiLieu entity)
        {
            if (entity == null) return null!;
            return new TaiLieuDto
            {
                Id = entity.Id,
                TenTaiLieu = entity.TenTaiLieu,
                MoTa = entity.MoTa,
                DungLuong = entity.DungLuong,
                NgayTao = entity.NgayTao,
                TrangThai = entity.TrangThai,
                DanhMucId = entity.DanhMucId,
                TenDanhMuc = entity.DanhMuc?.TenDanhMuc,
                PhongBanId = entity.PhongBanId,
                TenPhongBan = entity.PhongBan?.TenPhongBan,
                ChuSoHuuId = entity.ChuSoHuuId,
                TenChuSoHuu = entity.ChuSoHuu?.HoTen,
                DuongDan = entity.DanhSachPhienBan?.OrderByDescending(v => v.NgayTao).FirstOrDefault()?.DuongDanFile,
                Versions = entity.DanhSachPhienBan?.Select(v => v.ToPhienBanDto()).ToList() ?? new List<PhienBanDto>()
            };
        }

        public static PhienBanDto ToPhienBanDto(this PhienBanTaiLieu entity)
        {
            if (entity == null) return null!;
            return new PhienBanDto
            {
                Id = entity.Id,
                SoPhienBan = entity.SoPhienBan,
                NgayTao = entity.NgayTao,
                NguoiTao = entity.NguoiTao?.HoTen
            };
        }

        public static NguoiDungDto ToDto(this NguoiDung entity)
        {
            if (entity == null) return null!;
            return new NguoiDungDto
            {
                Id = entity.Id,
                HoTen = entity.HoTen,
                Email = entity.Email,
                TrangThai = entity.TrangThai,
                VaiTroId = entity.VaiTroId,
                TenVaiTro = entity.VaiTro?.TenVaiTro,
                PhongBanId = entity.PhongBanId,
                TenPhongBan = entity.PhongBan?.TenPhongBan,
                QuyenHans = entity.VaiTro?.VaiTroQuyenHans?
                    .Select(vq => vq.QuyenHan?.TenQuyenHan ?? "")
                    .Where(n => !string.IsNullOrEmpty(n))
                    .ToList() ?? new List<string>(),
                HinhAnh = entity.HinhAnh
            };
        }
        public static ThongBaoDto ToDto(this ThongBao entity)
        {
            if (entity == null) return null!;
            return new ThongBaoDto
            {
                Id = entity.Id,
                TieuDe = entity.TieuDe,
                NoiDung = entity.NoiDung,
                NgayDang = entity.NgayDang,
                IsPinned = entity.IsPinned,
                ChuyenMucId = entity.ChuyenMucId,
                TenChuyenMuc = entity.ChuyenMuc?.TenChuyenMuc,
                TacGiaId = entity.TacGiaId,
                TenTacGia = entity.TacGia?.HoTen,
                DanhSachBinhLuan = entity.DanhSachBinhLuan?.Select(c => c.ToBinhLuanDto()).ToList() ?? new List<BinhLuanDto>()
            };
        }

        public static BinhLuanDto ToBinhLuanDto(this BinhLuan entity)
        {
            if (entity == null) return null!;
            return new BinhLuanDto
            {
                Id = entity.Id,
                ThongBaoId = entity.ThongBaoId,
                TacGiaId = entity.TacGiaId,
                TenNguoiDung = entity.TacGia?.HoTen ?? "Ẩn danh",
                NoiDung = entity.NoiDung,
                ThoiGian = entity.ThoiGian
            };
        }

        public static CuocTroChuyenDto ToDto(this CuocTroChuyen entity)
        {
            if (entity == null) return null!;
            return new CuocTroChuyenDto
            {
                Id = entity.Id,
                TenCuocTroChuyen = entity.TenCuocTroChuyen,
                NgayTao = entity.NgayTao,
                TenThanhVien = entity.DanhSachThanhVien?.Select(m => m.NguoiDung?.HoTen ?? "").ToList() ?? new List<string>(),
                TinNhanMoiNhat = entity.DanhSachTinNhan?.OrderByDescending(m => m.ThoiGian).Take(5).Select(m => m.ToDto()).ToList() ?? new List<TinNhanDto>()
            };
        }

        public static TinNhanDto ToDto(this TinNhan entity)
        {
            if (entity == null) return null!;
            return new TinNhanDto
            {
                Id = entity.Id,
                NoiDung = entity.NoiDung,
                ThoiGianGui = entity.ThoiGian,
                NguoiGuiId = entity.NguoiGuiId,
                TenNguoiGui = entity.NguoiGui?.HoTen
            };
        }

        public static ChiaSeDto ToDto(this ChiaSeTaiLieu entity)
        {
            if (entity == null) return null!;
            return new ChiaSeDto
            {
                Id = entity.Id,
                TaiLieuId = entity.TaiLieuId,
                TenTaiLieu = entity.TaiLieu?.TenTaiLieu,
                QuyenHan = entity.QuyenHan,
                NguoiDuocChiaSeId = entity.NguoiDuocChiaSeId,
                TenNguoiDuocChiaSe = entity.NguoiDuocChiaSe?.HoTen,
                PhongBanDuocChiaSeId = entity.PhongBanDuocChiaSeId,
                TenPhongBanDuocChiaSe = entity.PhongBanDuocChiaSe?.TenPhongBan
            };
        }
    }
}

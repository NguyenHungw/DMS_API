using System;
using System.Collections.Generic;

namespace DMS.API.DTOs
{
    public class TaiLieuDto
    {
        public int Id { get; set; }
        public string TenTaiLieu { get; set; } = string.Empty;
        public string? MoTa { get; set; }
        public string? DungLuong { get; set; }
        public DateTime NgayTao { get; set; }
        public string? TrangThai { get; set; }
        
        public int? DanhMucId { get; set; }
        public string? TenDanhMuc { get; set; }
        
        public int? PhongBanId { get; set; }
        public string? TenPhongBan { get; set; }
        
        public int? ChuSoHuuId { get; set; }
        public string? TenChuSoHuu { get; set; }
        
        public string? DuongDan { get; set; }
        
        public List<PhienBanDto> Versions { get; set; } = new List<PhienBanDto>();
    }

    public class PhienBanDto
    {
        public int Id { get; set; }
        public string SoPhienBan { get; set; } = string.Empty;
        public DateTime NgayTao { get; set; }
        public string? NguoiTao { get; set; }
    }
}

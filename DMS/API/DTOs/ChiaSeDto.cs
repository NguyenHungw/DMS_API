using System;

namespace DMS.API.DTOs
{
    public class ChiaSeDto
    {
        public int Id { get; set; }
        public int TaiLieuId { get; set; }
        public string? TenTaiLieu { get; set; }
        public string QuyenHan { get; set; } = string.Empty;
        
        public int? NguoiDuocChiaSeId { get; set; }
        public string? TenNguoiDuocChiaSe { get; set; }
        
        public int? PhongBanDuocChiaSeId { get; set; }
        public string? TenPhongBanDuocChiaSe { get; set; }
    }
}

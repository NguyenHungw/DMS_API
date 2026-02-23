using System;

namespace DMS.API.DTOs
{
    public class ThongBaoDto
    {
        public int Id { get; set; }
        public string TieuDe { get; set; } = string.Empty;
        public string? NoiDung { get; set; }
        public DateTime NgayDang { get; set; }
        public bool IsPinned { get; set; }
        
        public int? ChuyenMucId { get; set; }
        public string? TenChuyenMuc { get; set; }
        
        public int? TacGiaId { get; set; }
        public string? TenTacGia { get; set; }

        public List<BinhLuanDto> DanhSachBinhLuan { get; set; } = new();
    }
}

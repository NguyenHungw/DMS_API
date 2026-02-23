using System;

namespace DMS.API.DTOs
{
    public class BinhLuanDto
    {
        public int Id { get; set; }
        public int ThongBaoId { get; set; }
        public int TacGiaId { get; set; }
        public string TenNguoiDung { get; set; } = string.Empty;
        public string NoiDung { get; set; } = string.Empty;
        public string ThoiGian { get; set; } = string.Empty;
    }
}

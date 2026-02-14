using System;

namespace DMS.Domain.Entities
{
    public class ChiaSeTaiLieu
    {
        public int Id { get; set; }
        public int TaiLieuId { get; set; }
        public TaiLieu? TaiLieu { get; set; }

        public int? NguoiDuocChiaSeId { get; set; }
        public NguoiDung? NguoiDuocChiaSe { get; set; }

        public int? PhongBanDuocChiaSeId { get; set; }
        public PhongBan? PhongBanDuocChiaSe { get; set; }

        public string QuyenHan { get; set; } = "View"; // View, Download, Edit
        public DateTime NgayChiaSe { get; set; } = DateTime.Now;
    }
}

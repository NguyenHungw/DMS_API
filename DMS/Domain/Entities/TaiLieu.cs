using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DMS.Domain.Entities
{
    public class TaiLieu
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(255)]
        public string TenTaiLieu { get; set; } = string.Empty;
        
        public int LoaiTaiLieuId { get; set; }
        [ForeignKey("LoaiTaiLieuId")]
        public LoaiTaiLieu? LoaiTaiLieu { get; set; }
        
        public int PhongBanId { get; set; }
        [ForeignKey("PhongBanId")]
        public PhongBan? PhongBan { get; set; }
        
        public int ChuSoHuuId { get; set; }
        [ForeignKey("ChuSoHuuId")]
        public NguoiDung? ChuSoHuu { get; set; }
        
        public int? DanhMucId { get; set; }
        [ForeignKey("DanhMucId")]
        public DanhMuc? DanhMuc { get; set; }

        public DateTime NgayTao { get; set; } = DateTime.Now;
        public DateTime NgayCapNhat { get; set; } = DateTime.Now;
        
        public string DungLuong { get; set; } = "0 KB";
        
        public string TrangThai { get; set; } = "Draft"; // Draft (Nháp), Approved (Duyệt), Circulating (Lưu hành)
        
        public string AccessLevel { get; set; } = "Internal"; // Public, Internal, Restricted
        
        public string? MoTa { get; set; }
        
        public List<PhienBanTaiLieu> DanhSachPhienBan { get; set; } = new();
        public List<ChiaSeTaiLieu> DanhSachChiaSe { get; set; } = new();
    }
}

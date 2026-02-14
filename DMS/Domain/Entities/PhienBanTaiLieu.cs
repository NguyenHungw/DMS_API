using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DMS.Domain.Entities
{
    public class PhienBanTaiLieu
    {
        [Key]
        public int Id { get; set; }
        
        public int TaiLieuId { get; set; }
        [ForeignKey("TaiLieuId")]
        public TaiLieu? TaiLieu { get; set; }
        
        [Required]
        public string SoPhienBan { get; set; } = "1.0";
        
        [Required]
        public string DuongDanFile { get; set; } = string.Empty;
        
        public DateTime NgayTao { get; set; } = DateTime.Now;
        
        public int NguoiTaoId { get; set; }
        [ForeignKey("NguoiTaoId")]
        public NguoiDung? NguoiTao { get; set; }
        
        public string? GhiChuThayDoi { get; set; }
    }
}

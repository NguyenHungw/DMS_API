using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DMS.Domain.Entities
{
    public class NhatKyHoatDong
    {
        [Key]
        public int Id { get; set; }
        
        public int NguoiDungId { get; set; }
        [ForeignKey("NguoiDungId")]
        public NguoiDung? NguoiDung { get; set; }
        
        [Required]
        public string HanhDong { get; set; } = string.Empty; // Create, Update, Delete, Login, Download
        
        public string DoiTuong { get; set; } = string.Empty; // TaiLieu, ThongBao, NguoiDung
        
        public DateTime ThoiGian { get; set; } = DateTime.Now;
        
        public string? ChiTiet { get; set; }
        
        public string? IPAddress { get; set; }
    }
}

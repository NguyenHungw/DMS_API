using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DMS.Domain.Entities
{
    public class ThongBao
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string TieuDe { get; set; } = string.Empty;
        
        [Required]
        public string NoiDung { get; set; } = string.Empty;
        
        public int TacGiaId { get; set; }
        [ForeignKey("TacGiaId")]
        public NguoiDung? TacGia { get; set; }
        
        public DateTime NgayDang { get; set; } = DateTime.Now;
        
        public int ChuyenMucId { get; set; }
        [ForeignKey("ChuyenMucId")]
        public ChuyenMuc? ChuyenMuc { get; set; }
        
        public bool IsPinned { get; set; } = false;
        
        public List<BinhLuan> DanhSachBinhLuan { get; set; } = new();
    }
}

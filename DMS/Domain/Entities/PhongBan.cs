using System.ComponentModel.DataAnnotations;

namespace DMS.Domain.Entities
{
    public class PhongBan
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string TenPhongBan { get; set; } = string.Empty;
        
        public string? MoTa { get; set; }
        
        public List<NguoiDung> DanhSachNguoiDung { get; set; } = new();
    }
}

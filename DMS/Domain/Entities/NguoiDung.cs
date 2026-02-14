using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DMS.Domain.Entities
{
    public class NguoiDung
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string HoTen { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        public string MatKhau { get; set; } = string.Empty;
        
        public int VaiTroId { get; set; }
        [ForeignKey("VaiTroId")]
        public VaiTro? VaiTro { get; set; }
        
        public int PhongBanId { get; set; }
        [ForeignKey("PhongBanId")]
        public PhongBan? PhongBan { get; set; }
        
        public string TrangThai { get; set; } = "Active"; // Active, Inactive, Blocked
        
        public DateTime NgayTao { get; set; } = DateTime.Now;
    }
}

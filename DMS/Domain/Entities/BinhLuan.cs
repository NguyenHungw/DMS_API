using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DMS.Domain.Entities
{
    public class BinhLuan
    {
        [Key]
        public int Id { get; set; }
        
        public int ThongBaoId { get; set; }
        
        [ForeignKey("ThongBaoId")]
        public ThongBao? ThongBao { get; set; }
        
        public int TacGiaId { get; set; }
        
        [ForeignKey("TacGiaId")]
        public NguoiDung? TacGia { get; set; }
        
        [Required]
        public string NoiDung { get; set; } = string.Empty;
        
        public string ThoiGian { get; set; } = string.Empty;
    }
}

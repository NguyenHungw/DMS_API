using System.ComponentModel.DataAnnotations;

namespace DMS.Domain.Entities
{
    public class LoaiTaiLieu
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string TenLoai { get; set; } = string.Empty; // PDF, DOCX, XLSX, MP4, etc.
        
        public string? MoTa { get; set; }
    }
}

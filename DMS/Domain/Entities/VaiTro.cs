using System.ComponentModel.DataAnnotations;

namespace DMS.Domain.Entities
{
    public class VaiTro
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string TenVaiTro { get; set; } = string.Empty; // Admin, Manager, User, Guest
        
        public string? MoTa { get; set; }
    }
}

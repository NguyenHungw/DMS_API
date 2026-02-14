using System.ComponentModel.DataAnnotations;

namespace DMS.Domain.Entities
{
    public class ChuyenMuc
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string TenChuyenMuc { get; set; } = string.Empty; // News, Policy, Event
        
        public string MauSac { get; set; } = "blue";
        
        public string? MoTa { get; set; }
    }
}

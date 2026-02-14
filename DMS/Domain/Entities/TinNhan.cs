using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DMS.Domain.Entities
{
    public class TinNhan
    {
        [Key]
        public int Id { get; set; }
        
        public int CuocTroChuyenId { get; set; }
        [ForeignKey("CuocTroChuyenId")]
        public CuocTroChuyen? CuocTroChuyen { get; set; }
        
        public int NguoiGuiId { get; set; }
        [ForeignKey("NguoiGuiId")]
        public NguoiDung? NguoiGui { get; set; }
        
        [Required]
        public string NoiDung { get; set; } = string.Empty;
        
        public DateTime ThoiGian { get; set; } = DateTime.Now;
        
        public bool DaDoc { get; set; } = false;
    }
}

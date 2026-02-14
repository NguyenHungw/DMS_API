using System.ComponentModel.DataAnnotations;

namespace DMS.Domain.Entities
{
    public class CuocTroChuyen
    {
        [Key]
        public int Id { get; set; }
        
        public string? TenCuocTroChuyen { get; set; } // Group name or null for 1-1
        
        public bool LaNhom { get; set; } = false;
        
        public DateTime NgayTao { get; set; } = DateTime.Now;
        
        public List<ThanhVienCuocTroChuyen> DanhSachThanhVien { get; set; } = new();
        public List<TinNhan> DanhSachTinNhan { get; set; } = new();
    }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace DMS.Domain.Entities
{
    public class ThanhVienCuocTroChuyen
    {
        public int CuocTroChuyenId { get; set; }
        [ForeignKey("CuocTroChuyenId")]
        public CuocTroChuyen? CuocTroChuyen { get; set; }
        
        public int NguoiDungId { get; set; }
        [ForeignKey("NguoiDungId")]
        public NguoiDung? NguoiDung { get; set; }
        
        public DateTime NgayThamGia { get; set; } = DateTime.Now;
    }
}

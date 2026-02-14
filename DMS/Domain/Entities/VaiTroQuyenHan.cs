namespace DMS.Domain.Entities
{
    public class VaiTroQuyenHan
    {
        public int VaiTroId { get; set; }
        public VaiTro? VaiTro { get; set; }

        public int QuyenHanId { get; set; }
        public QuyenHan? QuyenHan { get; set; }
    }
}

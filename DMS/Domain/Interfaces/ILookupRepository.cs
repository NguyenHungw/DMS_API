using DMS.Domain.Entities;

namespace DMS.Domain.Interfaces
{
    public interface ILookupRepository
    {
        Task<IEnumerable<PhongBan>> LayTatCaPhongBan();
        Task<IEnumerable<VaiTro>> LayTatCaVaiTro();
        Task<IEnumerable<LoaiTaiLieu>> LayTatCaLoaiTaiLieu();
        Task<IEnumerable<ChuyenMuc>> LayTatCaChuyenMuc();
        Task<IEnumerable<DanhMuc>> LayTatCaDanhMuc();
        Task<IEnumerable<QuyenHan>> LayTatCaQuyenHan();
    }
}

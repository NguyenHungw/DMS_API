using DMS.Domain.Entities;

namespace DMS.Domain.Interfaces
{
    public interface IChiaSeRepository : IGenericRepository<ChiaSeTaiLieu>
    {
        Task<IEnumerable<ChiaSeTaiLieu>> LayDanhSachChiaSe(int taiLieuId);
        Task ChiaSe(ChiaSeTaiLieu chiaSe);
        Task ThuHoiChiaSe(int chiaSeId);
        Task<bool> KiemTraQuyenTruyCap(int taiLieuId, int nguoiDungId, string quyenYeuCau);
    }
}

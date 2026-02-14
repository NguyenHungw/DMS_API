using DMS.Domain.Entities;

namespace DMS.Domain.Interfaces
{
    public interface ITaiLieuRepository : IGenericRepository<TaiLieu>
    {
        Task<IEnumerable<TaiLieu>> LayTatCaVoiChiTiet();
        Task<TaiLieu?> LayTheoIdVoiChiTiet(int id);
        Task<IEnumerable<TaiLieu>> LayTheoPhongBan(int phongBanId);
        Task Them(TaiLieu taiLieu);
        Task CapNhat(TaiLieu taiLieu);
        Task Xoa(int id);
        
        // Versioning
        Task ThemPhienBan(PhienBanTaiLieu phienBan);
    }
}

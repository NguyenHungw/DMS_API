using DMS.Domain.Entities;

namespace DMS.Domain.Interfaces
{
    public interface IThongBaoRepository : IGenericRepository<ThongBao>
    {
        Task<IEnumerable<ThongBao>> LayTatCaVoiChiTiet();
        Task<ThongBao?> LayTheoIdVoiChiTiet(int id);
        Task Them(ThongBao thongBao);
        Task Xoa(int id);
        
        // Categories
        Task<IEnumerable<ChuyenMuc>> LayTatCaChuyenMuc();
    }
}

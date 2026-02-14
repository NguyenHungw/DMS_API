using DMS.Domain.Entities;

namespace DMS.Domain.Interfaces
{
    public interface INguoiDungRepository : IGenericRepository<NguoiDung>
    {
        Task<IEnumerable<NguoiDung>> LayTatCaVoiChiTiet();
        Task<NguoiDung?> LayTheoIdVoiChiTiet(int id);
        Task<NguoiDung?> LayTheoEmail(string email);
        Task ThemNguoiDung(NguoiDung nguoiDung);
        Task CapNhat(NguoiDung nguoiDung);
        Task Xoa(int id);
    }
}

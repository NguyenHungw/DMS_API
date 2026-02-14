using DMS.Domain.Entities;

namespace DMS.Domain.Interfaces
{
    public interface IXacThucRepository : IGenericRepository<NguoiDung>
    {
        Task<NguoiDung?> LayNguoiDungTheoEmail(string email);
        Task LuuNguoiDung(NguoiDung nguoiDung);
        Task<bool> KiemTraPhongBan(int id);
        Task<bool> KiemTraVaiTro(int id);
    }
}

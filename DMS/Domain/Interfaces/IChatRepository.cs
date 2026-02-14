using DMS.Domain.Entities;

namespace DMS.Domain.Interfaces
{
    public interface IChatRepository : IGenericRepository<CuocTroChuyen>
    {
        Task<IEnumerable<CuocTroChuyen>> LayCuocTroChuyenCuaUser(int userId);
        Task<CuocTroChuyen?> LayChiTietCuocTroChuyen(int id);
        Task ThemTinNhan(TinNhan tinNhan);
        Task TaoCuocTroChuyen(CuocTroChuyen cuocTroChuyen);
    }
}

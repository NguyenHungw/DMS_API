using DMS.Domain.Entities;

namespace DMS.Domain.Interfaces
{
    public interface INhatKyRepository : IGenericRepository<NhatKyHoatDong>
    {
        Task LuuLog(NhatKyHoatDong log);
        Task<IEnumerable<NhatKyHoatDong>> LayNhatKy();
    }
}

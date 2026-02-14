using DMS.Domain.Entities;
using DMS.Domain.Interfaces;
using DMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DMS.Infrastructure.Repositories
{
    public class NhatKyRepository : GenericRepository<NhatKyHoatDong>, INhatKyRepository
    {
        public NhatKyRepository(DMSContext context) : base(context) { }

        public async Task LuuLog(NhatKyHoatDong log) => await AddAsync(log);

        public async Task<IEnumerable<NhatKyHoatDong>> LayNhatKy()
        {
            return await _dbSet
                .Include(l => l.NguoiDung)
                .OrderByDescending(l => l.ThoiGian)
                .ToListAsync();
        }
    }
}

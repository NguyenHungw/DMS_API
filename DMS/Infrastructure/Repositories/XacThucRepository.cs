using Microsoft.EntityFrameworkCore;
using DMS.Domain.Entities;
using DMS.Domain.Interfaces;
using DMS.Infrastructure.Data;

namespace DMS.Infrastructure.Repositories
{
    public class XacThucRepository : GenericRepository<NguoiDung>, IXacThucRepository
    {
        public XacThucRepository(DMSContext context) : base(context) { }

        public async Task<NguoiDung?> LayNguoiDungTheoEmail(string email) => 
            await _dbSet
                .Include(u => u.VaiTro)
                .FirstOrDefaultAsync(u => u.Email == email);

        public async Task LuuNguoiDung(NguoiDung nguoiDung) => await AddAsync(nguoiDung);

        public async Task<bool> KiemTraPhongBan(int id) => await _context.PhongBans.AnyAsync(p => p.Id == id);
        public async Task<bool> KiemTraVaiTro(int id) => await _context.VaiTros.AnyAsync(v => v.Id == id);
    }
}

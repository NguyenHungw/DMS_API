using Microsoft.EntityFrameworkCore;
using DMS.Domain.Entities;
using DMS.Domain.Interfaces;
using DMS.Infrastructure.Data;

namespace DMS.Infrastructure.Repositories
{
    public class ChatRepository : GenericRepository<CuocTroChuyen>, IChatRepository
    {
        public ChatRepository(DMSContext context) : base(context) { }

        public async Task<IEnumerable<CuocTroChuyen>> LayCuocTroChuyenCuaUser(int userId) => 
            await _dbSet
                .Include(c => c.DanhSachThanhVien)
                    .ThenInclude(m => m.NguoiDung)
                .Where(c => c.DanhSachThanhVien.Any(m => m.NguoiDungId == userId))
                .ToListAsync();

        public async Task<CuocTroChuyen?> LayChiTietCuocTroChuyen(int id) => 
            await _dbSet
                .Include(c => c.DanhSachTinNhan)
                    .ThenInclude(m => m.NguoiGui)
                .FirstOrDefaultAsync(c => c.Id == id);

        public async Task ThemTinNhan(TinNhan tinNhan)
        {
            await _context.TinNhans.AddAsync(tinNhan);
            await SaveChangesAsync();
        }

        public async Task TaoCuocTroChuyen(CuocTroChuyen cuocTroChuyen) => await AddAsync(cuocTroChuyen);
    }
}

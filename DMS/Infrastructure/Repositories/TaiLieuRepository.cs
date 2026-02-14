using Microsoft.EntityFrameworkCore;
using DMS.Domain.Entities;
using DMS.Domain.Interfaces;
using DMS.Infrastructure.Data;

namespace DMS.Infrastructure.Repositories
{
    public class TaiLieuRepository : GenericRepository<TaiLieu>, ITaiLieuRepository
    {
        public TaiLieuRepository(DMSContext context) : base(context) { }

        public async Task<IEnumerable<TaiLieu>> LayTatCaVoiChiTiet() => 
            await _dbSet
                .Include(t => t.LoaiTaiLieu)
                .Include(t => t.PhongBan)
                .Include(t => t.ChuSoHuu)
                .Include(t => t.DanhSachPhienBan)
                .ToListAsync();
        
        public async Task<TaiLieu?> LayTheoIdVoiChiTiet(int id) => 
            await _dbSet
                .Include(t => t.LoaiTaiLieu)
                .Include(t => t.PhongBan)
                .Include(t => t.ChuSoHuu)
                .Include(t => t.DanhSachPhienBan)
                .FirstOrDefaultAsync(t => t.Id == id);
        
        public async Task<IEnumerable<TaiLieu>> LayTheoPhongBan(int phongBanId) => 
            await _dbSet
                .Where(t => t.PhongBanId == phongBanId)
                .Include(t => t.LoaiTaiLieu)
                .ToListAsync();
        
        public async Task Them(TaiLieu taiLieu) => await AddAsync(taiLieu);
        
        public async Task CapNhat(TaiLieu taiLieu)
        {
            Update(taiLieu);
            await SaveChangesAsync();
        }

        public async Task Xoa(int id)
        {
            var doc = await GetByIdAsync(id);
            if (doc != null)
            {
                Delete(doc);
                await SaveChangesAsync();
            }
        }

        public async Task ThemPhienBan(PhienBanTaiLieu phienBan)
        {
            await _context.PhienBanTaiLieus.AddAsync(phienBan);
            await SaveChangesAsync();
        }
    }
}

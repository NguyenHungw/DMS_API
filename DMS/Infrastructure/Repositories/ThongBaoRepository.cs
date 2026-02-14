using Microsoft.EntityFrameworkCore;
using DMS.Domain.Entities;
using DMS.Domain.Interfaces;
using DMS.Infrastructure.Data;

namespace DMS.Infrastructure.Repositories
{
    public class ThongBaoRepository : GenericRepository<ThongBao>, IThongBaoRepository
    {
        public ThongBaoRepository(DMSContext context) : base(context) { }

        public async Task<IEnumerable<ThongBao>> LayTatCaVoiChiTiet() => 
            await _dbSet
                .Include(t => t.ChuyenMuc)
                .Include(t => t.TacGia)
                .OrderByDescending(t => t.IsPinned)
                .ThenByDescending(t => t.NgayDang)
                .ToListAsync();

        public async Task<ThongBao?> LayTheoIdVoiChiTiet(int id) => 
            await _dbSet
                .Include(t => t.ChuyenMuc)
                .Include(t => t.TacGia)
                .Include(t => t.DanhSachBinhLuan)
                    .ThenInclude(c => c.TacGia)
                .FirstOrDefaultAsync(t => t.Id == id);

        public async Task Them(ThongBao thongBao) => await AddAsync(thongBao);

        public async Task Xoa(int id)
        {
            var news = await GetByIdAsync(id);
            if (news != null)
            {
                Delete( news);
                await SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ChuyenMuc>> LayTatCaChuyenMuc() => 
            await _context.ChuyenMucs.ToListAsync();
    }
}

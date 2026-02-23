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
            var news = await LayTheoIdVoiChiTiet(id);
            if (news != null)
            {
                // Xóa tất cả bình luận liên quan trước để tránh lỗi FK constraint
                if (news.DanhSachBinhLuan != null && news.DanhSachBinhLuan.Any())
                {
                    _context.BinhLuans.RemoveRange(news.DanhSachBinhLuan);
                }

                _dbSet.Remove(news);
                await SaveChangesAsync();
            }
        }

        public async Task XoaBinhLuan(int id)
        {
            var comment = await _context.BinhLuans.FindAsync(id);
            if (comment != null)
            {
                _context.BinhLuans.Remove(comment);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ChuyenMuc>> LayTatCaChuyenMuc() => 
            await _context.ChuyenMucs.ToListAsync();
    }
}

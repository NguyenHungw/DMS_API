using DMS.Domain.Entities;
using DMS.Domain.Interfaces;
using DMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DMS.Infrastructure.Repositories
{
    public class ChiaSeRepository : GenericRepository<ChiaSeTaiLieu>, IChiaSeRepository
    {
        public ChiaSeRepository(DMSContext context) : base(context) { }

        public async Task<IEnumerable<ChiaSeTaiLieu>> LayDanhSachChiaSe(int taiLieuId)
        {
            return await _dbSet
                .Include(s => s.NguoiDuocChiaSe)
                .Include(s => s.PhongBanDuocChiaSe)
                .Where(s => s.TaiLieuId == taiLieuId)
                .ToListAsync();
        }

        public async Task ChiaSe(ChiaSeTaiLieu chiaSe) => await AddAsync(chiaSe);

        public async Task ThuHoiChiaSe(int chiaSeId)
        {
            var share = await GetByIdAsync(chiaSeId);
            if (share != null)
            {
                Delete(share);
                await SaveChangesAsync();
            }
        }

        public async Task<bool> KiemTraQuyenTruyCap(int taiLieuId, int nguoiDungId, string quyenYeuCau)
        {
            // Kiểm tra xem user có phải chủ sở hữu không
            var taiLieu = await _context.TaiLieus.FindAsync(taiLieuId);
            if (taiLieu != null && taiLieu.ChuSoHuuId == nguoiDungId) return true;

            // Kiểm tra trong bảng chia sẻ cho cá nhân
            var shareUser = await _context.ChiaSeTaiLieus
                .AnyAsync(s => s.TaiLieuId == taiLieuId && s.NguoiDuocChiaSeId == nguoiDungId && (s.QuyenHan == quyenYeuCau || quyenYeuCau == "View"));
            
            if (shareUser) return true;

            // Kiểm tra chia sẻ cho phòng ban của user
            var user = await _context.NguoiDungs.FindAsync(nguoiDungId);
            if (user != null)
            {
                var shareDept = await _context.ChiaSeTaiLieus
                    .AnyAsync(s => s.TaiLieuId == taiLieuId && s.PhongBanDuocChiaSeId == user.PhongBanId && (s.QuyenHan == quyenYeuCau || quyenYeuCau == "View"));
                
                if (shareDept) return true;
            }

            return false;
        }
    }
}

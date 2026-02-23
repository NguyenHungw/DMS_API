using Microsoft.EntityFrameworkCore;
using DMS.Domain.Entities;
using DMS.Domain.Interfaces;
using DMS.Infrastructure.Data;

namespace DMS.Infrastructure.Repositories
{
    public class NguoiDungRepository : GenericRepository<NguoiDung>, INguoiDungRepository
    {
        public NguoiDungRepository(DMSContext context) : base(context) { }

        public async Task<IEnumerable<NguoiDung>> LayTatCaVoiChiTiet() => 
            await _dbSet
                .Include(u => u.VaiTro)
                    .ThenInclude(v => v!.VaiTroQuyenHans)
                    .ThenInclude(vq => vq.QuyenHan)
                .Include(u => u.PhongBan)
                .ToListAsync();
        
        public async Task<NguoiDung?> LayTheoIdVoiChiTiet(int id) => 
            await _dbSet
                .Include(u => u.VaiTro)
                    .ThenInclude(v => v!.VaiTroQuyenHans)
                    .ThenInclude(vq => vq.QuyenHan)
                .Include(u => u.PhongBan)
                .FirstOrDefaultAsync(u => u.Id == id);
        
        public async Task<NguoiDung?> LayTheoEmail(string email) => 
            await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
        
        public async Task ThemNguoiDung(NguoiDung nguoiDung) => await AddAsync(nguoiDung);

        public async Task CapNhat(NguoiDung nguoiDung)
        {
            Update(nguoiDung);
            await SaveChangesAsync();
        }

        public async Task Xoa(int id)
        {
            var user = await GetByIdAsync(id);
            if (user != null)
            {
                Delete(user);
                await SaveChangesAsync();
            }
        }
    }
}

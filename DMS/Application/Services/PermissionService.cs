using DMS.Domain.Entities;
using DMS.Domain.Interfaces;
using DMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DMS.Application.Services
{
    public class PermissionService
    {
        private readonly DMSContext _context;
        public PermissionService(DMSContext context) => _context = context;

        public async Task<bool> CheckPermission(int userId, string permissionName)
        {
            var user = await _context.NguoiDungs
                .Include(u => u.VaiTro)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null || user.VaiTroId == 0) return false;

            // Admin luôn có mọi quyền
            if (user.VaiTro?.TenVaiTro == "Administrator" || user.VaiTroId == 1) return true;

            // Kiểm tra trong bảng VaiTroQuyenHan
            return await _context.VaiTroQuyenHans
                .AnyAsync(vp => vp.VaiTroId == user.VaiTroId && vp.QuyenHan!.TenQuyenHan == permissionName);
        }
    }
}

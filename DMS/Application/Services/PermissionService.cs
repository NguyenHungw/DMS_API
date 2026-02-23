using DMS.Domain.Entities;
using DMS.Domain.Interfaces;
using DMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using DMS.API.DTOs;

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

        public async Task<List<RolePermissionDto>> GetRolesWithPermissionsAsync()
        {
            var roles = await _context.VaiTros.ToListAsync();
            var rolePermissions = await _context.VaiTroQuyenHans
                .Include(vp => vp.QuyenHan)
                .ToListAsync();

            return roles.Select(r => new RolePermissionDto
            {
                RoleId = r.Id,
                RoleName = r.TenVaiTro,
                Permissions = rolePermissions
                    .Where(vp => vp.VaiTroId == r.Id)
                    .Select(vp => new PermissionDto
                    {
                        Id = vp.QuyenHanId,
                        TenQuyenHan = vp.QuyenHan?.TenQuyenHan ?? ""
                    }).ToList()
            }).ToList();
        }

        public async Task<List<PermissionDto>> GetAllPermissionsAsync()
        {
            return await _context.QuyenHans
                .Select(p => new PermissionDto { Id = p.Id, TenQuyenHan = p.TenQuyenHan })
                .ToListAsync();
        }

        public async Task<bool> AssignPermissionsToRoleAsync(AssignPermissionDto dto)
        {
            var role = await _context.VaiTros.FindAsync(dto.RoleId);
            if (role == null) return false;

            // Xóa các quyền cũ
            var existing = _context.VaiTroQuyenHans.Where(vp => vp.VaiTroId == dto.RoleId);
            _context.VaiTroQuyenHans.RemoveRange(existing);

            // Thêm quyền mới
            foreach (var pId in dto.PermissionIds)
            {
                _context.VaiTroQuyenHans.Add(new VaiTroQuyenHan
                {
                    VaiTroId = dto.RoleId,
                    QuyenHanId = pId
                });
            }

            return await _context.SaveChangesAsync() > 0;
        }
    }
}

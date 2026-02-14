using Microsoft.EntityFrameworkCore;
using DMS.Domain.Entities;
using DMS.Domain.Interfaces;
using DMS.Infrastructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DMS.Infrastructure.Repositories
{
    public class LookupRepository : ILookupRepository
    {
        private readonly DMSContext _context;
        public LookupRepository(DMSContext context) => _context = context;

        public async Task<IEnumerable<PhongBan>> LayTatCaPhongBan() => await _context.PhongBans.ToListAsync();
        public async Task<IEnumerable<VaiTro>> LayTatCaVaiTro() => await _context.VaiTros.ToListAsync();
        public async Task<IEnumerable<LoaiTaiLieu>> LayTatCaLoaiTaiLieu() => await _context.LoaiTaiLieus.ToListAsync();
        public async Task<IEnumerable<ChuyenMuc>> LayTatCaChuyenMuc() => await _context.ChuyenMucs.ToListAsync();
        public async Task<IEnumerable<DanhMuc>> LayTatCaDanhMuc() => await _context.DanhMucs.ToListAsync();
        public async Task<IEnumerable<QuyenHan>> LayTatCaQuyenHan() => await _context.QuyenHans.ToListAsync();
    }
}

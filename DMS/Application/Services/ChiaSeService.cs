using DMS.Domain.Entities;
using DMS.Domain.Interfaces;
using DMS.API.DTOs;
using DMS.Application.Mappings;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMS.Application.Services
{
    public class ChiaSeService
    {
        private readonly IChiaSeRepository _repo;
        public ChiaSeService(IChiaSeRepository repo) => _repo = repo;

        public async Task<IEnumerable<ChiaSeDto>> LayDanhSach(int taiLieuId)
        {
            var data = await _repo.LayDanhSachChiaSe(taiLieuId);
            return data.Select(s => s.ToDto());
        }
        
        public async Task ChiaSeChoNguoiDung(int taiLieuId, int userId, string quyen)
        {
            var chiaSe = new ChiaSeTaiLieu { TaiLieuId = taiLieuId, NguoiDuocChiaSeId = userId, QuyenHan = quyen };
            await _repo.ChiaSe(chiaSe);
        }

        public async Task ChiaSeChoPhongBan(int taiLieuId, int phongBanId, string quyen)
        {
            var chiaSe = new ChiaSeTaiLieu { TaiLieuId = taiLieuId, PhongBanDuocChiaSeId = phongBanId, QuyenHan = quyen };
            await _repo.ChiaSe(chiaSe);
        }

        public async Task ThuHoi(int id) => await _repo.ThuHoiChiaSe(id);

        public async Task<bool> CoQuyen(int taiLieuId, int userId, string quyen) 
            => await _repo.KiemTraQuyenTruyCap(taiLieuId, userId, quyen);
    }
}

using DMS.Domain.Entities;
using DMS.Domain.Interfaces;
using DMS.API.DTOs;
using DMS.Application.Mappings;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMS.Application.Services
{
    public class ThongBaoService
    {
        private readonly IThongBaoRepository _repo;
        public ThongBaoService(IThongBaoRepository repo) => _repo = repo;

        public async Task<IEnumerable<ThongBaoDto>> DanhSachThongBao()
        {
            var data = await _repo.LayTatCaVoiChiTiet();
            return data.Select(t => t.ToDto());
        }

        public async Task<ThongBaoDto?> ChiTietThongBao(int id)
        {
            var data = await _repo.LayTheoIdVoiChiTiet(id);
            return data?.ToDto();
        }

        public async Task DangThongBao(ThongBao t) => await _repo.Them(t);
        public async Task XoaThongBao(int id) => await _repo.Xoa(id);
        public async Task<IEnumerable<ChuyenMuc>> DanhSachChuyenMuc() => await _repo.LayTatCaChuyenMuc();
    }
}

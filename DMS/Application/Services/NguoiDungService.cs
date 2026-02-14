using DMS.Domain.Entities;
using DMS.Domain.Interfaces;
using DMS.API.DTOs;
using DMS.Application.Mappings;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMS.Application.Services
{
    public class NguoiDungService
    {
        private readonly INguoiDungRepository _repo;
        public NguoiDungService(INguoiDungRepository repo) => _repo = repo;

        public async Task<IEnumerable<NguoiDungDto>> DanhSachNguoiDung()
        {
            var data = await _repo.LayTatCaVoiChiTiet();
            return data.Select(u => u.ToDto());
        }

        public async Task<NguoiDungDto?> ChiTietNguoiDung(int id)
        {
            var data = await _repo.LayTheoIdVoiChiTiet(id);
            return data?.ToDto();
        }

        public async Task TaoNguoiDung(NguoiDung u) => await _repo.ThemNguoiDung(u);
        public async Task CapNhat(NguoiDung u) => await _repo.CapNhat(u);
        public async Task Xoa(int id) => await _repo.Xoa(id);
    }
}

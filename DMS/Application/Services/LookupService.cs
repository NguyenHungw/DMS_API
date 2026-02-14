using DMS.Domain.Entities;
using DMS.Domain.Interfaces;
using DMS.API.DTOs;
using DMS.Application.Mappings;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMS.Application.Services
{
    public class LookupService
    {
        private readonly ILookupRepository _repo;
        public LookupService(ILookupRepository repo) => _repo = repo;

        public async Task<IEnumerable<PhongBan>> LayTatCaPhongBan() => await _repo.LayTatCaPhongBan();
        public async Task<IEnumerable<VaiTro>> LayTatCaVaiTro() => await _repo.LayTatCaVaiTro();
        public async Task<IEnumerable<LoaiTaiLieu>> LayTatCaLoaiTaiLieu() => await _repo.LayTatCaLoaiTaiLieu();
        public async Task<IEnumerable<ChuyenMuc>> LayTatCaChuyenMuc() => await _repo.LayTatCaChuyenMuc();
        public async Task<IEnumerable<DanhMucDto>> LayTatCaDanhMuc()
        {
            var data = await _repo.LayTatCaDanhMuc();
            return data.Select(d => d.ToDto());
        }
        public async Task<IEnumerable<QuyenHan>> LayTatCaQuyenHan() => await _repo.LayTatCaQuyenHan();
    }
}

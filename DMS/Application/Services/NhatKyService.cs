using DMS.Domain.Entities;
using DMS.Domain.Interfaces;

namespace DMS.Application.Services
{
    public class NhatKyService
    {
        private readonly INhatKyRepository _repo;
        public NhatKyService(INhatKyRepository repo) => _repo = repo;

        public async Task GhiLog(int userId, string hanhDong, string doiTuong, string? chiTiet = null, string? ip = null)
        {
            var log = new NhatKyHoatDong
            {
                NguoiDungId = userId,
                HanhDong = hanhDong,
                DoiTuong = doiTuong,
                ChiTiet = chiTiet,
                IPAddress = ip,
                ThoiGian = DateTime.Now
            };
            await _repo.LuuLog(log);
        }

        public async Task<IEnumerable<NhatKyHoatDong>> LayTatCa() => await _repo.LayNhatKy();
    }
}

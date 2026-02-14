using DMS.Domain.Entities;
using DMS.Domain.Interfaces;
using DMS.API.DTOs;
using DMS.Application.Mappings;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMS.Application.Services
{
    public class ChatService
    {
        private readonly IChatRepository _repo;
        public ChatService(IChatRepository repo) => _repo = repo;

        public async Task<IEnumerable<CuocTroChuyenDto>> DanhSachCuocTroChuyen(int userId)
        {
            var data = await _repo.LayCuocTroChuyenCuaUser(userId);
            return data.Select(c => c.ToDto());
        }

        public async Task<CuocTroChuyenDto?> ChiTietCuocTroChuyen(int id)
        {
            var data = await _repo.LayChiTietCuocTroChuyen(id);
            return data?.ToDto();
        }

        public async Task GuiTinNhan(TinNhan m) => await _repo.ThemTinNhan(m);
    }
}

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

        public async Task<NguoiDung?> ChiTietNguoiDungEntity(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task TaoNguoiDung(NguoiDung u) 
        {
            await _repo.ThemNguoiDung(u);
            await _repo.SaveChangesAsync();
        }
        public async Task CapNhat(NguoiDung u) 
        {
            await _repo.CapNhat(u);
            await _repo.SaveChangesAsync();
        }
        public async Task Xoa(int id) 
        {
            await _repo.Xoa(id);
            await _repo.SaveChangesAsync();
        }

        public async Task<bool> CapNhatVaiTro(int userId, int roleId)
        {
            var user = await _repo.GetByIdAsync(userId);
            if (user == null) return false;

            user.VaiTroId = roleId;
            await _repo.CapNhat(user);
            return true;
        }

        public async Task CapNhatAvatar(int userId, string avatarUrl)
        {
            var user = await _repo.GetByIdAsync(userId);
            if (user != null)
            {
                user.HinhAnh = avatarUrl;
                await _repo.CapNhat(user);
                await _repo.SaveChangesAsync();
            }
        }

        public async Task CapNhatThongTin(NguoiDungDto u)
        {
            var user = await _repo.GetByIdAsync(u.Id);
            if (user != null)
            {
                user.HoTen = u.HoTen;
                user.Email = u.Email;
                // Có thể cập nhật thêm các trường khác nếu cần
                await _repo.CapNhat(user);
                await _repo.SaveChangesAsync();
            }
        }
    }
}

using DMS.Domain.Entities;
using DMS.Domain.Interfaces;
using DMS.API.DTOs;
using DMS.Application.Mappings;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMS.Application.Services
{
    public class TaiLieuService
    {
        private readonly ITaiLieuRepository _repo;
        private readonly IWebHostEnvironment env;

        public TaiLieuService(ITaiLieuRepository repo) => _repo = repo;

        public async Task<IEnumerable<TaiLieuDto>> DanhSachTaiLieu()
        {
            var data = await _repo.LayTatCaVoiChiTiet();
            return data.Select(t => t.ToDto());
        }

        public async Task<TaiLieuDto?> ChiTietTaiLieu(int id)
        {
            var data = await _repo.LayTheoIdVoiChiTiet(id);
            return data?.ToDto();
        }

        public async Task<TaiLieu?> LayTheoId(int id) => await _repo.GetByIdAsync(id);

        public async Task LuuTaiLieuFile(IFormFile file, TaiLieu t)
        {
            if (file != null)
            {
                var folderPath = Path.Combine(env.WebRootPath, "TaiLieu");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                string uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
                string extension = Path.GetExtension(file.FileName);
                string filePath = Path.Combine(folderPath, uniqueFileName);


                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                t.DungLuong = (file.Length / 1024).ToString() + " KB"; // Luu dung luong file

                t.TenTaiLieu = fileName;

            }

            await _repo.Them(t);
        }
        public async Task LuuTaiLieu( TaiLieu t) => await _repo.Them(t);
        
        public async Task XoaTaiLieu(int id) => await _repo.Xoa(id);
        
        public async Task TaoPhienBanMoi(PhienBanTaiLieu pb) => await _repo.ThemPhienBan(pb);
    }
}

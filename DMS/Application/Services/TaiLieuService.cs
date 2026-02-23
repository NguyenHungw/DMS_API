using DMS.Domain.Entities;
using DMS.Domain.Interfaces;
using DMS.API.DTOs;
using DMS.Application.Mappings;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace DMS.Application.Services
{
    public class TaiLieuService
    {
        private readonly ITaiLieuRepository _repo;
        private readonly IWebHostEnvironment env;

        public TaiLieuService(ITaiLieuRepository repo, IWebHostEnvironment environment)
        {
            _repo = repo;
            env = environment;
        }

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

        public async Task<TaiLieu> LuuTaiLieuFile(IFormFile file, TaiLieu t)
        {
            if (file != null)
            {
                var folderPath = Path.Combine(env.WebRootPath, "uploads", "TaiLieu");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                
                string uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
                string filePath = Path.Combine(folderPath, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                t.DungLuong = (file.Length / 1024).ToString() + " KB";
                
                // Create the initial version
                var phienBan = new PhienBanTaiLieu
                {
                    SoPhienBan = "1.0",
                    DuongDanFile = uniqueFileName,
                    NgayTao = DateTime.Now,
                    NguoiTaoId = t.ChuSoHuuId,
                    GhiChuThayDoi = "Tải lên tài liệu ban đầu"
                };
                t.DanhSachPhienBan.Add(phienBan);
            }

            await _repo.Them(t);
            await _repo.SaveChangesAsync();
            return t;
        }
        public async Task LuuTaiLieu( TaiLieu t) 
        {
            await _repo.CapNhat(t);
        }

        public async Task<TaiLieuDto?> CapNhatMetadata(int id, DocumentUploadDto dto)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return null;

            entity.TenTaiLieu = dto.TenTaiLieu;
            entity.MoTa = dto.MoTa;
            entity.DanhMucId = dto.DanhMucId;
            entity.NgayCapNhat = DateTime.Now;

            await _repo.CapNhat(entity);
            await _repo.SaveChangesAsync();

            var updated = await _repo.LayTheoIdVoiChiTiet(id);
            return updated?.ToDto();
        }
        
        public async Task XoaTaiLieu(int id) 
        {
            await _repo.Xoa(id);
            await _repo.SaveChangesAsync();
        }
        
        public async Task<IEnumerable<TaiLieuDto>> LayDanhSachChoDuyet()
        {
            var data = await _repo.LayTatCaVoiChiTiet();

            return data
                .Where(t => !string.IsNullOrEmpty(t.TrangThai) && 
                            t.TrangThai.Trim().Equals("PendingApproval", StringComparison.OrdinalIgnoreCase))
                .Select(t => t.ToDto());
        }

        public async Task TaoPhienBanMoi(PhienBanTaiLieu pb) 
        {
            await _repo.ThemPhienBan(pb);
            await _repo.SaveChangesAsync();
        }
    }
}

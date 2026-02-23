using DMS.Domain.Entities;
using DMS.Domain.Interfaces;
using DMS.API.DTOs;
using DMS.Application.Mappings;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMS.Application.Services
{
    public class DanhMucService
    {
        private readonly IDanhMucRepository _repo;

        public DanhMucService(IDanhMucRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<DanhMucDto>> LayTatCa()
        {
            var data = await _repo.GetAllAsync();
            return data.Select(d => d.ToDto());
        }

        public async Task<DanhMucDto> TaoMoi(DanhMucDto dto)
        {
            var entity = new DanhMuc
            {
                TenDanhMuc = dto.TenDanhMuc,
                MoTa = dto.MoTa,
                DanhMucChaId = dto.DanhMucChaId
            };

            await _repo.AddAsync(entity);
            await _repo.SaveChangesAsync();

            return entity.ToDto();
        }

        public async Task<bool> Xoa(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return false;

            _repo.Delete(entity);
            return await _repo.SaveChangesAsync();
        }
    }
}

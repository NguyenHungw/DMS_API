using Microsoft.AspNetCore.Mvc;
using DMS.Application.Services;
using DMS.Domain.Entities;
using DMS.API.DTOs;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace DMS.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrator,Admin")]
    public class NguoiDungController : ControllerBase
    {
        private readonly NguoiDungService _service;
        public NguoiDungController(NguoiDungService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> LayDanhSach()
        {
            var result = await _service.DanhSachNguoiDung();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> LayChiTiet(int id)
        {
            var u = await _service.ChiTietNguoiDung(id);
            if (u == null) return NotFound();
            return Ok(u);
        }

        [HttpPost]
        public async Task<IActionResult> Tao(NguoiDung u)
        {
            if (!string.IsNullOrEmpty(u.MatKhau))
            {
                u.MatKhau = BCrypt.Net.BCrypt.HashPassword(u.MatKhau);
            }
            await _service.TaoNguoiDung(u);
            return CreatedAtAction(nameof(LayChiTiet), new { id = u.Id }, u);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> CapNhat(int id, [FromBody] NguoiDung u)
        {
            var existingUser = await _service.ChiTietNguoiDungEntity(id); // I need to add this method or use a DTO
            if (existingUser == null) return NotFound();

            existingUser.HoTen = u.HoTen;
            existingUser.Email = u.Email;
            existingUser.PhongBanId = u.PhongBanId;
            existingUser.VaiTroId = u.VaiTroId;
            existingUser.TrangThai = u.TrangThai;
            
            if (!string.IsNullOrEmpty(u.MatKhau))
            {
                existingUser.MatKhau = BCrypt.Net.BCrypt.HashPassword(u.MatKhau);
            }

            await _service.CapNhat(existingUser);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Xoa(int id)
        {
            await _service.Xoa(id);
            return NoContent();
        }

        [HttpPatch("{id}/role")]
        public async Task<IActionResult> AssignRole(int id, [FromBody] int roleId)
        {
            var success = await _service.CapNhatVaiTro(id, roleId);
            if (!success) return BadRequest("Không thể cập nhật vai trò.");
            return Ok("Cập nhật vai trò thành công.");
        }
    }
}

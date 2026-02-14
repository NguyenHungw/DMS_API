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
    [Authorize]
    public class ThongBaoController : ControllerBase
    {
        private readonly ThongBaoService _service;
        public ThongBaoController(ThongBaoService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> DanhSach()
        {
            var list = await _service.DanhSachThongBao();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> LayChiTiet(int id)
        {
            var t = await _service.ChiTietThongBao(id);
            if (t == null) return NotFound();
            return Ok(t);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,Manager")]
        public async Task<IActionResult> Tao(ThongBao t)
        {
            await _service.DangThongBao(t);
            return CreatedAtAction(nameof(LayChiTiet), new { id = t.Id }, t);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator,Manager")]
        public async Task<IActionResult> Xoa(int id)
        {
            await _service.XoaThongBao(id);
            return NoContent();
        }
    }
}

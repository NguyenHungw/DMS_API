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
    [Authorize(Roles = "Administrator")]
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
            await _service.TaoNguoiDung(u);
            return CreatedAtAction(nameof(LayChiTiet), new { id = u.Id }, u);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> CapNhat(int id, NguoiDung u)
        {
            if (id != u.Id) return BadRequest();
            await _service.CapNhat(u);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Xoa(int id)
        {
            await _service.Xoa(id);
            return NoContent();
        }
    }
}

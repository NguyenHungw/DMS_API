using Microsoft.AspNetCore.Mvc;
using DMS.Application.Services;
using DMS.API.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace DMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DanhMucController : ControllerBase
    {
        private readonly DanhMucService _service;

        public DanhMucController(DanhMucService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.LayTatCa());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DanhMucDto dto)
        {
            var result = await _service.TaoMoi(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.Xoa(id);
            if (!result) return NotFound();
            return Ok("Đã xóa danh mục thành công.");
        }
    }
}

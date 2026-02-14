using Microsoft.AspNetCore.Mvc;
using DMS.Application.Services;
using Microsoft.AspNetCore.Authorization;

namespace DMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class LookupController : ControllerBase
    {
        private readonly LookupService _service;
        public LookupController(LookupService service) => _service = service;

        [HttpGet("phong-ban")]
        public async Task<IActionResult> GetPhongBan() => Ok(await _service.LayTatCaPhongBan());

        [HttpGet("vai-tro")]
        public async Task<IActionResult> GetVaiTro() => Ok(await _service.LayTatCaVaiTro());

        [HttpGet("loai-tai-lieu")]
        public async Task<IActionResult> GetLoaiTaiLieu() => Ok(await _service.LayTatCaLoaiTaiLieu());

        [HttpGet("chuyen-muc")]
        public async Task<IActionResult> GetChuyenMuc() => Ok(await _service.LayTatCaChuyenMuc());

        [HttpGet("danh-muc")]
        public async Task<IActionResult> GetDanhMuc() => Ok(await _service.LayTatCaDanhMuc());

        [HttpGet("quyen-han")]
        public async Task<IActionResult> GetQuyenHan() => Ok(await _service.LayTatCaQuyenHan());
    }
}

using Microsoft.AspNetCore.Mvc;
using DMS.Application.Services;
using DMS.API.DTOs;
using System.Threading.Tasks;

namespace DMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly XacThucService _service;
        public AuthController(XacThucService service) => _service = service;

        [HttpPost("dang-ky")]
        public async Task<IActionResult> DangKy(DangKyDTO dto)
        {
            var errorMessage = await _service.DangKy(dto);
            if (errorMessage != null) return BadRequest(errorMessage);
            return Ok("Đăng ký thành công.");
        }

        [HttpPost("dang-nhap")]
        public async Task<IActionResult> DangNhap(DangNhapDTO dto)
        {
            var result = await _service.DangNhap(dto);
            if (result == null) return Unauthorized("Email hoặc mật khẩu không đúng.");
            return Ok(result);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using DMS.Application.Services;
using DMS.Domain.Entities;
using DMS.API.DTOs;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace DMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly NguoiDungService _userService;
        private readonly IWebHostEnvironment _env;

        public ProfileController(NguoiDungService userService, IWebHostEnvironment env)
        {
            _userService = userService;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> GetProfile()
        {
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdStr) || !int.TryParse(userIdStr, out int userId))
                return Unauthorized();

            var user = await _userService.ChiTietNguoiDung(userId);
            if (user == null) return NotFound();

            return Ok(user);
        }

        [HttpPost("avatar")]
        public async Task<IActionResult> UploadAvatar(IFormFile file)
        {
            if (file == null || file.Length == 0) return BadRequest("Không có file.");

            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdStr) || !int.TryParse(userIdStr, out int userId))
                return Unauthorized();

            var uploads = Path.Combine(_env.WebRootPath, "uploads", "avatars");
            if (!Directory.Exists(uploads)) Directory.CreateDirectory(uploads);

            var fileName = $"avatar_{userId}_{DateTime.Now.Ticks}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploads, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var avatarUrl = $"/uploads/avatars/{fileName}";
            
            // Cập nhật database thông qua service
            // Ở đây tôi cần thêm method CapNhatAvatar vào service
            await _userService.CapNhatAvatar(userId, avatarUrl);

            return Ok(new { url = avatarUrl });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProfile([FromBody] NguoiDungDto updatedUser)
        {
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdStr) || !int.TryParse(userIdStr, out int userId))
                return Unauthorized();

            if (userId != updatedUser.Id) return BadRequest();

            // Cần thêm method CapNhatThongTin vào service
            await _userService.CapNhatThongTin(updatedUser);
            
            return NoContent();
        }
    }
}

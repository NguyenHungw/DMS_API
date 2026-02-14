using Microsoft.AspNetCore.Mvc;
using DMS.Application.Services;
using DMS.Domain.Entities;
using Microsoft.AspNetCore.Authorization;

namespace DMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ChiaSeController : ControllerBase
    {
        private readonly ChiaSeService _service;
        private readonly PermissionService _permissionService;
        private readonly NhatKyService _nhatKyService;

        public ChiaSeController(ChiaSeService service, PermissionService permissionService, NhatKyService nhatKyService)
        {
            _service = service;
            _permissionService = permissionService;
            _nhatKyService = nhatKyService;
        }

        private int GetUserId() => int.Parse(User.FindFirst("sub")?.Value ?? "0");
        private string GetIP() => HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";

        [HttpGet("{taiLieuId}")]
        public async Task<IActionResult> DanhSach(int taiLieuId) => Ok(await _service.LayDanhSach(taiLieuId));

        [HttpPost("nguoi-dung")]
        public async Task<IActionResult> ChiaSeUser(int taiLieuId, int userId, string quyen = "View")
        {
            if (!await _permissionService.CheckPermission(GetUserId(), "Share"))
                return Forbid("Bạn không có quyền chia sẻ tài liệu.");

            await _service.ChiaSeChoNguoiDung(taiLieuId, userId, quyen);
            await _nhatKyService.GhiLog(GetUserId(), "SHARE", "TaiLieu", $"Chia sẻ tài liệu ID: {taiLieuId} cho người dùng ID: {userId}", GetIP());
            return Ok("Đã chia sẻ cho người dùng.");
        }

        [HttpPost("phong-ban")]
        public async Task<IActionResult> ChiaSeDept(int taiLieuId, int phongBanId, string quyen = "View")
        {
            if (!await _permissionService.CheckPermission(GetUserId(), "Share"))
                return Forbid("Bạn không có quyền chia sẻ tài liệu.");

            await _service.ChiaSeChoPhongBan(taiLieuId, phongBanId, quyen);
            await _nhatKyService.GhiLog(GetUserId(), "SHARE", "TaiLieu", $"Chia sẻ tài liệu ID: {taiLieuId} cho phòng ban ID: {phongBanId}", GetIP());
            return Ok("Đã chia sẻ cho phòng ban.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ThuHoi(int id)
        {
            if (!await _permissionService.CheckPermission(GetUserId(), "Share"))
                return Forbid("Bạn không có quyền thu hồi chia sẻ.");

            await _service.ThuHoi(id);
            await _nhatKyService.GhiLog(GetUserId(), "REVOKE", "TaiLieu", $"Thu hồi quyền chia sẻ ID: {id}", GetIP());
            return Ok("Đã thu hồi quyền chia sẻ.");
        }
    }
}

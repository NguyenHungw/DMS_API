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
    public class TaiLieuController : ControllerBase
    {
        private readonly TaiLieuService _service;
        private readonly PermissionService _permissionService;
        private readonly NhatKyService _nhatKyService;
        public TaiLieuController(TaiLieuService service, PermissionService permissionService, NhatKyService nhatKyService)
        {
            _service = service;
            _permissionService = permissionService;
            _nhatKyService = nhatKyService;
        }

        private int GetUserId() => int.Parse(User.FindFirst("sub")?.Value ?? "0");
        private string GetIP() => HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";

        [HttpGet]
        public async Task<IActionResult> DanhSach()
        {
            var docs = await _service.DanhSachTaiLieu();
            return Ok(docs);
        }

        [HttpGet("tim-kiem")]
        public async Task<IActionResult> TimKiem(string? tuKhoa, int? danhMucId, int? ownerId, DateTime? tuNgay, DateTime? denNgay)
        {
            var docs = await _service.DanhSachTaiLieu();
            var filtered = docs.AsEnumerable();

            if (!string.IsNullOrEmpty(tuKhoa))
                filtered = filtered.Where(t => t.TenTaiLieu.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase) || (t.MoTa != null && t.MoTa.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase)));
            
            if (danhMucId.HasValue)
                filtered = filtered.Where(t => t.DanhMucId == danhMucId);

            if (ownerId.HasValue)
                filtered = filtered.Where(t => t.ChuSoHuuId == ownerId);

            if (tuNgay.HasValue)
                filtered = filtered.Where(t => t.NgayTao >= tuNgay);

            if (denNgay.HasValue)
                filtered = filtered.Where(t => t.NgayTao <= denNgay);

            await _nhatKyService.GhiLog(GetUserId(), "SEARCH", "TaiLieu", $"Tìm kiếm tài liệu với từ khóa: {tuKhoa}", GetIP());

            return Ok(filtered);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> LayChiTiet(int id)
        {
            var t = await _service.ChiTietTaiLieu(id);
            if (t == null) return NotFound();

            await _nhatKyService.GhiLog(GetUserId(), "VIEW", "TaiLieu", $"Xem tài liệu: {t.TenTaiLieu}", GetIP());
            
            return Ok(t);
        }
        [HttpPost]
        public async Task<IActionResult> Tao(TaiLieu t)
        {
            if (!await _permissionService.CheckPermission(GetUserId(), "Upload"))
                return Forbid("Bạn không có quyền tải lên tài liệu.");

            //await _service.LuuTaiLieu(file, t);
            await _service.LuuTaiLieu(t);
            await _nhatKyService.GhiLog(GetUserId(), "CREATE", "TaiLieu", $"Tạo mới tài liệu: {t.TenTaiLieu}", GetIP());
            return CreatedAtAction(nameof(LayChiTiet), new { id = t.Id }, t);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Xoa(int id)
        {
            if (!await _permissionService.CheckPermission(GetUserId(), "Delete"))
                return Forbid("Bạn không có quyền xóa tài liệu.");

            await _service.XoaTaiLieu(id);
            await _nhatKyService.GhiLog(GetUserId(), "DELETE", "TaiLieu", $"Xóa tài liệu ID: {id}", GetIP());
            return NoContent();
        }

        [HttpPost("phien-ban")]
        public async Task<IActionResult> TaoPhienBan(PhienBanTaiLieu pb)
        {
            if (!await _permissionService.CheckPermission(GetUserId(), "Edit"))
                return Forbid("Bạn không có quyền chỉnh sửa tài liệu.");

            await _service.TaoPhienBanMoi(pb);
            await _nhatKyService.GhiLog(GetUserId(), "EDIT", "TaiLieu", $"Tạo phiên bản mới cho tài liệu ID: {pb.TaiLieuId}", GetIP());
            return Ok(pb);
        }

        [HttpPatch("{id}/gui-duyet")]
        public async Task<IActionResult> GuiDuyet(int id)
        {
            var t = await _service.LayTheoId(id);
            if (t == null) return NotFound();
            if (t.ChuSoHuuId != GetUserId()) return Forbid("Chỉ chủ sở hữu mới có thể gửi duyệt.");

            t.TrangThai = "PendingApproval";
            await _service.LuuTaiLieu(t);
            await _nhatKyService.GhiLog(GetUserId(), "SUBMIT", "TaiLieu", $"Gửi duyệt tài liệu: {t.TenTaiLieu}", GetIP());
            return Ok("Tài liệu đã được gửi duyệt.");
        }

        [HttpPatch("{id}/phe-duyet")]
        public async Task<IActionResult> PheDuyet(int id)
        {
            if (!await _permissionService.CheckPermission(GetUserId(), "Approve"))
                return Forbid("Bạn không có quyền phê duyệt tài liệu.");

            var t = await _service.LayTheoId(id);
            if (t == null) return NotFound();

            t.TrangThai = "Approved";
            await _service.LuuTaiLieu(t); 
            await _nhatKyService.GhiLog(GetUserId(), "APPROVE", "TaiLieu", $"Phê duyệt tài liệu: {t.TenTaiLieu}", GetIP());
            return Ok("Tài liệu đã được phê duyệt.");
        }

        [HttpPatch("{id}/ban-hanh")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> BanHanh(int id)
        {
            var t = await _service.LayTheoId(id);
            if (t == null) return NotFound();

            t.TrangThai = "Circulating";
            await _service.LuuTaiLieu(t);
            await _nhatKyService.GhiLog(GetUserId(), "CIRCULATE", "TaiLieu", $"Ban hành tài liệu: {t.TenTaiLieu}", GetIP());
            return Ok("Tài liệu đã được ban hành.");
        }
    }
}

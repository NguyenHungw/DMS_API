using Microsoft.AspNetCore.Mvc;
using DMS.Application.Services;
using DMS.Domain.Entities;
using DMS.API.DTOs;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using Microsoft.AspNetCore.Hosting;

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
        private readonly IWebHostEnvironment env;

        public TaiLieuController(TaiLieuService service, PermissionService permissionService, NhatKyService nhatKyService, IWebHostEnvironment environment)
        {
            _service = service;
            _permissionService = permissionService;
            _nhatKyService = nhatKyService;
            env = environment;
        }

        private int GetUserId() => int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");
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
        public async Task<IActionResult> Tao([FromForm] DocumentUploadDto dto)
        {
            if (!await _permissionService.CheckPermission(GetUserId(), "Upload"))
                return StatusCode(403, "Bạn không có quyền tải lên tài liệu.");

            var t = new TaiLieu
            {
                TenTaiLieu = dto.TenTaiLieu,
                MoTa = dto.MoTa,
                DanhMucId = dto.DanhMucId,
                LoaiTaiLieuId = dto.LoaiTaiLieuId ?? 1, // Default to PDF if not specified
                ChuSoHuuId = GetUserId(),
                PhongBanId = 1, // Default or fetch from user profile
                TrangThai = "Draft"
            };

            if (dto.File != null)
            {
                await _service.LuuTaiLieuFile(dto.File, t);
            }
            else
            {
                await _service.LuuTaiLieu(t);
            }

            await _nhatKyService.GhiLog(GetUserId(), "CREATE", "TaiLieu", $"Tải lên tài liệu: {t.TenTaiLieu}", GetIP());
            return CreatedAtAction(nameof(LayChiTiet), new { id = t.Id }, t);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMetadata(int id, [FromBody] DocumentUploadDto dto)
        {
            if (!await _permissionService.CheckPermission(GetUserId(), "Edit"))
                return StatusCode(403, "Bạn không có quyền chỉnh sửa tài liệu.");

            var result = await _service.CapNhatMetadata(id, dto);
            if (result == null) return NotFound();

            await _nhatKyService.GhiLog(GetUserId(), "EDIT", "TaiLieu", $"Cập nhật metadata tài liệu: {dto.TenTaiLieu}", GetIP());
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Xoa(int id)
        {
            if (!await _permissionService.CheckPermission(GetUserId(), "Delete"))
                return StatusCode(403, "Bạn không có quyền xóa tài liệu.");

            await _service.XoaTaiLieu(id);
            await _nhatKyService.GhiLog(GetUserId(), "DELETE", "TaiLieu", $"Xóa tài liệu ID: {id}", GetIP());
            return NoContent();
        }

        [HttpPost("phien-ban")]
        public async Task<IActionResult> TaoPhienBan(PhienBanTaiLieu pb)
        {
            if (!await _permissionService.CheckPermission(GetUserId(), "Edit"))
                return StatusCode(403, "Bạn không có quyền chỉnh sửa tài liệu.");

            await _service.TaoPhienBanMoi(pb);
            await _nhatKyService.GhiLog(GetUserId(), "EDIT", "TaiLieu", $"Tạo phiên bản mới cho tài liệu ID: {pb.TaiLieuId}", GetIP());
            return Ok(pb);
        }

        [HttpPatch("{id}/gui-duyet")]
        public async Task<IActionResult> GuiDuyet(int id)
        {
            var t = await _service.LayTheoId(id);
            if (t == null) return NotFound();
            if (t.ChuSoHuuId != GetUserId()) return StatusCode(403, "Chỉ chủ sở hữu mới có thể gửi duyệt.");

            t.TrangThai = "PendingApproval";
            await _service.LuuTaiLieu(t);
            await _nhatKyService.GhiLog(GetUserId(), "SUBMIT", "TaiLieu", $"Gửi duyệt tài liệu: {t.TenTaiLieu}", GetIP());
            return Ok("Tài liệu đã được gửi duyệt.");
        }

        [HttpPatch("{id}/phe-duyet")]
        public async Task<IActionResult> PheDuyet(int id)
        {
            if (!await _permissionService.CheckPermission(GetUserId(), "Approve"))
                return StatusCode(403, "Bạn không có quyền phê duyệt tài liệu.");

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
        [HttpGet("pending")]
        public async Task<IActionResult> DanhSachChoDuyet()
        {
            if (!User.IsInRole("Administrator") && !await _permissionService.CheckPermission(GetUserId(), "Approve"))
                return StatusCode(403, "Bạn không có quyền xem danh sách chờ phê duyệt.");

            var list = await _service.LayDanhSachChoDuyet();
            return Ok(list);
        }

        [HttpGet("{id}/download")]
        public async Task<IActionResult> Download(int id)
        {
            var t = await _service.ChiTietTaiLieu(id); // Use ChiTietTaiLieu to get the DTO with mapping or handle raw entity
            if (t == null) return NotFound();

            if (string.IsNullOrEmpty(t.DuongDan))
                return BadRequest("Tài liệu không có tệp đính kèm.");

            var filePath = Path.Combine(env.WebRootPath, "uploads", "TaiLieu", t.DuongDan);
            if (!System.IO.File.Exists(filePath))
                return NotFound("Không tìm thấy tệp tin trên máy chủ.");

            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            // Xác định Content-Type dựa trên đuôi file nếu cần, hoặc dùng mặc định
            return File(memory, "application/octet-stream", t.TenTaiLieu + Path.GetExtension(t.DuongDan));
        }
    }
}

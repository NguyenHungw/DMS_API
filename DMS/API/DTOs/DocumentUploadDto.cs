using Microsoft.AspNetCore.Http;

namespace DMS.API.DTOs
{
    public class DocumentUploadDto
    {
        public string TenTaiLieu { get; set; } = string.Empty;
        public string? MoTa { get; set; }
        public int? DanhMucId { get; set; }
        public int? LoaiTaiLieuId { get; set; }
        public IFormFile? File { get; set; }
    }
}

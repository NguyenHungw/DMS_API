namespace DMS.API.DTOs
{
    public class DanhMucDto
    {
        public int Id { get; set; }
        public string TenDanhMuc { get; set; } = string.Empty;
        public string? MoTa { get; set; }
        public int? DanhMucChaId { get; set; }
        public string? TenDanhMucCha { get; set; }
    }
}

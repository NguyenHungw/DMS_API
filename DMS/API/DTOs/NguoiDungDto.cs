namespace DMS.API.DTOs
{
    public class NguoiDungDto
    {
        public int Id { get; set; }
        public string HoTen { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? TrangThai { get; set; }
        
        public int? VaiTroId { get; set; }
        public string? TenVaiTro { get; set; }
        
        public int? PhongBanId { get; set; }
        public string? TenPhongBan { get; set; }
    }
}

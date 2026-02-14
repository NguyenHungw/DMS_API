namespace DMS.API.DTOs
{
    public class DangKyDTO
    {
        public string HoTen { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string MatKhau { get; set; } = string.Empty;
        public int VaiTroId { get; set; } = 2; // Mặc định là User
        public int PhongBanId { get; set; } = 1;
    }

    public class DangNhapDTO
    {
        public string Email { get; set; } = string.Empty;
        public string MatKhau { get; set; } = string.Empty;
    }

    public class KetQuaXacThucDTO
    {
        public string Token { get; set; } = string.Empty;
        public string HoTen { get; set; } = string.Empty;
        public string VaiTro { get; set; } = string.Empty;
    }
}

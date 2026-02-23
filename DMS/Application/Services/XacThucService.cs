using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DMS.Domain.Entities;
using DMS.Domain.Interfaces;
using DMS.API.DTOs;
using Microsoft.Extensions.Configuration;

namespace DMS.Application.Services
{
    public class XacThucService
    {
        private readonly IXacThucRepository _repo;
        private readonly IConfiguration _config;

        public XacThucService(IXacThucRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        public async Task<string?> DangKy(DangKyDTO dto)
        {
            var exists = await _repo.LayNguoiDungTheoEmail(dto.Email);
            if (exists != null) return "Email đã tồn tại.";

            if (!await _repo.KiemTraPhongBan(dto.PhongBanId))
                return "Phòng ban không tồn tại.";

            if (!await _repo.KiemTraVaiTro(dto.VaiTroId))
                return "Vai trò không tồn tại.";

            var user = new NguoiDung
            {
                HoTen = dto.HoTen,
                Email = dto.Email,
                MatKhau = BCrypt.Net.BCrypt.HashPassword(dto.MatKhau),
                VaiTroId = dto.VaiTroId,
                PhongBanId = dto.PhongBanId,
                TrangThai = "Active"
            };

            await _repo.LuuNguoiDung(user);
            await _repo.SaveChangesAsync();
            return null; // Thành công
        }

        public async Task<KetQuaXacThucDTO?> DangNhap(DangNhapDTO dto)
        {
            var user = await _repo.LayNguoiDungTheoEmail(dto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.MatKhau, user.MatKhau))
                return null;

            return new KetQuaXacThucDTO
            {
                Token = TaoToken(user),
                HoTen = user.HoTen,
                VaiTro = user.VaiTro?.TenVaiTro ?? "User"
            };
        }

        private string TaoToken(NguoiDung user)
        {
            var jwtSettings = _config.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.HoTen),
                new Claim(ClaimTypes.Role, user.VaiTro?.TenVaiTro ?? "User")
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["ExpiryInMinutes"] ?? "60")),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

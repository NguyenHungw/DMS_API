using System;
using System.Collections.Generic;

namespace DMS.API.DTOs
{
    public class CuocTroChuyenDto
    {
        public int Id { get; set; }
        public string? TenCuocTroChuyen { get; set; }
        public DateTime NgayTao { get; set; }
        public List<string> TenThanhVien { get; set; } = new List<string>();
        public List<TinNhanDto> TinNhanMoiNhat { get; set; } = new List<TinNhanDto>();
    }

    public class TinNhanDto
    {
        public int Id { get; set; }
        public string NoiDung { get; set; } = string.Empty;
        public DateTime ThoiGianGui { get; set; }
        public int NguoiGuiId { get; set; }
        public string? TenNguoiGui { get; set; }
    }
}

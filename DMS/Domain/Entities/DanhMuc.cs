using System;
using System.Collections.Generic;

namespace DMS.Domain.Entities
{
    public class DanhMuc
    {
        public int Id { get; set; }
        public string TenDanhMuc { get; set; } = string.Empty;
        public string? MoTa { get; set; }
        
        public int? DanhMucChaId { get; set; }
        public DanhMuc? DanhMucCha { get; set; }
        
        public ICollection<DanhMuc> DanhMucCon { get; set; } = new List<DanhMuc>();
        public ICollection<TaiLieu> DanhSachTaiLieu { get; set; } = new List<TaiLieu>();
    }
}

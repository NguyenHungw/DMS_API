using System;

namespace DMS.Domain.Entities
{
    public class QuyenHan
    {
        public int Id { get; set; }
        public string TenQuyenHan { get; set; } = string.Empty; // View, Upload, Edit, Delete, Share, Approve
    }
}

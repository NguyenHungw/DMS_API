using System.Collections.Generic;

namespace DMS.API.DTOs
{
    public class PermissionDto
    {
        public int Id { get; set; }
        public string TenQuyenHan { get; set; } = string.Empty;
    }

    public class RolePermissionDto
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public List<PermissionDto> Permissions { get; set; } = new List<PermissionDto>();
    }

    public class AssignPermissionDto
    {
        public int RoleId { get; set; }
        public List<int> PermissionIds { get; set; } = new List<int>();
    }
}

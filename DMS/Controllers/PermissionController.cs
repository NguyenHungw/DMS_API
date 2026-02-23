using Microsoft.AspNetCore.Mvc;
using DMS.Application.Services;
using DMS.API.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermissionController : ControllerBase
    {
        private readonly PermissionService _service;
        public PermissionController(PermissionService service) => _service = service;

        [HttpGet("roles")]
        public async Task<IActionResult> GetRoles()
        {
            var result = await _service.GetRolesWithPermissionsAsync();
            return Ok(result);
        }
      
        [HttpGet("all")]
        public async Task<IActionResult> GetAllPermissions()
        {
            var result = await _service.GetAllPermissionsAsync();
            return Ok(result);
        }

        [HttpPost("assign")]
        public async Task<IActionResult> Assign(AssignPermissionDto dto)
        {
            var success = await _service.AssignPermissionsToRoleAsync(dto);
            if (!success) return BadRequest("Không thể gán quyền (Sai RoleId hoặc có lỗi xảy ra).");
            return Ok("Cập nhật quyền thành công.");
        }

    }
}

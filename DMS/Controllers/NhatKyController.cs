using Microsoft.AspNetCore.Mvc;
using DMS.Application.Services;
using Microsoft.AspNetCore.Authorization;

namespace DMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrator,Manager")]
    public class NhatKyController : ControllerBase
    {
        private readonly NhatKyService _service;
        public NhatKyController(NhatKyService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> TatCa() => Ok(await _service.LayTatCa());
    }
}

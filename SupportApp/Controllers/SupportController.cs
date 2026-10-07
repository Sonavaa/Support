using Microsoft.AspNetCore.Mvc;
using SupportApp.DTOs.SupportDTOs;
using SupportApp.Services.Interfaces;

namespace SupportApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupportController : ControllerBase
    {
        private readonly ISupportService _supportService;

        public SupportController(ISupportService supportService)
        {
                _supportService = supportService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSupport([FromForm] CreateSupportDTO createSupportDTO)
        {
            var result = await _supportService.CreateSupportAsync(createSupportDTO);
            return Ok(result);
        }
    }
}

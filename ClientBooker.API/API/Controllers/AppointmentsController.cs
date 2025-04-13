using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.Services;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly AppointmentService _service;
        public AppointmentsController(AppointmentService service) => _service = service;

        [HttpPost]
        public async Task<ActionResult<AppointmentDto>> Create(AppointmentDto dto)
            => Ok(await _service.CreateAsync(dto));

        [HttpGet]
        public async Task<ActionResult<List<AppointmentDto>>> GetAll()
            => Ok(await _service.GetAllAsync());

        [HttpDelete("{id}")]
        public async Task<IActionResult> Cancel(Guid id)
        {
            await _service.CancelAsync(id);
            return NoContent();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.Services;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly ClientService _service;
        public ClientsController(ClientService service) => _service = service;

        [HttpPost]
        public async Task<ActionResult<ClientDto>> Create(ClientDto dto)
            => Ok(await _service.CreateAsync(dto));

        [HttpGet]
        public async Task<ActionResult<List<ClientDto>>> GetAll()
            => Ok(await _service.GetAllAsync());
    }
}

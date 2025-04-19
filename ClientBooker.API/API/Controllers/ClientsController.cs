using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using ClientBooker.API.Domain.Exceptions;

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
        {
            try
            {
                var createdClient = await _service.CreateAsync(dto);
                return Created($"Client Created {createdClient.Id}", createdClient);
            }
            catch (ClientAlreadyExistsException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, statusCode: 500);
            }

        }

        [HttpGet]
        public async Task<ActionResult<List<ClientDto>>> GetAll()
            => Ok(await _service.GetAllAsync());
    }
}

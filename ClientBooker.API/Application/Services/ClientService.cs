namespace Application.Services
{
    using Application.DTOs;
    using ClientBooker.API.Domain.Exceptions;
    using Domain.Entities;
    using Domain.Interfaces;

    public class ClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository repo) => _clientRepository = repo;

        public async Task<ClientDto> CreateAsync(ClientDto dto)
        {            
            var existingClient = await _clientRepository.GetByEmailAsync(dto.Email).ConfigureAwait(false);

            if (existingClient != null)
            { 
                throw new ClientAlreadyExistsException($"Client with email {dto.Email} already exists.");
            }
            
            Console.WriteLine($"[ClientService] Creating client: {dto.Name} | clientId: {dto.Email}");
            
            var client = new Client(dto.Name, dto.Phone, dto.Email);

            var saved = await _clientRepository.AddAsync(client).ConfigureAwait(false);

            return new ClientDto
            {
                Id = saved.Id,
                Name = saved.Name,
                Phone = saved.Phone,
                Email = saved.Email
            };
        }

        public async Task<List<ClientDto>> GetAllAsync()
        {
            Console.WriteLine("[ClientService] Retrieving all clients");
            var clients = await _clientRepository.GetAllAsync().ConfigureAwait(false);
            return [.. clients.Select(c => new ClientDto
            {
                Id = c.Id,
                Name = c.Name,
                Phone = c.Phone,
                Email = c.Email
            })];
        }
    }
}

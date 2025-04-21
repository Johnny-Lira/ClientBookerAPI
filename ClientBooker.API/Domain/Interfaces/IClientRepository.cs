namespace Domain.Interfaces;

using Domain.Entities;

public interface IClientRepository
{
    Task<Client> AddAsync(Client client);
    Task<List<Client>> GetAllAsync();
    Task<Client?> GetByEmailAsync(string email);
}
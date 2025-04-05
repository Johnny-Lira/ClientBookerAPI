namespace Domain.Interfaces;

using Domain.Entities;

public interface IClientRepository
{
    Task<Client> AddAsync(Client Client);
    Task<List<Client>> GetAllAsync();
}
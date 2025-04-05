namespace Domain.Interfaces;

using Domain.Entities;

public interface IClienteRepository
{
    Task<Cliente> AddAsync(Cliente cliente);
    Task<List<Cliente>> GetAllAsync();
}
namespace Domain.Interfaces;

using Domain.Entities;

public interface IAppointmentRepository
{
    Task<Appointment> AddAsync(Appointment Appointment);
    Task<List<Appointment>> GetAllAsync();
    Task<bool> ExisteAppointmentMesmoHorarioAsync(DateTime horario);
    Task DeleteAsync(Guid id);
}

namespace Domain.Interfaces;

using Domain.Entities;

public interface IAppointmentRepository
{
    Task<Appointment> AddAsync(Appointment appointment);
    Task<List<Appointment>> GetAllAsync();
    Task<bool> ExistsAppointmentAtSameTimeAsync(DateTime time);
    Task DeleteAsync(Guid id);
}

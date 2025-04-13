using Infrastructure.Persistence.Context;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly AppDbContext _context;
        public AppointmentRepository(AppDbContext context) => _context = context;

        public async Task<Appointment> AddAsync(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
            return appointment;
        }

        public async Task<List<Appointment>> GetAllAsync()
        {
            return await _context.Appointments.ToListAsync();
        }

        public async Task<bool> ExistsAppointmentAtSameTimeAsync(DateTime time)
        {
            return await _context.Appointments.AnyAsync(a => a.DateTime == time);
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.Appointments.FindAsync(id);
            if (entity != null)
            {
                _context.Appointments.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }

}

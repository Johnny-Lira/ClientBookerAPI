namespace Application.Services
{
    using Domain.Interfaces;
    using Domain.Entities;
    using Application.DTOs;

    public class AppointmentService
    {
        private readonly IAppointmentRepository _repo;

        public AppointmentService(IAppointmentRepository repo) => _repo = repo;

        public async Task<AppointmentDto> CreateAsync(AppointmentDto dto)
        {
            Console.WriteLine($"[AppointmentService] Creating appointment for ClientId {dto.ClientId} at {dto.DateTime}");

            if (await _repo.ExistsAppointmentAtSameTimeAsync(dto.DateTime))
                throw new InvalidOperationException("An appointment already exists at this time.");

            var appointment = new Appointment(dto.ClientId, dto.DateTime, dto.Description);
            var saved = await _repo.AddAsync(appointment);

            return new AppointmentDto
            {
                Id = saved.Id,
                ClientId = saved.ClientId,
                DateTime = saved.DateTime,
                Description = saved.Description
            };
        }

        public async Task<List<AppointmentDto>> GetAllAsync()
        {
            Console.WriteLine("[AppointmentService] Retrieving all appointments");
            var appointments = await _repo.GetAllAsync();
            return appointments.Select(s => new AppointmentDto
            {
                Id = s.Id,
                ClientId = s.ClientId,
                DateTime = s.DateTime,
                Description = s.Description
            }).ToList();
        }

        public Task CancelAsync(Guid id)
        {
            Console.WriteLine($"[AppointmentService] Cancelling appointment with Id: {id}");
            return _repo.DeleteAsync(id);
        }
    }
}

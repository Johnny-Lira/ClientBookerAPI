namespace Application.DTOs
{
    public class AppointmentDto
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public DateTime DateTime { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}


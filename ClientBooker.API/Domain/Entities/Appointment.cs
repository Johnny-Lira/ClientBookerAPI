namespace Domain.Entities;

public class Appointment
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public Guid ClientId { get; init; }
    public DateTime DateTime { get; protected set; }
    public string Description { get; protected set; }

    protected Appointment() { } // Parameterless constructor for EF Core

    public Appointment(Guid clientId, DateTime datetime, string description)
    {
        if (datetime <= DateTime.Now)
            throw new ArgumentException("Data e hora devem ser futuras.");

        ClientId = clientId;
        DateTime = datetime;
        Description = description;
    }
}
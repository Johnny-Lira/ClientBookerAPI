namespace Domain.Entities;

public class Appointment
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public Guid ClienteId { get; private set; }
    public DateTime DataHora { get; private set; }
    public string Descricao { get; private set; }

    public Appointment(Guid clienteId, DateTime dataHora, string descricao)
    {
        if (dataHora <= DateTime.Now)
            throw new ArgumentException("Data e hora devem ser futuras.");

        ClienteId = clienteId;
        DataHora = dataHora;
        Descricao = descricao;
    }
}
namespace Domain.Entities;

public record Client
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Nome { get; private set; }
    public string Telefone { get; private set; }
    public string Email { get; private set; }

    public Client(string nome, string telefone, string email)
    {
        Nome = nome;
        Telefone = telefone;
        Email = email;
    }
}
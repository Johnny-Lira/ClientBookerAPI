namespace Domain.Entities;

public record Client
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Name { get; protected set; }
    public string Phone { get; protected set; }
    public string Email { get; protected set; }

    protected Client() { } // Parameterless constructor for EF Core

    public Client(string name, string phone, string email)
    {
        Name = name;
        Phone = ValidatePhone(phone);
        Email = ValidateEmail(email);
    }

    private static string ValidatePhone(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
            throw new ArgumentException("Telefone não pode ser vazio.");

        phone = new string([.. phone.Where(char.IsDigit)]);
        
        if (phone.Length < 10 || phone.Length > 15)
            throw new ArgumentException("Telefone deve ter entre 10 e 15 caracteres.");

        return phone;
    }

    public static string ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email não pode ser vazio.");

        email = email.Trim().ToLower();

        if (!email.Contains("@") || !email.Contains("."))
            throw new ArgumentException("Email inválido.");

        if (email.Length < 5 || email.Length > 50)
            throw new ArgumentException("Email deve ter entre 5 e 50 caracteres.");

        return email;
    }
}
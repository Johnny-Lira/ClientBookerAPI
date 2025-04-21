namespace ClientBooker.API.Domain.Exceptions
{
    public class ClientAlreadyExistsException(string? message) : Exception(message)
    {
    }
}
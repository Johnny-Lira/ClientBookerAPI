namespace ClientBooker.API.Domain.Exceptions
{
    [Serializable]
    internal class ClientAlreadyExistsException(string? message) : Exception(message)
    {
    }
}
namespace DesafioTarget.Common
{
    public class NotFoundException(string message) : Exception(message);
    public class DomainException(string message) : Exception(message);
}

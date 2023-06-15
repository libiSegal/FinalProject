
namespace Dal.Exceptions;

public class InvalidDataException : Exception
{
    public InvalidDataException() { }
    public InvalidDataException(string message) : base(message) { }
    public InvalidDataException(Exception inner, string message) : base(message, inner) { }
}

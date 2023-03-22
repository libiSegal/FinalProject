
namespace Dal.Exceptions;

public class NotExistsDataObjectException : Exception
{
    public NotExistsDataObjectException() { }



    public NotExistsDataObjectException(string message) : base(message)
    {

    }
    public NotExistsDataObjectException(Exception inner, string message) : base(message, inner) { }
}

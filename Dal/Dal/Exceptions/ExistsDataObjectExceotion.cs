
namespace Dal.Exceptions;
public class ExistsDataObjectExceotion : Exception
{
    public ExistsDataObjectExceotion(){ }
    public ExistsDataObjectExceotion(string message) : base(message) { }
    public ExistsDataObjectExceotion(Exception inner, string message) :base(message, inner){ }       
}

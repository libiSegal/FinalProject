
namespace BL.DataAPI;
public class BLException : Exception
{
    public int Status { get;  }
    override public string Message { get; }
    public BLException(Exception ex, int status = 500)
    {
        Message = ex.Message;
        Status = status;
    }
    public BLException(string message, int status = 500)
    {
        Message = message;
        Status = status;
    }
}

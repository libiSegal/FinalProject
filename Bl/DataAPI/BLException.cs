using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.DataAPI;

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
    /*private int ErrorCode(Exception ex)
    {
        switch(ex)
        {
            case ExistsDataObjectExceotion:                
                return 400;
            default: 
                return 500;
        }
    }*/
}

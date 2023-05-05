using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.DataAPI;

public class GlobalException : Exception
{
    public int Status { get;  }
    override public string Message { get; }
    public GlobalException(Exception ex)
    {
        Message = ex.Message;
        Status = ErrorCode(ex);
    }
    private int ErrorCode(Exception ex)
    {
        switch(ex)
        {
            case ExistsDataObjectExceotion:                
                return 400;
            default: 
                return 500;
        }
    }
}

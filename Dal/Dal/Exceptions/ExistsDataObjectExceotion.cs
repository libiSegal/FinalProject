using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Exceptions
{
    public class ExistsDataObjectExceotion : Exception
    {
        public ExistsDataObjectExceotion(){ }

       
        
        public ExistsDataObjectExceotion(string message):base(message)
        {

        }
        public ExistsDataObjectExceotion(Exception inner, string message) :base(message, inner){ }
            
    }
}

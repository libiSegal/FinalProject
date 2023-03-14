using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Exceptions
{
    public class NotExistsDataObjectException : Exception
    {
        public NotExistsDataObjectException() { }



        public NotExistsDataObjectException(string message) : base(message)
        {

        }
        public NotExistsDataObjectException(Exception inner, string message) : base(message, inner) { }
    }
}

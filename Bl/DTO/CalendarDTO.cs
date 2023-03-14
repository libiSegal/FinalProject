using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class CalendarDTO
    {
        public List<Dictionary<DateTime, List<WashAbleDTO>>> Machines { get; set; }
        public CalendarDTO()
        {
            Machines = new List<Dictionary<DateTime, List<WashAbleDTO>>>();
        }
    }
}

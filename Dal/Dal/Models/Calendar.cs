
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

public class Calendar 
{
    public List<Dictionary<DateTime , List<WashAble>>> Machines { get; set; }
    public Calendar()
    {
        Machines = new List<Dictionary<DateTime, List<WashAble>>>();
    }
}



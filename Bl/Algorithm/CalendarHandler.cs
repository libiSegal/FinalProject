using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.Algorithm
{
    public  class CalendarHandler
    {
        
        public  Dictionary<DateTime, Dictionary<string, Category>> DatesLessThan24Hours(Dictionary<DateTime, Dictionary<string, Category>> datesDict) 

         => datesDict.Where(k => (k.Key - DateTime.Now).TotalHours < 24 && (k.Key - DateTime.Now).TotalHours >= 0).ToDictionary(k => k.Key , v=> v.Value);



    }
}

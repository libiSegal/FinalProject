using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.DataImplementation.ServiceInterfaces
{
    public interface ICalendarService
    {
        Dictionary<WashAbleDTO, DateTime> GetNecessaryWasAbles(CalendarDTO calendar,
            List<WashAbleDTO> cleanWashAbles, List<WashAbleDTO> dirtyWashAbles);
    }
}


namespace BL.DataImplementation.ServiceInterfaces;
public interface ICalendarService
{
    Dictionary<WashAbleDTO, DateTime> GetNecessaryWashAbles(CalendarDTO calendar,
        List<WashAbleDTO> cleanWashAbles, List<WashAbleDTO> dirtyWashAbles);
    public void ElapsedDates(ManagerDTO manager , List<WashAbleDTO> allWashable);
}


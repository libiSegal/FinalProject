namespace Bl.Algorithm
{
    public interface ICalendarHandler
    {
        Dictionary<WashAbleDTO, DateTime> GetNecessaryWasAbles(Dictionary<DateTime, Dictionary<string, List<Category>>> calendar, int spareDays, 
            List<WashAbleDTO> cleanWashAbles , List<WashAbleDTO> DirtyWashAbles);
    
    }
}
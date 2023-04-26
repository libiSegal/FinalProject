namespace Bl.Algorithm
{
    public interface ICalendarHandler
    {
        List<WashAbleDTO> ChangeCategoriesToWashAbles(KeyValuePair<string, List<Category>> userDict, List<WashAbleDTO> cleanWashAbles, List<WashAbleDTO> dirtyWashAbles);
        Dictionary<DateTime, Dictionary<string, List<Category>>> DatesLessThan24Hours(Dictionary<DateTime, Dictionary<string, List<Category>>> datesDict);
        bool ExistsInClean(string userID, Category category, List<WashAbleDTO> cleanWashAbles);
        Dictionary<WashAbleDTO, DateTime> FinallyDict(Dictionary<DateTime, Dictionary<string, List<WashAbleDTO>>> usersWithWashAble);
        Dictionary<DateTime, Dictionary<string, List<WashAbleDTO>>> FindWashAbleToWash(Dictionary<DateTime, Dictionary<string, List<Category>>> specificDateDict, List<WashAbleDTO> allWashAbles);
        List<WashAbleDTO> GetCleanWashables(List<WashAbleDTO> allWashAbles);
        List<WashAbleDTO> GetDirtyWashables(List<WashAbleDTO> allWashAbles);
        Dictionary<WashAbleDTO, DateTime> GetNecessaryWasAbles(Dictionary<DateTime, Dictionary<string, List<Category>>> calendar, List<WashAbleDTO> allWashAbles);
       Task<WashAbleDTO> NecessaryWashAble(string userID, Category category, List<WashAbleDTO> dirtyWashAbles);
    }
}
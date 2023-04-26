namespace Bl.Algorithm
{
    public interface ISchedulerService
    {
        List<WashAblesCollection> ListOfWashablesCollections { get; set; }

        List<WashAbleDTO> AllWashAbles(List<WashAbleDTO> managerItems, List<UserDTO> users);
        double CalculateNacessaryPoints(List<WashAbleDTO> necessaryList, Dictionary<WashAbleDTO, DateTime> necessaryDatesDict);
        Dictionary<string, List<WashAbleDTO>> FlatWashablesCollections();
        List<WashAbleDTO> GetDirtyWashables(List<WashAbleDTO> allWashAbles);
        Dictionary<string, List<WashAbleDTO>> Scheduler(ManagerDTO manager);
        void SortByTotalPointsWeight();
        void SortToCollections(List<WashAbleDTO> dirtyWashabels);
        void WeightingCollection(Dictionary<WashAbleDTO, DateTime> necessaryDatesDict);
    }
}
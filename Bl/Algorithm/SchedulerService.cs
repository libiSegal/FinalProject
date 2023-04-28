
using System.Collections;
using System.Diagnostics;

namespace Bl.Algorithm;

public class SchedulerService : ISchedulerService
{
    private List<WashAblesCollection> _listOfWashablesCollections { get; set; } = new();
    ICalendarHandler _calendarHandler;
    const int AllPrecent = 100;
    public SchedulerService(ICalendarHandler calendarHandler)
    {
        _calendarHandler = calendarHandler;
    }
    public Dictionary<string, List<WashAbleDTO>> Scheduler(ManagerDTO manager)
    {
        List<WashAbleDTO> allWashAbles = AllWashAbles(manager.Items, manager.UsersDTO);
        List<WashAbleDTO> dirtyWashAbles = GetDirtyWashables(allWashAbles);
        List<WashAbleDTO> cleanWashAbles = GetCleanWashables(allWashAbles);

        Dictionary<WashAbleDTO, DateTime> necessaryWashAbles = _calendarHandler.GetNecessaryWasAbles(manager.Calendar, 1, cleanWashAbles, dirtyWashAbles);

        SortToCollections(dirtyWashAbles);

        WeightingCollection(necessaryWashAbles);

        SortByTotalPointsWeight();

        return FlatWashablesCollections();
    }

    //return a list of all washables 
    private List<WashAbleDTO> AllWashAbles(List<WashAbleDTO> managerItems, List<UserDTO> users)
    {
        List<WashAbleDTO> allWashAbles = managerItems;
        allWashAbles.AddRange(users.SelectMany(lst => lst.Items.Select(w => w)).ToList());
        return allWashAbles;
    }

    //sort to clean and dirty
    private List<WashAbleDTO> GetDirtyWashables(List<WashAbleDTO> allWashAbles) => allWashAbles.FindAll(w => w.Status == Status.dirty);
    private List<WashAbleDTO> GetCleanWashables(List<WashAbleDTO> allWashAbles) => allWashAbles.FindAll(w => w.Status == Status.clean);
    //sort to collections 
    private void SortToCollections(List<WashAbleDTO> dirtyWashabels)
    {
        Dictionary<string, WashAblesCollection> dictionaryCollectionsTypes = new();
        dirtyWashabels.ForEach(item =>
        {
            if (!dictionaryCollectionsTypes.ContainsKey(item.CollectionType))
            {
                dictionaryCollectionsTypes.Add(item.CollectionType, new WashAblesCollection(item.CollectionType));
            }
            dictionaryCollectionsTypes[item.CollectionType].AddWashableToCollection(item);
        });
        _listOfWashablesCollections = dictionaryCollectionsTypes.Values.ToList();

    }
    
    //calculte the weight for each collection;
    private void WeightingCollection(Dictionary<WashAbleDTO, DateTime> necessaryDatesDict)
    {
        
        double criticalPoints = 0, necessaryPoints = 0, standardPoints = 0, totalPoints = 0;
        double criticalPercent = 0, necesseryPercent = 0, standardPercent = 0;

        _listOfWashablesCollections.ForEach(collation =>
        {
            criticalPoints += collation.WashAblesSortedByNecessary[(int)NecessityLevel.critical].Count;
            necessaryPoints += collation.WashAblesSortedByNecessary[(int)NecessityLevel.necessary].Count;
            standardPoints += collation.WashAblesSortedByNecessary[(int)NecessityLevel.standard].Count;

        });

        totalPoints += criticalPoints + necessaryPoints + standardPoints;

        criticalPercent = (criticalPoints / totalPoints) * AllPrecent;
        necesseryPercent = (necessaryPoints / totalPoints) * AllPrecent;
        standardPercent = (standardPoints / totalPoints) * AllPrecent;
        _listOfWashablesCollections.ForEach(collation =>
        {
            collation.TotalPointsWeight = Math.Pow(collation.WashAblesSortedByNecessary[(int)NecessityLevel.critical].Count * (AllPrecent - criticalPercent), 2) +
            CalculateNacessaryPoints(collation.WashAblesSortedByNecessary[(int)NecessityLevel.necessary], necessaryDatesDict) +
            collation.WashAblesSortedByNecessary[(int)NecessityLevel.standard].Count * (AllPrecent - standardPoints);
        });
    }
    //calculte the necessary weightPoints for each collection;
    private double CalculateNacessaryPoints(List<WashAbleDTO> necessaryList, Dictionary<WashAbleDTO, DateTime> necessaryDatesDict)
    {
        const double PercentagesMultiply = 0.1 ;
        double points = 0;
        necessaryList.ForEach(washAble =>
        {
            points += (24 - (necessaryDatesDict[washAble] - DateTime.Now).TotalHours) * PercentagesMultiply + 1 ;
        });
        return points;
    }
    private void SortByTotalPointsWeight()
    {
        _listOfWashablesCollections.Sort((first, second) => first.TotalPointsWeight.CompareTo(second.TotalPointsWeight));
    }

    private Dictionary<string, List<WashAbleDTO>> FlatWashablesCollections()
    {
        Dictionary<string, List<WashAbleDTO>> result = new();
        _listOfWashablesCollections.ForEach(collection => result.Add(collection.Type, collection.GetWashAblesSortedByNecessary()));
        return result;
    }

}

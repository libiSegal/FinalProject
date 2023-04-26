
using System.Collections;
using System.Diagnostics;

namespace Bl.Algorithm;

public class SchedulerService : ISchedulerService
{
    public List<WashAblesCollection> ListOfWashablesCollections { get; set; } = new();
    ICalendarHandler _calendarHandler;
    public SchedulerService(ICalendarHandler calendarHandler)
    {
        _calendarHandler = calendarHandler;
    }
    public Dictionary<string, List<WashAbleDTO>> Scheduler(ManagerDTO manager)
    {
        List<WashAbleDTO> allWashAbles = AllWashAbles(manager.Items, manager.UsersDTO);
        Dictionary<WashAbleDTO, DateTime> necessaryWashAbles = _calendarHandler.GetNecessaryWasAbles(manager.Calendar, allWashAbles);
        //allWashAbles = AllWashAbles(manager.Items, manager.UsersDTO);

        allWashAbles = GetDirtyWashables(allWashAbles);

        SortToCollections(allWashAbles);

        WeightingCollection(necessaryWashAbles);

        SortByTotalPointsWeight();

        return FlatWashablesCollections();
    }

    //return a list of all washables 
    public List<WashAbleDTO> AllWashAbles(List<WashAbleDTO> managerItems, List<UserDTO> users)
    {
        List<WashAbleDTO> allWashAbles = managerItems;
        allWashAbles.AddRange(users.SelectMany(lst => lst.Items.Select(w => w)).ToList());
        return allWashAbles;
    }

    //sort to clean and dirty
    public List<WashAbleDTO> GetDirtyWashables(List<WashAbleDTO> allWashAbles) => allWashAbles.FindAll(w => w.Status == Status.dirty);

    //sort to collections 
    public void SortToCollections(List<WashAbleDTO> dirtyWashabels)
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
        ListOfWashablesCollections = dictionaryCollectionsTypes.Values.ToList();

    }

    //calculte the weight for each collection;
    public void WeightingCollection(Dictionary<WashAbleDTO, DateTime> necessaryDatesDict)
    {
        //Dictionary<WashAbleDTO, DateTime> necessaryDatesDict = new();
        double criticalPoints = 0, necessaryPoints = 0, standardPoints = 0, totalPoints = 0;
        double criticalPercent = 0, necesseryPercent = 0, standardPercent = 0;

        ListOfWashablesCollections.ForEach(collation =>
        {
            criticalPoints += collation.WashAblesSortedByNecessary[(int)NecessityLevel.critical].Count;
            necessaryPoints += collation.WashAblesSortedByNecessary[(int)NecessityLevel.necessary].Count;
            standardPoints += collation.WashAblesSortedByNecessary[(int)NecessityLevel.standard].Count;

        });

        totalPoints += criticalPoints + necessaryPoints + standardPoints;

        criticalPercent = (criticalPoints / totalPoints) * 100;
        necesseryPercent = (necessaryPoints / totalPoints) * 100;
        standardPercent = (standardPoints / totalPoints) * 100;
        int i = 0;
        ListOfWashablesCollections.ForEach(collation =>
        {

            collation.TotalPointsWeight = Math.Pow(collation.WashAblesSortedByNecessary[(int)NecessityLevel.critical].Count * (100 - criticalPercent), 2) +
            CalculateNacessaryPoints(collation.WashAblesSortedByNecessary[(int)NecessityLevel.necessary], necessaryDatesDict) +
            collation.WashAblesSortedByNecessary[(int)NecessityLevel.standard].Count * (100 - standardPoints);
        });

    }

    public double CalculateNacessaryPoints(List<WashAbleDTO> necessaryList, Dictionary<WashAbleDTO, DateTime> necessaryDatesDict)
    {
        double points = 0;
        necessaryList.ForEach(washAble =>
        {
            points += (24 - (necessaryDatesDict[washAble] - DateTime.Now).TotalHours) * 0.1 + 1;
        });
        return points;
    }
    public void SortByTotalPointsWeight()
    {
        ListOfWashablesCollections.Sort((first, second) => first.TotalPointsWeight.CompareTo(second.TotalPointsWeight));
    }

    public Dictionary<string, List<WashAbleDTO>> FlatWashablesCollections()
    {
        Dictionary<string, List<WashAbleDTO>> result = new();
        ListOfWashablesCollections.ForEach(collection => result.Add(collection.Type, collection.GetWashAblesSortedByNecessary()));
        return result;
    }

}

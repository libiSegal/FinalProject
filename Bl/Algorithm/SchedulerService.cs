
namespace BL.Algorithm;
public class SchedulerService : ISchedulerService
{
    private List<WashAblesCollection> _listOfWashablesCollections { get; set; } = new();
    private readonly ICalendarService _calendarService;   
    public SchedulerService(ICalendarService calendarService) 
    {
       _calendarService = calendarService;
    }
    #region Scheduler
    public Dictionary<string, List<WashAbleDTO>> Scheduler(ManagerDTO manager)
    {
        List<WashAbleDTO> allWashAbles = AllWashAbles(manager.Items, manager.UsersDTO);
        List<WashAbleDTO> dirtyWashAbles = GetDirtyWashables(allWashAbles);
        List<WashAbleDTO> cleanWashAbles = GetCleanWashables(allWashAbles);

        Dictionary<WashAbleDTO, DateTime> necessaryWashAbles = _calendarService.GetNecessaryWashAbles(manager.Calendar, cleanWashAbles, dirtyWashAbles);

        SortToCollections(dirtyWashAbles);

        ShareCollectionsByWeight(manager.WashingMachineWeight);

        WeightingCollection(necessaryWashAbles);

        SortByTotalPointsWeight();

        return FlatWashablesCollections();
    }
    #endregion

    #region Getall wash ables
    //return a list of all washables 
    private List<WashAbleDTO> AllWashAbles(List<WashAbleDTO> managerItems, List<UserDTO> users)
    {
        List<WashAbleDTO> allWashAbles = managerItems;
        allWashAbles.AddRange(users.SelectMany(lst => lst.Items.Select(w => w)).ToList());
        return allWashAbles;
    }
    #endregion

    #region Get clean and dirty wash ables
    //sort to clean and dirty
    private List<WashAbleDTO> GetDirtyWashables(List<WashAbleDTO> allWashAbles) => allWashAbles.FindAll(w => w.Status == Status.dirty);
    private List<WashAbleDTO> GetCleanWashables(List<WashAbleDTO> allWashAbles) => allWashAbles.FindAll(w => w.Status == Status.clean);
    #endregion

    #region Sort to collections
    //sort the dirty wash ables to collections 
    private void SortToCollections(List<WashAbleDTO> dirtyWashAbels)
    {
        Dictionary<string, WashAblesCollection> dictionaryCollectionsTypes = new();
        dirtyWashAbels.ForEach(item =>
        {
            if (!dictionaryCollectionsTypes.ContainsKey(item.CollectionType))
            {
                dictionaryCollectionsTypes.Add(item.CollectionType, new WashAblesCollection(item.CollectionType));
            }
            dictionaryCollectionsTypes[item.CollectionType].AddWashableToCollection(item);
        });
        _listOfWashablesCollections = dictionaryCollectionsTypes.Values.ToList();

    }
    #endregion#region Share collections by weight

    #region Share collections by weight
    //Share too big collections to small collections
    private void ShareCollectionsByWeight(int washingMachineWeight)
    {
        _listOfWashablesCollections.ForEach(w => w.Type += " ");
        for (int index = 0; index < _listOfWashablesCollections.Count; index++)
        {
            if (_listOfWashablesCollections[index].Weight > washingMachineWeight)
            {
                double currentWeight = 0;
                List<WashAbleDTO> washAblesList = _listOfWashablesCollections[index].GetWashAblesSortedByNecessary();
                WashAblesCollection firstPartOfTheList = new(_listOfWashablesCollections[index].Type);
                WashAblesCollection secondPartOfTheList = new(_listOfWashablesCollections[index].Type);
                firstPartOfTheList.SetWashAblesSortedByNecessaryFromList(
                    washAblesList.TakeWhile(w => (currentWeight += w.Weight) < washingMachineWeight).ToList());
                secondPartOfTheList.SetWashAblesSortedByNecessaryFromList(_listOfWashablesCollections[index].
                    GetWashAblesSortedByNecessary().Except(firstPartOfTheList.GetWashAblesSortedByNecessary()).ToList());
                _listOfWashablesCollections[index] = firstPartOfTheList;
                secondPartOfTheList.Type += "*";
                _listOfWashablesCollections.Add(secondPartOfTheList);
            }
        };
    }
    #endregion

    #region Weighting collection
    //calculte the weight for each collection;
    private void WeightingCollection(Dictionary<WashAbleDTO, DateTime> necessaryDatesDict)
    {
        const int AllPrecent = 100;
        double criticalPoints = 0, necessaryPoints = 0, standardPoints = 0, totalPoints = 0;
        double criticalPercent = 0; 

        _listOfWashablesCollections.ForEach(collection =>
        {
            criticalPoints += collection.WashAblesSortedByNecessary[(int)NecessityLevel.critical].Count;
            necessaryPoints += collection.WashAblesSortedByNecessary[(int)NecessityLevel.necessary].Count;
            standardPoints += collection.WashAblesSortedByNecessary[(int)NecessityLevel.standard].Count;
        });

        totalPoints = criticalPoints + necessaryPoints + standardPoints;
        criticalPercent = (criticalPoints / totalPoints) * AllPrecent;
        _listOfWashablesCollections.ForEach(collation =>
        {
            collation.TotalPointsWeight = Math.Pow(collation.WashAblesSortedByNecessary[(int)NecessityLevel.critical].Count * (AllPrecent - criticalPercent), 2) +
            CalculateNacessaryPoints(collation.WashAblesSortedByNecessary[(int)NecessityLevel.necessary], necessaryDatesDict)+
            CalculateStandardPoints(collation.WashAblesSortedByNecessary[(int)NecessityLevel.standard]);
        });
    }
    #endregion

    #region Calculate nacessary points
    //calculte the necessary weightPoints for each collection;
    private double CalculateNacessaryPoints(List<WashAbleDTO> necessaryList, Dictionary<WashAbleDTO, DateTime> necessaryDatesDict)
    {
        const int oneDay = 24;
        double points = 0;
        necessaryList.ForEach(washAble =>
        {
            points += (oneDay - (necessaryDatesDict[washAble] - DateTime.Now).TotalHours);
        });
        return points;
    }
    #endregion

    #region Calculate standard points
    //calculte the standard weightPoints for each collection;
    private double CalculateStandardPoints(List<WashAbleDTO> standardList)
    {
        const double relativePercent = 0.01;
        double points = standardList.Count;
        standardList.ForEach(washAble =>
        {
            points += (DateTime.Now - washAble.EnteryDate).TotalHours * relativePercent;
        });
        return points;
    }
    #endregion

    #region Sort by total points weight
    private void SortByTotalPointsWeight()
    {
        _listOfWashablesCollections.Sort((first, second) => first.TotalPointsWeight.CompareTo(second.TotalPointsWeight));
    }
    #endregion

    #region Flat washables collections
    private Dictionary<string, List<WashAbleDTO>> FlatWashablesCollections()
    {
        Dictionary<string, List<WashAbleDTO>> result = new();
        _listOfWashablesCollections.ForEach(collection => result.Add(collection.Type, collection.GetWashAblesSortedByNecessary()));
        return result;
    }
    #endregion   
}
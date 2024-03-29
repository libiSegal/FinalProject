﻿
using MongoDB.Driver.Linq;
using System.Diagnostics;
using System.Linq;

namespace Bl.DataImplementation.ServiceClasses;
public class CalendarService : ICalendarService
{
    private readonly IWashAbleService _washAbleService;
    private readonly IManagerService _managerService;
    public CalendarService(IWashAbleService washAbleService, IManagerService managerService)
    {
        _washAbleService = washAbleService;
        _managerService = managerService;
    }

    #region Get necessary wash ables
    //The main function of CalanderService. returns the necessery wash ables acording to the calendar
    public Dictionary<WashAbleDTO, DateTime> GetNecessaryWashAbles(CalendarDTO calendar,
            List<WashAbleDTO> cleanWashAbles, List<WashAbleDTO> dirtyWashAbles)
    {
        Dictionary<DateTime, Dictionary<string, List<WashAbleDTO>>> conciseCalendar;

        calendar.WashAbleCalendar = GetWashAbleAcordingSoonDates(calendar.WashAbleCalendar);

        conciseCalendar = FindWashAbleForUserAccordingCategory(calendar.WashAbleCalendar, cleanWashAbles, dirtyWashAbles);

        return FlatDictionary(conciseCalendar);
    }
    #endregion

    #region Get wash able acording soon date
    private static Dictionary<DateTime, Dictionary<string, List<Category>>> GetWashAbleAcordingSoonDates(Dictionary<DateTime, Dictionary<string, List<Category>>> datesDict)
        => datesDict.Where(k => (k.Key - DateTime.Now).TotalHours <= (24) && (k.Key - DateTime.Now).TotalHours >= 0).ToDictionary(k => k.Key, v => v.Value);
        
    
    #endregion

    #region Find wash able for user according category
    //Find match wash able - clean or dirty - for every category of every user, and handle the wash ables accordingly to the status
    private Dictionary<DateTime, Dictionary<string, List<WashAbleDTO>>> FindWashAbleForUserAccordingCategory(Dictionary<DateTime,
        Dictionary<string, List<Category>>> specificDateDict, List<WashAbleDTO> cleanWashAbles, List<WashAbleDTO> dirtyWashAbles)
    {
        Dictionary<DateTime, Dictionary<string, List<WashAbleDTO>>> usersWithWashAble = new();
        specificDateDict.ToList().ForEach(dict =>
        {
            usersWithWashAble.Add(dict.Key, new Dictionary<string, List<WashAbleDTO>>());
            dict.Value.ToList().ForEach(user =>
            {
                usersWithWashAble[dict.Key]
                .Add(user.Key, ChangeCategoriesToWashAbles(user, cleanWashAbles, dirtyWashAbles));
            });
        });
        return usersWithWashAble;
    }
    #endregion

    #region Change the categories to washAbles
    //Search a match wash able for every category 
    private List<WashAbleDTO> ChangeCategoriesToWashAbles(KeyValuePair<string, List<Category>> userDict, List<WashAbleDTO> cleanWashAbles, List<WashAbleDTO> dirtyWashAbles)
    {
        List<WashAbleDTO> necessaryWashAble = new();
        userDict.Value.ForEach(category =>
        {
            if (!ExistsInClean(userDict.Key, category, cleanWashAbles))
                necessaryWashAble.Add(NecessaryWashAble(userDict.Key, category, dirtyWashAbles).Result);
        });
        return necessaryWashAble;
    }
    #endregion

    #region Exists in clean
    //Check if exist clean wash able with this category
    private static bool ExistsInClean(string userID, Category category, List<WashAbleDTO> cleanWashAbles)
        => cleanWashAbles.Find(w => w.UserId == userID && w.Category == category) != null;
    #endregion

    #region Necessary wash able
    //Update the NecessaryLevel for the necessery category
    public async Task<WashAbleDTO> NecessaryWashAble(string userID, Category category, List<WashAbleDTO> dirtyWashAbles)
    {
        WashAbleDTO? washAble = dirtyWashAbles.Find(w => w.UserId == userID && w.Category == category);
        Debug.WriteLine(washAble);
        if (washAble != null && (washAble.NecessityLevel != NecessityLevel.necessary || washAble.NecessityLevel != NecessityLevel.critical))
        {
            washAble.NecessityLevel = NecessityLevel.necessary;
            await _washAbleService.UpdateObject(washAble);
            return washAble;
        }
        else
            throw new BLException("There is not exists a washable with this user and category", 400);
    }
    #endregion

    public async void ElapsedDates(ManagerDTO manager, List<WashAbleDTO> allwashables)
    {
        manager.Calendar.WashAbleCalendar.ToList()
                .ForEach(date => RemovElapsedDates(manager.Calendar, allwashables, DateTime.Now, date.Key));
        await _managerService.UpdateObject(manager);
    }
    private void RemovElapsedDates(CalendarDTO calendar, List<WashAbleDTO> allwashables, DateTime fromDate, DateTime expireDate)
    {
        List<WashAbleDTO> washAblesToUpdate = new();
        if (fromDate - expireDate > TimeSpan.FromHours(2))
        {
            Debug.WriteLine(expireDate);
            calendar.WashAbleCalendar.Remove(expireDate);

        }
        allwashables.ForEach(washAble =>
        {
            if (washAble.NecessityLevel == NecessityLevel.necessary)
                washAble.NecessityLevel = NecessityLevel.standard;
            _washAbleService.UpdateObject(washAble);
        });
    }

    #region Flat dictionary
    private static Dictionary<WashAbleDTO, DateTime> FlatDictionary(Dictionary<DateTime, Dictionary<string, List<WashAbleDTO>>> usersWithWashAble)
    {
        Dictionary<WashAbleDTO, DateTime> finallyDict = new();
        usersWithWashAble.ToList().ForEach(dict =>
        {
            dict.Value.ToList().ForEach(user =>
            {
                user.Value.ForEach(w => finallyDict.Add(w, dict.Key));
            });
        });
        return finallyDict;
    }
    #endregion


}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.Algorithm
{
    public class CalendarHandler : ICalendarHandler
    {
        IWashAbleService _washAbleService;
        public CalendarHandler(IWashAbleService washAbleService)
        {
            _washAbleService = washAbleService;
        }

        public Dictionary<WashAbleDTO, DateTime> GetNecessaryWasAbles(Dictionary<DateTime, Dictionary<string, List<Category>>> calendar, List<WashAbleDTO> allWashAbles)
        {
            Dictionary<DateTime, Dictionary<string, List<WashAbleDTO>>> conciseCalendar;

            calendar = GetWashAbleAcordingSoonDates(calendar);

            conciseCalendar = FindWashAbleToWash(calendar, allWashAbles);

            return FinallyDict(conciseCalendar);

        }

        public Dictionary<DateTime, Dictionary<string, List<Category>>> GetWashAbleAcordingSoonDates(Dictionary<DateTime, Dictionary<string, List<Category>>> datesDict)

             => datesDict.Where(k => (k.Key - DateTime.Now).TotalHours < 24 && (k.Key - DateTime.Now).TotalHours >= 0).ToDictionary(k => k.Key, v => v.Value);

        public Dictionary<DateTime, Dictionary<string, List<WashAbleDTO>>> FindWashAbleToWash(Dictionary<DateTime, Dictionary<string, List<Category>>> specificDateDict,List<WashAbleDTO> allWashAbles)
            
        {
            Dictionary<DateTime, Dictionary<string, List<WashAbleDTO>>> usersWithWashAble = new();

            List<WashAbleDTO> cleanWashAbles = GetCleanWashables(allWashAbles);
            List<WashAbleDTO> dirtyWashAbles = GetDirtyWashables(allWashAbles);
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
        public List<WashAbleDTO> ChangeCategoriesToWashAbles(KeyValuePair<string, List<Category>> userDict, List<WashAbleDTO> cleanWashAbles, List<WashAbleDTO> dirtyWashAbles)
        {

            List<WashAbleDTO> necessaryWashAble = new();
            userDict.Value.ForEach(category => 
            {
                if (!ExistsInClean(userDict.Key, category, cleanWashAbles))
                {
                    necessaryWashAble.Add(NecessaryWashAble(userDict.Key, category, dirtyWashAbles).Result);
                }

            });
            return necessaryWashAble;
        }
        public Dictionary<WashAbleDTO, DateTime> FinallyDict(Dictionary<DateTime, Dictionary<string, List<WashAbleDTO>>> usersWithWashAble)
        {
            Dictionary<WashAbleDTO, DateTime> finallyDict = new Dictionary<WashAbleDTO, DateTime>();
            usersWithWashAble.ToList().ForEach(dict =>
            {
                dict.Value.ToList().ForEach(user =>
                {
                    user.Value.ForEach(w => finallyDict.Add(w, dict.Key));
                });
            });
            return finallyDict;
        }
        public List<WashAbleDTO> GetCleanWashables(List<WashAbleDTO> allWashAbles) => allWashAbles.FindAll(w => w.Status == Status.clean);
        public List<WashAbleDTO> GetDirtyWashables(List<WashAbleDTO> allWashAbles) => allWashAbles.FindAll(w => w.Status == Status.dirty);
        public bool ExistsInClean(string userID, Category category, List<WashAbleDTO> cleanWashAbles)
            => cleanWashAbles.Find(w => w.UserId == userID && w.Category == category) != null;

        public async Task<WashAbleDTO> NecessaryWashAble(string userID, Category category, List<WashAbleDTO> dirtyWashAbles)
        {
            WashAbleDTO washAble = dirtyWashAbles.First(w => w.UserId == userID && w.Category == category);
            if (washAble != null && (washAble.NecessityLevel != NecessityLevel.necessary || washAble.NecessityLevel != NecessityLevel.critical))
            {
                washAble.NecessityLevel = NecessityLevel.necessary;
                await _washAbleService.UpdateObject(washAble);
                return washAble;
            }
            else throw new Exception("There is not exists a washable with this condition");

        }

    }
}

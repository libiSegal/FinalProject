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

        public Dictionary<WashAbleDTO, DateTime> GetNecessaryWasAbles(Dictionary<DateTime, Dictionary<string, List<Category>>> calendar, int spareDays,
            List<WashAbleDTO> cleanWashAbles, List<WashAbleDTO> dirtyWashAbles)
        {
            Dictionary<DateTime, Dictionary<string, List<WashAbleDTO>>> conciseCalendar;

            calendar = GetWashAbleAcordingSoonDates(calendar, spareDays);

            conciseCalendar = FindWashAbleForUserAccordingCategory(calendar, cleanWashAbles, dirtyWashAbles);

            return simplifiedDictionary(conciseCalendar);

        }

        private Dictionary<DateTime, Dictionary<string, List<Category>>> GetWashAbleAcordingSoonDates(Dictionary<DateTime, Dictionary<string, List<Category>>> datesDict, int spareDays)

             => datesDict.Where(k => (k.Key - DateTime.Now).TotalHours < (spareDays * 24) && (k.Key - DateTime.Now).TotalHours >= 0).ToDictionary(k => k.Key, v => v.Value);

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
        private List<WashAbleDTO> ChangeCategoriesToWashAbles(KeyValuePair<string, List<Category>> userDict, List<WashAbleDTO> cleanWashAbles, List<WashAbleDTO> dirtyWashAbles)
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


        private bool ExistsInClean(string userID, Category category, List<WashAbleDTO> cleanWashAbles)
            => cleanWashAbles.Find(w => w.UserId == userID && w.Category == category) != null;

        public async Task<WashAbleDTO> NecessaryWashAble(string userID, Category category, List<WashAbleDTO> dirtyWashAbles)
        {
            WashAbleDTO? washAble = dirtyWashAbles.Find(w => w.UserId == userID && w.Category == category);
            if (washAble != null && (washAble.NecessityLevel != NecessityLevel.necessary || washAble.NecessityLevel != NecessityLevel.critical))
            {
                washAble.NecessityLevel = NecessityLevel.necessary;
                await _washAbleService.UpdateObject(washAble);
                return washAble;
            }
            else 
                throw new Exception("There is not exists a washable with this user and category");

        }
        private Dictionary<WashAbleDTO, DateTime> simplifiedDictionary(Dictionary<DateTime, Dictionary<string, List<WashAbleDTO>>> usersWithWashAble)
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
    }
}

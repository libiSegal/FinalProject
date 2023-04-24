using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.Algorithm
{
    public class CalendarHandler
    {

        public Dictionary<DateTime, Dictionary<string, Category>> DatesLessThan24Hours(Dictionary<DateTime, Dictionary<string, Category>> datesDict)

             => datesDict.Where(k => (k.Key - DateTime.Now).TotalHours < 24 && (k.Key - DateTime.Now).TotalHours >= 0).ToDictionary(k => k.Key, v => v.Value);
        public Dictionary<DateTime, List<WashAbleDTO>> FindWashAbleToWash(Dictionary<DateTime, Dictionary<string, Category>> specificDateDict)
        {
          
        }

        public static List<WashAbleDTO> GetCleanWashables(List<WashAbleDTO> allWashAbles) => allWashAbles.FindAll(w => w.Status == Status.clean);

        public bool ExistsInClean(string userID, Category category, List<WashAbleDTO> cleanWashAbles)
            => cleanWashAbles.First(w => w.UserId == userID && w.Category == category) != null;

        public WashAbleDTO NecessaryWashAble(string userID, Category category, List<WashAbleDTO> dirtyWashAbles)
        {
            WashAbleDTO washAble = dirtyWashAbles.First(w => w.UserId == userID && w.Category == category);
            if (washAble != null)
            {
                washAble.NecessityLevel = NecessityLevel.necessary;
                return washAble;
            }
            else throw new Exception("There is not exists a washable with this condition");

        }
       
    }
}

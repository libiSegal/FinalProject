
namespace Dal.Models;
public class Calendar 
{
    public Dictionary<DateTime, Dictionary<string, List<Category>>> WashAbleCalendar { get; set; }
    public Calendar()
    {
        WashAbleCalendar = new();
    }
    public Calendar(Dictionary<DateTime, Dictionary<string, List<Category>>> calendar)
    {
        WashAbleCalendar = calendar;
    }
}



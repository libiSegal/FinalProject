
namespace BL.DTO;
public class CalendarDTO
{
    public Dictionary<DateTime, Dictionary<string, List<Category>>> WashAbleCalendar { get; set; }
    public CalendarDTO()
    {
        WashAbleCalendar = new();
    }
}
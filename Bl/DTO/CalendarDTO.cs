
namespace BL.DTO;
public class CalendarDTO
{
    public Dictionary<DateTime, Dictionary<string, List<Category>>> Calendar { get; set; }
    public CalendarDTO()
    {
        Calendar = new();
    }
    public CalendarDTO(Dictionary<DateTime, Dictionary<string, List<Category>>> calendar)
    {
        Calendar = calendar;
    }

}

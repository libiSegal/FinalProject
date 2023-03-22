
namespace BL.DTO;
public class CalendarDTO
{
    public List<Dictionary<DateTime, List<WashAbleDTO>>> Machines { get; set; }
    public CalendarDTO()
    {
        Machines = new List<Dictionary<DateTime, List<WashAbleDTO>>>();
    }
}

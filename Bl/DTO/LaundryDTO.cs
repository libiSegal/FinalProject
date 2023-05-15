
namespace BL.DTO;
public class LaundryDTO : IDataObject
{
    public string ID { get; set; }
    public string Type { get; set; }
    public DateTime Date { get; set; }
    public string ManagerID { get; set; }
    public List<WashAbleDTO> WashAbles { get; set; }
    public LaundryDTO()
    {
        ID = string.Empty;
        Type = string.Empty;
        Date = DateTime.Now;
        WashAbles = new();
        ManagerID = string.Empty;
    }
}

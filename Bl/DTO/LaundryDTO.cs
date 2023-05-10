
namespace BL.DTO;
public class LaundryDTO : IDataObject
{
    public string ID { get; set; }
    public string Type { get; set; }
    public DateTime Date { get; set; }
    public string ManagerID { get; set; }
    public List<WashAbleDTO> WashAbles { get; set; }//It doesn't have a set because there is no laundry without wash ables;
    public LaundryDTO()
    {
        ID = string.Empty;
        Type = string.Empty;
        Date = DateTime.Now;
        WashAbles = new();
        ManagerID = string.Empty;
    }
    public LaundryDTO(string type, DateTime dateTime, List<WashAbleDTO> washAbles, string managerId)
    {
        ID = string.Empty;
        Type = type;
        Date = dateTime;
        WashAbles = new(washAbles);
        ManagerID = managerId;
    }
}

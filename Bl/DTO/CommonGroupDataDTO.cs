
namespace BL.DTO;
public class CommonGroupDataDTO
{
    public string ID { get; set; }
    public string ManagerID { get; set; }
    public List<string> WashAblesCollectionTypes { get; set; }
    public CommonGroupDataDTO()
    {
        ID = string.Empty;
        ManagerID = string.Empty;
        WashAblesCollectionTypes = new();
    }
}
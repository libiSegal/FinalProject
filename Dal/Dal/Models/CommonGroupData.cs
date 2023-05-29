
namespace Dal.Models;
public class CommonGroupData
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string ID { get; set; }
    public List<string> WashAblesCollectionTypes { get; set; }
    public string ManagerID { get; set; }
    public CommonGroupData()
    {
        ID = string.Empty;
        WashAblesCollectionTypes = new();
        ManagerID = string.Empty;
    }
}
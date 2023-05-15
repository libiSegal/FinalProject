
namespace Dal.Models;
public class Laundry : IDataBaseObject//It doesn't have a set because history can't be update laundry is not!!! a embedded duocument ;
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string ID { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public string ManagerID { get; set; }
    public string Type { get; set; }
    public DateTime Date { get; set; }
    public List<string> WashAblesIDs { get; set; }
    public Laundry()
    {
        ID = string.Empty;
        Type = string.Empty;
        ManagerID = string.Empty;
        Type = string.Empty;
        WashAblesIDs = new();
    }
}

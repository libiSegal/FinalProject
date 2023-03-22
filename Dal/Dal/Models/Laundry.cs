
namespace Dal.Models;
public class Laundry : IDataBaseObject//It doesn't have a set because history can't be update laundry is not!!! a embedded duocument ;
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string ID { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public string ManagerID { get; set; }
    public string Name { get; }
    public DateTime Date { get; }
    public int WashingTime { get;  }
    public List<WashAble> WashAbles { get; }
    public Laundry(string name, DateTime dateTime, List<WashAble> washAbles, string managerID)
    {
        ID = "";
        Name = name;
        Date = dateTime;
        WashAbles = new(washAbles);
        ManagerID = managerID;
    }

}

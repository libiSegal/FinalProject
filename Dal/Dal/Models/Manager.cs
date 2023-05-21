
namespace Dal.Models;
public class Manager : User
{
    //public List<string> WashAblesCollectionTypes { get; set;}
    public int WashingMachineWeight { get; set; }
    public Calendar Calendar { get; set;}
    [BsonIgnore]
    public override string ManagerID { get; set; }
    public Manager() : base()
    {
       // WashAblesCollectionTypes = new();
        WashingMachineWeight = 1;
        ManagerID = string.Empty;
        Calendar = new();
    }
}

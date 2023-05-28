
namespace Dal.Models;
public class Manager : User
{
   // public List<string> WashAblesCollectionTypes { get; set;}
    public List<Laundry> Laundries { get; set; }

    readonly int minimumWashingMachineWeight = 1;
    public int WashingMachineWeight { get; set; }
    public Calendar Calendar { get; set;}

    [BsonIgnore]
    public override string ManagerID { get; set; }
    public Manager() : base()
    {
       // WashAblesCollectionTypes = new();
        Laundries = new();
        WashingMachineWeight = minimumWashingMachineWeight;
        ManagerID = string.Empty;
        Calendar = new();
    }
}
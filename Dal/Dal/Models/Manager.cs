
namespace Dal.Models;
public class Manager : User
{
    [BsonIgnore]
    readonly int _minimumWashingMachineWeight = 1;
    public List<Laundry> Laundries { get; set; }

    public int WashingMachineWeight { get; set; }
    public Calendar Calendar { get; set;}

    [BsonIgnore]
    public override string ManagerID { get; set; }
    public Manager() : base()
    {
        Laundries = new();     
        ManagerID = string.Empty;
        Calendar = new();
        WashingMachineWeight = _minimumWashingMachineWeight;
    }
}
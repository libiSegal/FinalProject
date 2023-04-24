
namespace Dal.Models;
public class Manager : User
{
    public List<string> WashAblesCollectionTypes { get; set; }
    public WashingMachine WashingMachine { get; set; }
    public List<Dictionary<DateTime, Dictionary<string, Category>>> Schedule { get; set; }
    //public Calendar Calendar { get; set; }

    [BsonIgnore]
    public override string ManagerID { get; set; }
    public Manager() : base()
    {
        WashAblesCollectionTypes = new();
        WashingMachine = new();
       // Calendar = new();
        ManagerID = "";
        Schedule = new();
    }
    public Manager(string name, string password, WashingMachine washingMachine) : base(name, password)
    {
        ManagerID = "";
       // Calendar = new();
        WashAblesCollectionTypes = new();
        WashingMachine = washingMachine;
        ActionPermissions = ActionPermission.a | ActionPermission.b;
        Schedule = new();
    }
    /*public Manager(string name, string password, WashingMachine washingMachine*//*, Calendar calendar*//*) :this(name, password, washingMachine)
    {
       // ManagerID = "";
       // Calendar = calendar;
       // WashingMachine = washingMachine;
       // WashAblesCollectionTypes = new();
        //ActionPermissions = ActionPermission.a | ActionPermission.b ;
       // Schedule = new();
    }*/

}



namespace Dal;

public class Manager : User
{
    
    public WashingMachine WashingMachine { get; set; }
    public Calendar Calendar { get; set; }

    [BsonIgnore]
    public override string ManagerID { get; set; }
    public Manager() : base()
    {
        WashingMachine = new("XXX" , "XXX" , new());
        Calendar = new();
        ManagerID = "";
    }
    public Manager(string name, string password, WashingMachine washingMachine, Calendar calendar) :base(name, password)
    {
        ManagerID = "";
        Calendar = calendar;
        WashingMachine = washingMachine;
        ActionPermissions = ActionPermission.a | ActionPermission.b ; 
    }

}

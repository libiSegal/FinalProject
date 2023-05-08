
using MongoDB.Bson.Serialization.Options;

namespace Dal.Models;
public class Manager : User
{
    public List<string> WashAblesCollectionTypes { get; set; }
    public WashingMachine WashingMachine { get; set; }
   // [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
    public Calendar Calendar { get; set; }

    [BsonIgnore]
    public override string ManagerID { get; set; }
    public Manager() : base()
    {
        WashAblesCollectionTypes = new();
        WashingMachine = new();
        ManagerID = "";
        Calendar = new();
    }
    /*public Manager(string name, string password, WashingMachine washingMachine) : base(name, password)
    {
        ManagerID = "";
        WashAblesCollectionTypes = new();
        WashingMachine = washingMachine;
        ActionPermissions = ActionPermission.a | ActionPermission.b;
        Calendar = new();
    }
    public Manager(string name, string password, WashingMachine washingMachine, Calendar calendar) : this(name, password, washingMachine)
    {
        Calendar = calendar;    
    }*/

}

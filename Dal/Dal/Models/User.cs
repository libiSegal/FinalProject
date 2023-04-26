namespace Dal.Models;

public class User : IDataBaseObject
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string ID { get; set; }

    public virtual string ManagerID { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }

    [BsonRepresentation(BsonType.String)]
    public ActionPermission ActionPermissions { get; set; }
   public User() 
    {
        ID = "";
        ManagerID = "";
        Password = "";
        Name = "";

    }
    public User(string name, string password)
    {
        ID = "";
        Name = name;
        Password = password;
        ManagerID = "";
    }
    public User(string name, string password, string managerId)
    {
        ID = "";
        Name = name;
        Password = password; 
        ManagerID = managerId;
    }
    public User(string name, string password, string managerId, ActionPermission actionPermissions) : this(name, password, managerId)
    {
        ActionPermissions = actionPermissions;
    }
}

namespace Dal.Models;
public class User : IDataBaseObject
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string ID { get; set; }
    public virtual string ManagerID { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public Gender Gender { get; set; }

    [BsonRepresentation(BsonType.String)]
    public ActionPermission ActionPermissions { get; set; }
    public User() 
    {
        ID = "";
        ManagerID = "";
        Password = "";
        Name = "";
    }
}

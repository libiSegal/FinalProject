using Dal;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Dal
{
    public class User : IDataBaseObject
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID { get; set; }
        private string ManagerId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public List<string> ItemsId { get; set; }
        [BsonRepresentation(BsonType.String)]
        public ActionPermission ActionPermissions { get; set; }
       public User() 
        {
            ID = "";
            ManagerId = "";
            Password = "";
            Name = "";
            ItemsId = new();
        }
        public User(string name, string password)
        {
            ID = "";
            Name = name;
            Password = password;
            ManagerId = "";
            ItemsId = new();
        }
        public User(string name, string password, string managerId)
        {
            ID = "";
            Name = name;
            Password = password; 
            ManagerId = managerId;
            ItemsId = new();
        }
        public User(string name, string password, string managerId, List<string> items, ActionPermission actionPermissions) : this(name, password, managerId)
        {
            ItemsId = new(items);
            ActionPermissions = actionPermissions;
        }
    }
}
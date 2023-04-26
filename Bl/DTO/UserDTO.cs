
namespace BL.DTO;

public class UserDTO : IDataObject
{

    public string ID { get; set; }
    public string ManagerID { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public List<WashAbleDTO> Items { get; set; }
   
    public ActionPermission ActionPermissions { get; set; }
 
    public UserDTO()
    {
        ID = "";
        Name = "";
        Password = "";
        ManagerID = "";
        Items = new List<WashAbleDTO>();
    }
    public UserDTO(string id , string name, string password, string managerId)
    {
        ID = id;
        Name = name;
        Password = password;
        ManagerID = managerId;
        Items = new List<WashAbleDTO>();
    }
    public UserDTO(string name, string password, string managerId)
    {
        ID = "";
        Name = name;
        Password = password;
        ManagerID = managerId;
        Items = new List<WashAbleDTO>();
    }
    public UserDTO(string id, string name, string password, string managerId, List<WashAbleDTO> items, ActionPermission actionPermissions) : this( id,name,password, managerId)
    {
        Items = items;
        ActionPermissions = actionPermissions;
    }
}


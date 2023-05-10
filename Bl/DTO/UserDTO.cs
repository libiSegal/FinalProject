
namespace BL.DTO;
public class UserDTO : IDataObject
{
    public string ID { get; set; }
    public string ManagerID { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public List<WashAbleDTO> Items { get; set; }
    public Gender Gender { get; set; }
    public ActionPermission ActionPermissions { get; set; }
    public UserDTO()
    {
        ID = string.Empty;
        Name = string.Empty;
        Password = string.Empty;
        ManagerID = string.Empty;
        Items = new List<WashAbleDTO>();
    }
}


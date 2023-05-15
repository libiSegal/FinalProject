
namespace Dal.Models;
public class User : IDataBaseObject
{
    /// <summary>
    /// Gets or sets the ID of the User. 
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string ID { get; set; }

    /// <summary>
    /// Gets or sets the ManagerID of the User. 
    /// </summary>
    public virtual string ManagerID { get; set; }

    /// <summary>
    /// Gets or sets the Name of the User. 
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the Password of the User.
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Gets or sets the Gender of the User.
    /// </summary>
    public Gender Gender { get; set; }
    public User() 
    {
        ID = string.Empty;
        ManagerID = string.Empty;
        Password = string.Empty;
        Name = string.Empty;
    }
}

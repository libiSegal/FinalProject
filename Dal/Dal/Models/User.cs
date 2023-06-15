
using InvalidDataException = Dal.Exceptions.InvalidDataException;

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
    /// Gets or sets the Password of the User.
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Gets or sets the Gender of the User.
    /// </summary>
    public Gender Gender { get; set; }

     private string name;

    public string Name
    {
        get { return name; }
        set { 
            if(value.Length < 2)
            {
                throw new InvalidDataException("Invalid name");
            }
            name = value;
        }
    }

    public User() 
    {
        ID = string.Empty;
        ManagerID = string.Empty;
        Password = string.Empty;
        name = string.Empty;
    }
}

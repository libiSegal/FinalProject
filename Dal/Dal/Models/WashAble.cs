
namespace Dal.Models;
public class WashAble : IDataBaseObject
{
    /// <summary>
    /// Gets or sets the ID of the WashAble.
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string ID { get;set; }
   
    /// <summary>
    ///  Gets or sets the Name of the WashAble.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the UserID of the WashAble.
    /// </summary>
    public string UserID { get; set; }

    /// <summary>
    /// Gets or sets the CollectionType of the WashAble.
    /// </summary>
    public string CollectionType { get; set; }

    /// <summary>
    /// Gets or sets the Status of the WashAble.
    /// </summary>

    [BsonRepresentation(BsonType.String)]
    public Status Status { get; set; }

    /// <summary>
    /// Gets or sets the NecessityLevel of the WashAble.
    /// </summary>

    [BsonRepresentation(BsonType.String)]
    public NecessityLevel NecessityLevel { get; set; }
    /// <summary>
    /// Gets or sets the Category of the WashAble.
    /// </summary>

    [BsonRepresentation(BsonType.String)]
    public Category Category { get; set; }

    /// <summary>
    /// Gets or sets the EntryDate of the WashAble.
    /// </summary>
    public DateTime EntryDate { get; set; }

    /// <summary>
    /// Gets or sets the Weight of the WashAble.
    /// </summary>
    public double Weight { get; set; }

    /// <summary>
    /// Gets or sets the MaxDeg of the WashAble.
    /// </summary>
    public int MaxDeg { get; set; }

    /// <summary>
    /// Gets or sets the MinDeg of the WashAble.
    /// </summary>
    public int MinDeg { get; set; }

    /// <summary>
    /// Gets or sets the MaxSqueezing of the WashAble.
    /// </summary>
    public int MaxSqueezing { get; set; }

    /// <summary>
    /// Gets or sets the MinSqueezing of the WashAble.
    /// </summary>
    public int MinSqueezing { get; set; }

    /// <summary>
    /// Gets or sets the Prevwash of the WashAble.
    /// </summary>
    public List<DateTime>? PrevWash { get; set; }
    public WashAble()
    {
        ID = string.Empty;
        Name = string.Empty;
        UserID = string.Empty;
        CollectionType = string.Empty;
    }
}

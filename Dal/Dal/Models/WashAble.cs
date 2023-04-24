
namespace Dal.Models;

public class WashAble : IDataBaseObject
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string ID { get; set; }
    public string Name { get; set; }

    public string UserID { get; set; }
    public string CollectionType { get; set; }

    [BsonRepresentation(BsonType.String)]
    public Status Status { get; set; }
    [BsonRepresentation(BsonType.String)]
    public NecessityLevel NecessityLevel { get; set; }

    [BsonRepresentation(BsonType.String)]
    public Category Category { get; set; }
    public int MaxDeg { get; set; }
    public int MinDeg { get; set; }
    public int MaxSqueezing { get; set; }
    public int MinSqueezing { get; set; }

    public List<DateTime>? PrevWash { get; set; }
    public WashAble()
    {
        ID = "";
        Name = "";
        UserID = "";
        CollectionType = "";
    }

    public WashAble(string name,string userId,  Status status ,NecessityLevel necessityLevel, Category category, int maxDeg, int maxSqueezing , string collectionType)
    {
        ID = "";
        Name = name;
        UserID = userId;
        Status = status;
        NecessityLevel = necessityLevel;
        Category = category;
        MaxDeg = maxDeg;
        MaxSqueezing = maxSqueezing;
        CollectionType = collectionType;
    }
    public WashAble( string name, string userId,  Status status ,NecessityLevel necessityLevel, Category category, int maxDeg,  int maxSqueezing,int minDeg, int minSqueezing , string collectionType):
        this(name,userId, status,necessityLevel, category,  maxDeg, maxSqueezing , collectionType)
    {     
        MinDeg = minDeg;
        MinSqueezing = minSqueezing;
    }




}

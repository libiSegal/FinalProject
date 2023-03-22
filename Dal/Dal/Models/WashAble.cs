
namespace Dal;

public class WashAble : IDataBaseObject
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string ID { get; set; }
    public string Name { get; set; }

    public string UserID { get; set; }

    [BsonRepresentation(BsonType.String)]
    public Colors Color { get; set; }
    [BsonRepresentation(BsonType.String)]
    public Status Status { get; set; }
    public int MaxDeg { get; set; }
    public int MinDeg { get; set; }
    public int MaxSqueezing { get; set; }
    public int MinSqueezing { get; set; }
    public bool NeedBoiling { get; set; }
    public bool ContainWool { get; set; }
    public bool ContainCoton { get; set; }
    [BsonRepresentation(BsonType.String)]
    public Category Category { get; set; }
    public List<DateTime>? PrevWash { get; set; }

    public WashAble(string name,string userId, Colors color, Status status , Category category, int maxDeg, int maxSqueezing)
    {
        ID = "";
        Name = name;
        UserID = userId;
        Color = color;
        Status = status;
        Category = category;
        MaxDeg = maxDeg;
        MaxSqueezing = maxSqueezing;
    }
    public WashAble( string name, string userId, Colors color, Status status, Category category, int maxDeg,  int maxSqueezing,int minDeg, int minSqueezing):
        this(name,userId, color, status, category, maxDeg, maxSqueezing)
    {     
        MinDeg = minDeg;
        MinSqueezing = minSqueezing;
    }

    public WashAble( string name, string userId, Colors color, Status status, Category category, int maxDeg, int maxSqueezing,int minDeg,int minSqueezing, bool needBoiling, bool containWool,bool containCoton)
        :this(name,userId, color, status, category, maxDeg, maxSqueezing, minDeg, minSqueezing)
    {
        NeedBoiling = needBoiling;
        ContainWool = containWool;
        ContainCoton = containCoton;
    }


}

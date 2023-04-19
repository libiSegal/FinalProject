
namespace BL.DTO;

public class WashAbleDTO : IDataObject
{
    public string ID { get; set; }
    public string Name { get; set; }
    public string UserId { get; set; }
    public Colors Color { get; set; }
    public Status Status { get; set; }
    public NecessityLevel NecessityLevel { get; set; }
    public int MaxDeg { get; set; }
    public int MinDeg { get; set; }
    public int MaxSqueezing { get; set; }
    public int MinSqueezing { get; set; }
    public bool NeedBoiling { get; set; }
    public bool ContainWool { get; set; }
    public bool ContainCoton { get; set; }
    public Category Category { get; set; }
    public List<DateTime> PrevWash { get; set; }

    public WashAbleDTO()
    {
        ID = "";
        Name = "";
        UserId = "";
        NecessityLevel = NecessityLevel.standard;
        PrevWash = new();
    }
    public WashAbleDTO(NecessityLevel necessityLevel)//need to delete this ctor
    {
       NecessityLevel = necessityLevel;
    }
    public WashAbleDTO(string name, string userId, Colors color, Status status,NecessityLevel necessityLevel, Category category, int maxDeg, int maxSqueezing)
    {
        ID = "";
        Name = name;
        UserId = userId;
        Color = color;
        Status = status;
        NecessityLevel = necessityLevel;
        Category = category;
        MaxDeg = maxDeg;
        MaxSqueezing = maxSqueezing;
        PrevWash = new();
    }
    public WashAbleDTO(string name, string userId, Colors color, Status status, NecessityLevel necessityLevel, Category category, int maxDeg, int maxSqueezing, int minDeg, int minSqueezing) 
        :this(name, userId, color, status,necessityLevel, category, maxDeg, maxSqueezing)
    {
        MinDeg = minDeg;
        MinSqueezing = minSqueezing;
    }

    public WashAbleDTO(string id,string name, string userId, Colors color, Status status, NecessityLevel necessityLevel, Category category, int maxDeg, int maxSqueezing, int minDeg, int minSqueezing) :
        this(name, userId, color, status,necessityLevel, category, maxDeg, maxSqueezing)
    {
        ID = id;
        MinDeg = minDeg;
        MinSqueezing = minSqueezing;
    }
    public WashAbleDTO(string id, string name, string userId, Colors color, Status status, NecessityLevel necessityLevel, Category category, int maxDeg, int maxSqueezing, int minDeg, int minSqueezing, bool needBoiling, bool containWool, bool containCoton)
        : this(name, userId, color, status,necessityLevel, category, maxDeg, maxSqueezing, minDeg, minSqueezing)
    {
        ID = id;
        NeedBoiling = needBoiling;
        ContainWool = containWool;
        ContainCoton = containCoton;
    }

    public override bool Equals(object? obj)
    {
        //Check for null and compare run-time types.
        if ((obj == null) || !this.GetType().Equals(obj.GetType()))
        {
            return false;
        }

        WashAbleDTO w = (WashAbleDTO)obj;
        return ID.Equals(w.ID) && Name.Equals(w.Name) && UserId.Equals(w.UserId) && Color.Equals(w.Color) && Status.Equals(w.Status)
            && Category.Equals(w.Category) && MaxDeg == w.MaxDeg && MaxSqueezing == w.MaxSqueezing && MinDeg == w.MinDeg && MinSqueezing == w.MinSqueezing
            && NeedBoiling == w.NeedBoiling && ContainCoton == w.ContainCoton && ContainWool == w.ContainWool && PrevWash.SequenceEqual(w.PrevWash);

    }

    public override int GetHashCode()
    {
        return MaxSqueezing ^ MaxDeg;
    }

}


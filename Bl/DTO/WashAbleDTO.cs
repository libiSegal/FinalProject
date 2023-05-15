
namespace BL.DTO;
public class WashAbleDTO : IDataObject
{
    public string ID { get; set; }
    public string Name { get; set; }
    public string UserId { get; set; }
    public string  CollectionType { get; set; }
    public Status Status { get; set; }
    public NecessityLevel NecessityLevel { get; set; }
    public Category Category { get; set; }
    public DateTime EnteryDate { get; set; }
    public double Weight { get; set; }
    public int MaxDeg { get; set; }
    public int MinDeg { get; set; }
    public int MaxSqueezing { get; set; }
    public int MinSqueezing { get; set; }
    public List<DateTime> PrevWash { get; set; }

    public WashAbleDTO()
    {
        ID = string.Empty;
        Name = string.Empty;
        UserId = string.Empty;
        CollectionType = string.Empty;
        NecessityLevel = NecessityLevel.standard;
        PrevWash = new();
    }
    public override bool Equals(object? obj)
    {
        //Check for null and compare run-time types.
        if ((obj == null) || !this.GetType().Equals(obj.GetType()))
        {
            return false;
        }

        WashAbleDTO w = (WashAbleDTO)obj;
        return ID.Equals(w.ID) && Name.Equals(w.Name) && UserId.Equals(w.UserId) && CollectionType.Equals(w.CollectionType)
            && Weight == w.Weight && Category == w.Category && MaxDeg == w.MaxDeg && MaxSqueezing == w.MaxSqueezing
            && MinDeg == w.MinDeg && MinSqueezing == w.MinSqueezing;
    }
    public override int GetHashCode()
    {
        return (int)(ID.GetHashCode() + UserId.GetHashCode() + CollectionType.GetHashCode() + 
            (int)Weight + Category + MaxDeg + MaxSqueezing + MinDeg + MinSqueezing);
    }

}


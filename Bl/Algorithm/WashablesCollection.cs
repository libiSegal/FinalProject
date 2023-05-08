
namespace Bl.Algorithm;

public class WashAblesCollection
{
    public string Type { get; set; }
    public List<WashAbleDTO>[] WashAblesSortedByNecessary { get; set; }
    public double TotalPointsWeight { get; set; }
    public double Weight { get; set; }
    public WashAblesCollection(string type)
    {
        int size = Enum.GetNames(typeof(NecessityLevel)).Length;
        Type = type;
        WashAblesSortedByNecessary = new List<WashAbleDTO>[size];
        WashAblesSortedByNecessary = Enumerable.Range(0, size).Select((i) => new List<WashAbleDTO>()).ToArray();
        Weight = 0;
    }
    public List<WashAbleDTO> GetWashAblesSortedByNecessary() => WashAblesSortedByNecessary.SelectMany(lst => lst.Select(w => w)).ToList();
    public void SetWashAblesSortedByNecessaryFromList(List<WashAbleDTO> washAbles) 
    {
        washAbles.ForEach(w => AddWashableToCollection(w));
    }       
    public void AddWashableToCollection(WashAbleDTO washAbleDTO)
    {
        WashAblesSortedByNecessary[(int)washAbleDTO.NecessityLevel].Add(washAbleDTO);
        Weight += washAbleDTO.Weight;
    }

    public void RemoveWashableFromCollection(WashAbleDTO washAbleDTO)
    {
        WashAblesSortedByNecessary[(int)washAbleDTO.NecessityLevel].Remove(washAbleDTO);
        Weight -= washAbleDTO.Weight;
    }
}


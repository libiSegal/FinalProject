
namespace Bl.Algorithm;

public class WashAblesCollection
{
    public string Type { get; set; }
    public List<WashAbleDTO>[] WashAblesSortedByNecessary { get; set; }
    public double TotalPointsWeight { get; set; }

    public WashAblesCollection(string type)
    {
        int size = Enum.GetNames(typeof(NecessityLevel)).Length;
        Type = type;
        WashAblesSortedByNecessary = new List<WashAbleDTO>[size];
        WashAblesSortedByNecessary = Enumerable.Range(0, size).Select((i) => new List<WashAbleDTO>()).ToArray();
    }
 

    public List<WashAbleDTO> GetWashAblesSortedByNecessary() => WashAblesSortedByNecessary.SelectMany(lst => lst.Select(w => w)).ToList();  

    public void AddWashableToCollection(WashAbleDTO washAbleDTO) => WashAblesSortedByNecessary[(int)washAbleDTO.NecessityLevel].Add(washAbleDTO);

 



}


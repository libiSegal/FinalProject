
namespace Bl.Algorithm;

public class WashAblesCollection
{
    public string Type { get; set; }
    public List<WashAbleDTO>[] WashAblesSortedByNecessary { get; set; }
    public double TotalPointsWeight { get; set; }

    public WashAblesCollection(string type)
    {
        Type = type;
        WashAblesSortedByNecessary = new List<WashAbleDTO>[3] {new List<WashAbleDTO>(), new List<WashAbleDTO>(), new List<WashAbleDTO>()};
    }
 

    public List<WashAbleDTO> GetWashAblesSortedByNecessary() => WashAblesSortedByNecessary.SelectMany(lst => lst.Select(w => w)).ToList();  

    public void AddWashableToCollection(WashAbleDTO washAbleDTO) => WashAblesSortedByNecessary[(int)washAbleDTO.NecessityLevel].Add(washAbleDTO);

/*    {
       int index = (int)washAbleDTO.NecessityLevel;
        if (WashAblesSortedByNecessary[index] == null)
        {
            WashAblesSortedByNecessary[index] = new();
        }
        
    }*/

}


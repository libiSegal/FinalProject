
namespace Bl.Algorithm;

public class WashablesCollection
{

    public string Name { get; set; }
    public string Type { get; set; }

    List<WashAbleDTO>[] WashAblesSortedByNecessary { get; set; } 
    public WashablesCollection(string type)
    {
        Name = "";
        Type = type;
        WashAblesSortedByNecessary = new List<WashAbleDTO>[3];
    }
    public WashablesCollection(string name, string type)
    {
        Name = name;
        Type = type;
        WashAblesSortedByNecessary = new  List<WashAbleDTO>[3];
    }

    public List<WashAbleDTO> GetWashAblesSortedByNecessary() => WashAblesSortedByNecessary.SelectMany(lst => lst.Select(w => w)).ToList();

    public void AddWashableToCollection(WashAbleDTO washAbleDTO)
    {
       int index = (int)washAbleDTO.NecessityLevel;
        if (WashAblesSortedByNecessary[index] == null)
        {
            WashAblesSortedByNecessary[index] = new();
        }
        WashAblesSortedByNecessary[index].Add(washAbleDTO);
    }

}


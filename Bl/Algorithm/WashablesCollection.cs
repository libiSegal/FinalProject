
namespace Bl.Algorithm;

public class WashablesCollection
{
    public string Name { get; set; }
    public string Type { get; set; }
    public LinkedList<WashAbleDTO> WashAbles { get; set; }

    public WashablesCollection(string type)
    {
        Name = "";
        Type = type;
        WashAbles = new();
    }
    public WashablesCollection(string name , string type)
    {
        Name = name;
        Type = type;
        WashAbles = new();
    }
     public void AddWashableToCollection(WashAbleDTO washAbleDTO)
    {

    }

}



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
    public WashablesCollection(string name, string type)
    {
        Name = name;
        Type = type;
        WashAbles = new();
    }
    public void AddWashableToCollection(WashAbleDTO washAbleDTO)
    {
        switch (washAbleDTO.NecessityLevel)
        {
            case NecessityLevel.critical:
                WashAbles.AddFirst(washAbleDTO);
                break;

            case NecessityLevel.standard:
                WashAbles.AddLast(washAbleDTO);
                break;

            case NecessityLevel.necessary:
               
                break;
        }
    }
 /*   public LinkedListNode<WashAbleDTO> AddWashableWithNecessaryLavel(WashAbleDTO washAbleDTO)
    {
        LinkedListNode<WashAbleDTO> node;

        foreach (var washable in WashAbles)
        {
            if (washable.NecessityLevel == NecessityLevel.critical)
            {
                node = new(washable);
                WashAbles.AddAfter(node , washAbleDTO);
                break;

            }
        }
        if(node == null)
        {

        }
        return node;
    }*/

}


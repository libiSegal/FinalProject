
namespace Bl.Algorithm;

public class SchedulerService
{
    public List<WashablesCollection> ListOfWashablesCollections { get; set; }
     
    public SchedulerService()
    {
        ListOfWashablesCollections = new();
        ListOfWashablesCollections[0] = new
    }
    //sort to clean and dirty
    public List<WashAbleDTO> GetDirtyWashables(List<WashAbleDTO> allWashAbles) => allWashAbles.FindAll(w => w.Status == Status.dirty);


    //return a list of all washables 
    public List<WashAbleDTO> AllWashAbles(ManagerDTO manager)
    {
       List<WashAbleDTO> allWashAbles = manager.Items;
       allWashAbles.AddRange(manager.UsersDTO.SelectMany(lst => lst.Items.Select(w => w)).ToList());
       return allWashAbles;
    }

    //sort to machines
    public List<WashablesCollection> SortToCollections(List<WashAbleDTO> dirtyWashabels)
    {


    }





    WashablesCollection washablesCollection = new("dark");

    public List<WashAbleDTO> fill()
    {
        washablesCollection.AddWashableToCollection(new WashAbleDTO(NecessityLevel.necessary));
        washablesCollection.AddWashableToCollection(new WashAbleDTO(NecessityLevel.critical));
        washablesCollection.AddWashableToCollection(new WashAbleDTO(NecessityLevel.standard));
        washablesCollection.AddWashableToCollection(new WashAbleDTO(NecessityLevel.standard));
        washablesCollection.AddWashableToCollection(new WashAbleDTO(NecessityLevel.necessary));
        washablesCollection.AddWashableToCollection(new WashAbleDTO(NecessityLevel.standard));
        washablesCollection.AddWashableToCollection(new WashAbleDTO(NecessityLevel.critical));
        washablesCollection.AddWashableToCollection(new WashAbleDTO(NecessityLevel.necessary));
        return washablesCollection.GetWashAblesSortedByNecessary();
    }



}

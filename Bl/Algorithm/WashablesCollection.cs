
namespace BL.Algorithm;
public class WashAblesCollection
{
    public string Type { get; set; }
    public List<WashAbleDTO>[] WashAblesSortedByNecessary { get; set; }
    public double TotalPointsWeight { get; set; }
    public double Weight { get; set; }
    public WashAblesCollection(string type)
    {
        Type = type;
        int size = Enum.GetNames(typeof(NecessityLevel)).Length;
        WashAblesSortedByNecessary = new List<WashAbleDTO>[size];
        WashAblesSortedByNecessary = Enumerable.Range(0, size).Select((i) => new List<WashAbleDTO>()).ToArray();
        Weight = 0;
    }
    #region Get sorted wash ables list
    public List<WashAbleDTO> GetWashAblesSortedByNecessary() => WashAblesSortedByNecessary.SelectMany(lst => lst.Select(w => w)).ToList();
    #endregion

    #region Set sort wash ables from list
    public void SetWashAblesSortedByNecessaryFromList(List<WashAbleDTO> washAbles) => washAbles.ForEach(w => AddWashableToCollection(w));
    #endregion

    #region Add wash able to collection
    public void AddWashableToCollection(WashAbleDTO washAbleDTO)
    {
        WashAblesSortedByNecessary[(int)washAbleDTO.NecessityLevel].Add(washAbleDTO);
        Weight += washAbleDTO.Weight;
    }
    #endregion

    #region Remove wash able from collection
    public void RemoveWashableFromCollection(WashAbleDTO washAbleDTO)
    {
        WashAblesSortedByNecessary[(int)washAbleDTO.NecessityLevel].Remove(washAbleDTO);
        Weight -= washAbleDTO.Weight;
    }
    #endregion
}
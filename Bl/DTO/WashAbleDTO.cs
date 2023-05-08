﻿
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
        ID = "";
        Name = "";
        UserId = "";
        CollectionType = "";
        NecessityLevel = NecessityLevel.standard;
        PrevWash = new();
    }
   /* public WashAbleDTO(string name, string userId,  Status status, NecessityLevel necessityLevel, Category category, DateTime entrydate, double weight, int maxDeg, int maxSqueezing, string collectionType)
    {
        ID = "";
        Name = name;
        UserId = userId;
        Status = status;
        NecessityLevel = necessityLevel;
        Category = category;
        EnteryDate = entrydate;
        MaxDeg = maxDeg;
        MaxSqueezing = maxSqueezing;
        PrevWash = new();
        CollectionType = collectionType;
        Weight = weight;
        WeightType = string.Empty;
    }*/
    /*public WashAbleDTO(string name, string userId,  Status status, NecessityLevel necessityLevel, Category category, DateTime entryDate, double weight, int maxDeg, int maxSqueezing, int minDeg, int minSqueezing, string collectionType) 
       // :this(name, userId, status,necessityLevel, category,entryDate, weight, maxDeg, maxSqueezing, collectionType)
    {
        ID = "";
        Name = name;
        UserId = userId;
        Status = status;
        NecessityLevel = necessityLevel;
        Category = category;
        EnteryDate = entryDate;
        MaxDeg = maxDeg;
        MaxSqueezing = maxSqueezing;
        MinDeg = minDeg;
        MinSqueezing = minSqueezing;
        PrevWash = new();
        CollectionType = collectionType;
        Weight = weight;
    }
    public WashAbleDTO(string id,string name, string userId,  Status status, NecessityLevel necessityLevel, Category category, DateTime entryDate, double weight, int maxDeg, int maxSqueezing, int minDeg, int minSqueezing, string collectionType) :
        this(name, userId, status, necessityLevel, category,entryDate, weight, maxDeg, maxSqueezing, minDeg, minSqueezing, collectionType)
    {
        ID = id;
    }*/


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
           // && PrevWash.SequenceEqual(w.PrevWash);
    }
    public override int GetHashCode()
    {
        return MinDeg * MaxDeg;
    }

}


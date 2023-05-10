
using MongoDB.Bson.Serialization.Options;

namespace Dal.Models;
public class Calendar 
{
    [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
    public Dictionary<DateTime, Dictionary<string, List<Category>>> WashAbleCalendar { get; set; }
    public Calendar()
    {
        WashAbleCalendar = new();
    }
}



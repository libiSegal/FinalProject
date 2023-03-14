
namespace Dal
{
    public class Laundry : IDataBaseObject//It doesn't have a set because history can't be update;
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID { get; set; }
        public string Name { get; }
        public DateTime Date { get; }
        public int WashingTime { get;  }
        public List<WashAble> WashAbles { get; }
        public Laundry(string name , DateTime dateTime , List<WashAble> washAbles)
        {
            ID = "";
            Name = name;
            Date = dateTime;
            WashAbles = new(washAbles);
        }

    }
}

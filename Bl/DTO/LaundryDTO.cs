using Dal;


namespace BL
{
    public class LaundryDTO : IDataObject
    {

        public string ID { get; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public List<WashAble> WashAbles { get; }//It doesn't have a set because there is no laundry without wash ables;
        public LaundryDTO()
        {
            ID = "";
            Name = "";
            Date = DateTime.Now;
            WashAbles = new();
        }
        public LaundryDTO(string name, DateTime dateTime, List<WashAble> washAbles)
        {
            ID = "";
            Name = name;
            Date = dateTime;
            WashAbles = new(washAbles);
        }
    }
}


namespace Dal
{
    internal class DBConnection : IDBConnection
    {
        private readonly IMongoDatabase _db;
        public string ManagersCollectionName { get; private set; } = "Manager";
        public string UsersCollectionName { get; private set; } = "User";//need to read it from file;
        public string LaundryCollectionName { get; private set; } = "Laundry";
        public string WashAblesCollectionName { get; private set; } = "WashAble";
        public string DataBaseName { get; private set; }
        public MongoClient Client { get; private set; }

        public IMongoCollection<User> UsersCollection { get; private set; }//we will need to add more collection;
        public IMongoCollection<Manager> ManagersCollection { get; private set; }
        public IMongoCollection<Laundry> LaundryCollection { get; private set; }
        public IMongoCollection<WashAble> WashAblesCollection { get; private set; }
        public DBConnection()//we need to read all the details  from json file
        {
            //mongodb+srv://estiZukerman:<password>@finalproject.ildtd7i.mongodb.net/?retryWrites=true&w=majority
            //mongodb://localhost:27017/dbtest?readPreference=primary
            Client = new MongoClient("mongodb://localhost:27017/dbtest?readPreference=primary");
            DataBaseName = "laundrySystem";
            _db = Client.GetDatabase(DataBaseName);
            UsersCollection = _db.GetCollection<User>(UsersCollectionName);
            ManagersCollection = _db.GetCollection<Manager>(ManagersCollectionName);
            LaundryCollection = _db.GetCollection<Laundry>(LaundryCollectionName);
            WashAblesCollection = _db.GetCollection<WashAble>(WashAblesCollectionName);
        }

    }
}

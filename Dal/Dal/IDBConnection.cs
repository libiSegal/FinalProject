
namespace Dal
{
    public interface IDBConnection
    {
        MongoClient Client { get; }
        string DataBaseName { get; }
        IMongoCollection<Laundry> LaundryCollection { get; }
        string LaundryCollectionName { get; }
        IMongoCollection<Manager> ManagersCollection { get; }
        string ManagersCollectionName { get; }
        IMongoCollection<User> UsersCollection { get; }
        string UsersCollectionName { get; }
        IMongoCollection<WashAble> WashAblesCollection { get; }
        string WashAblesCollectionName { get; }
    }
}
namespace Dal.DataApi
{
    public interface IDBConnection
    {
        IMongoCollection<Laundry> LaundryCollection { get; }
        IMongoCollection<Manager> ManagersCollection { get; }
        IMongoCollection<User> UsersCollection { get; }
        IMongoCollection<WashAble> WashAblesCollection { get; }
    }
}
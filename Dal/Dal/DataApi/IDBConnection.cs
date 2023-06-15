
namespace Dal.DataApi;
public interface IDBConnection
{
    IMongoCollection<Manager> ManagersCollection { get; }
    IMongoCollection<User> UsersCollection { get; }
    IMongoCollection<WashAble> WashAblesCollection { get; }
    IMongoCollection<CommonGroupData> CommonGroupDataCollection { get; }
}

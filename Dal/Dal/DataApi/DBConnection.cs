using Microsoft.Extensions.Options;

namespace Dal.DataApi
{
    internal class DBConnection : IDBConnection
    {
        public IMongoCollection<User> UsersCollection { get; private set; }
        public IMongoCollection<Manager> ManagersCollection { get; private set; }
        public IMongoCollection<Laundry> LaundryCollection { get; private set; }
        public IMongoCollection<WashAble> WashAblesCollection { get; private set; }

        public DBConnection(IOptions<LaundrySystemDatabaseSettings> laundrySystemDatabaseSettings)
        {
            var mongoClient = new MongoClient(laundrySystemDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(laundrySystemDatabaseSettings.Value.DatabaseName);

            ManagersCollection = mongoDatabase.GetCollection<Manager>(laundrySystemDatabaseSettings.Value.ManagersCollectionName);

            UsersCollection = mongoDatabase.GetCollection<User>(laundrySystemDatabaseSettings.Value.UsersCollectionName);

            WashAblesCollection = mongoDatabase.GetCollection<WashAble>(laundrySystemDatabaseSettings.Value.WashAbelsCollectionName);

            LaundryCollection = mongoDatabase.GetCollection<Laundry>(laundrySystemDatabaseSettings.Value.LaundryCollectionName);

        }

    }
}

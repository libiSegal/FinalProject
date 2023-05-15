using Microsoft.Extensions.Options;

namespace Dal.DataApi
{
    internal class DBConnection : IDBConnection
    {
        
        public IMongoCollection<User> UsersCollection { get; private set; }
        public IMongoCollection<Manager> ManagersCollection { get; private set; }
        public IMongoCollection<Laundry> LaundryCollection { get; private set; }
        public IMongoCollection<WashAble> WashAblesCollection { get; private set; }

        public DBConnection(LaundrySystemDatabaseSettings laundrySystemDatabaseSettings)
        {
            var mongoClient = new MongoClient(laundrySystemDatabaseSettings.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(laundrySystemDatabaseSettings.DatabaseName);
            ManagersCollection = mongoDatabase.GetCollection<Manager>(laundrySystemDatabaseSettings.ManagersCollectionName);
            UsersCollection = mongoDatabase.GetCollection<User>(laundrySystemDatabaseSettings.UsersCollectionName);
            WashAblesCollection = mongoDatabase.GetCollection<WashAble>(laundrySystemDatabaseSettings.WashAbelsCollectionName);
            LaundryCollection = mongoDatabase.GetCollection<Laundry>(laundrySystemDatabaseSettings.LaundryCollectionName);
        }
    }
}

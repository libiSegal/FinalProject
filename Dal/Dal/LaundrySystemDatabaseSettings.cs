
namespace Dal
{
    public class LaundrySystemDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string UsersCollectionName { get; set; } = null!;

        public string ManagersCollectionName { get; set; } = null!;

        public string WashAbelsCollectionName { get; set; } = null!;

        public string LaundryCollectionName { get; set; } = null!;

    }
}

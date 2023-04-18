namespace Dal.Models
{
    public interface ILaundrySystemDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string LaundryCollectionName { get; set; }
        string ManagersCollectionName { get; set; }
        string UsersCollectionName { get; set; }
        string WashAbelsCollectionName { get; set; }
    }
}
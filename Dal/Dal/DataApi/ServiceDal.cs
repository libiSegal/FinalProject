
namespace Dal.DataApi;
public static class ServiceDal
{
    public static IServiceCollection ExtensionServiceDal(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IDBConnection, DBConnection>();
        services.AddSingleton<IManagerCRUD, ManagerCRUD>();
        services.AddSingleton<IUserCRUD, UserCRUD>();
        services.AddSingleton<IWashAbleCRUD, WashAbleCRUD>();
        services.AddSingleton<ICommonGroupDataCRUD, CommonGroupDataCRUD>();

        var dbSettings = new LaundrySystemDatabaseSettings();
        configuration.GetSection("LaundrySystemDatabase").Bind(dbSettings);
        services.AddSingleton(dbSettings);

        return services;
    }
}

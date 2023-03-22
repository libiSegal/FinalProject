namespace Dal.DataApi;

public static class DIServiceDal
{
    public static IServiceCollection AddTestDal(this IServiceCollection services)
    {
        services.AddSingleton<IDBConnection, DBConnection>();
        services.AddSingleton<IManagerCRUD, ManagerCRUD>();
        services.AddSingleton<IUserCRUD, UserCRUD>();
        services.AddSingleton<ILaundryCRUD, LaundryCRUD>();
        services.AddSingleton<IWashAbleCRUD, WashAbleCRUD>();
        return services;

    }
}

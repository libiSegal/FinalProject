
namespace BL.DataApi;
public static class ServiceBl
{
    public static IServiceCollection ExtensionServiceBl(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IUserService, UserService>();
        services.AddSingleton<IManagerService, ManagerService>();
        services.AddSingleton<ICommonGroupDataService, CommonGroupDataService>();
        services.AddSingleton<IWashAbleService, WashAbleService>();
        services.AddSingleton<ICalendarService, CalendarService>();
        services.AddSingleton<ISchedulerService, SchedulerService>();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.ExtensionServiceDal(configuration);
        return services;
    }
}


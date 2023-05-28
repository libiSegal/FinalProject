
namespace BL.DataApi;
public static class DIServiceBl
{
    public static IServiceCollection AddTestBl(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IUserService, UserService>();
        services.AddSingleton<IManagerService, ManagerService>();
        //   services.AddSingleton<ILaundryService, LaundryService>();
        services.AddSingleton<ICommonGroupDataService, CommonGroupDataService>();
        services.AddSingleton<IWashAbleService, WashAbleService>();
        services.AddSingleton<ICalendarService, CalendarService>();
        services.AddSingleton<ISchedulerService, SchedulerService>();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddTestDal(configuration);
        return services;
    }
}


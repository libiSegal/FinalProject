using BL.DataImplementation.ServiceInterfaces;
using BL.DataImplementation.ServiceClasses;
using MongoDB.Driver.Core.Connections;
using Microsoft.Extensions.Configuration;
using Bl.Algorithm;
using Bl.DataImplementation.ServiceClasses;

namespace Bl.DataApi;

public static class DIServiceBl
{
    public static IServiceCollection AddTestBl(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddSingleton<IUserService, UserService>();
        services.AddSingleton<IManagerService, ManagerService>();
        services.AddSingleton<ILaundryService, LaundryService>();
        services.AddSingleton<IWashAbleService, WashAbleService>();
        services.AddSingleton<ICalendarService, CalendarService>();
        services.AddSingleton<ISchedulerService, SchedulerService>();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddTestDal(configuration);
        return services;
    }
}


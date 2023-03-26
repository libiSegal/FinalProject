using BL.DataImplementation.ServiceInterfaces;
using BL.DataImplementation.ServiceClasses;
using MongoDB.Driver.Core.Connections;
using Microsoft.Extensions.Configuration;

namespace Bl.DataApi;

public static class DIServiceBl
{
    public static IServiceCollection AddTestBl(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IUserService, UserService>();
        services.AddSingleton<IManagerService, ManagerService>();
        services.AddSingleton<ILaundryService, LaundryService>();
        services.AddSingleton<IWashAbleService, WashAbleService>();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddTestDal(configuration);
        return services;

    }
}


﻿using Bl;
using Dal.DataApi;
using Microsoft.Extensions.DependencyInjection;


namespace BL;

public static class DIServiceBl
{
    public static IServiceCollection AddTestBl(this IServiceCollection services)
    {
        services.AddSingleton<IUserService, UserService>();
        services.AddSingleton<IManagerService, ManagerService>();
        services.AddSingleton<ILaundryService, LaundryService>();
        services.AddSingleton<IWashAbleService, WashAbleService>();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddTestDal();
        return services;

    }
}


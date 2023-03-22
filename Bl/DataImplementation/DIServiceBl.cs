using AutoMapper;
using Bl;

using Dal;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public static class DIServiceBl
    {
        public static IServiceCollection AddTestBl(this IServiceCollection services)
        {
            services.AddSingleton<IUserService ,UserService>();
            services.AddSingleton<IManagerService, ManagerService>();
            services.AddSingleton<ILaundryService, LaundryService>();
            services.AddSingleton<IWashAbleService , WashAbleService>();

           services.AddSingleton(provider => new MapperConfiguration(
                   cfg =>
                   {
                       cfg.AddProfile(new UserProfile(provider.GetService<IWashAbleService>()));
                       cfg.AddProfile<ManagerProfile>();
                       cfg.AddProfile<ManagerDTOProfile>();
                   }).CreateMapper());
            services.AddTestDal();  
            return services;

        }
    }
}

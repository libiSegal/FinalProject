﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public static class DIServiceBl
    {
        public static void AddTestBl(this IServiceCollection services)
        {
            services.AddSingleton<IUserService ,UserService>();
            services.AddSingleton<IManagerService, ManagerService>();
            services.AddSingleton<ILaundryService, LaundryService>();
            services.AddSingleton<IWashAbleService , WashAbleService>();
    
        }
    }
}
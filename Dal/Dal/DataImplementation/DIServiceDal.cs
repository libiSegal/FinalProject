
using Microsoft.Extensions.DependencyInjection;

namespace Dal
{
    public static class DIServiceDal
    {
        public static void AddTestDal(this IServiceCollection services)
        {
            services.AddSingleton<IDBConnection, DBConnection>();
            services.AddSingleton<IManagerCRUD, ManagerCRUD>();
            services.AddSingleton<IUserCRUD, UserCRUD>();
            services.AddSingleton<ILaundryCRUD, LaundryCRUD>();
            services.AddSingleton<IWashAbleCRUD, WashAbleCRUD>();

        }
    }
}

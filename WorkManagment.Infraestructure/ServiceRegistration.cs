using Microsoft.Extensions.DependencyInjection;
using WorkManagment.Core.Interfaces;
using WorkManagment.Infraestructure.Data;
using WorkManagment.Infraestructure.Repositories;

namespace WorkManagment.Infraestructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
           

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddSingleton<DbContext>();
        }
    }
}

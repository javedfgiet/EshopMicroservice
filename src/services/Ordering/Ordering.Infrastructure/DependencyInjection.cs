using Microsoft.EntityFrameworkCore.Diagnostics;
using Ordering.Infrastructure.Data.Interceptors;

namespace Ordering.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
            IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DataBase");

            services.AddScoped<ISaveChangesInterceptor,AuditableEntityInterceptor>();
            services.AddScoped<ISaveChangesInterceptor,DispatchDomainEventsInterceptor>();

            services.AddDbContext<ApplicationDbContext>((sp,option )=>
            {
                option.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                option.UseSqlServer(connectionString);
            });


            return services;
        }
    }
}
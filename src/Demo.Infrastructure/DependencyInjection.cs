using Demo.Application.Common.Interfaces;
using Demo.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks().AddDbContextCheck<DemoDbContext>();
            
            services.AddDbContext<DemoDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DemoDb")));

            services.AddScoped<IDemoDbContext>(provider => provider.GetService<DemoDbContext>());

            return services;
        }
    }
}
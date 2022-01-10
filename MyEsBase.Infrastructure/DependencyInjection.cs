using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyEsBase.Application.Common.Interfaces;
using MyEsBase.Infrastructure.Persistence;

namespace MyEsBase.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IEventStore, EventStore.EventStore>();

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseMySql(
                configuration.GetConnectionString("DefaultConnection"),
                new MySqlServerVersion(new Version(8, 0, 27)),
                b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName));
        });
    }
}
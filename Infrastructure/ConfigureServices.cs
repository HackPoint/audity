using Application.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ConfigureServices {
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration) {
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
      
        //todo: add UseInMemoryDatabase
        services.AddDbContext<ApplicationDbContext>(opts =>
            opts.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        return services;
    }
}
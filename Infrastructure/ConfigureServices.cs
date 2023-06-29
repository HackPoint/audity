using System.Text;
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

        // string connectionCommand = "Pooling=false;Timeout=300;CommandTimeout=300;";
        string? connectionString = string.Empty;
        try {
            string db = EnvironmentHelper.GetValue<string>("POSTGRES_DB");
            string port = EnvironmentHelper.GetValue<string>("POSTGRES_PORT");
            string usr = EnvironmentHelper.GetValue<string>("POSTGRES_USER");
            string pwd = EnvironmentHelper.GetValue<string>("POSTGRES_PASSWORD");
            string host = EnvironmentHelper.GetValue<string>("POSTGRES_HOST");
            string searchPath = EnvironmentHelper.GetValue<string>("POSTGRES_SEARCH_PATH");

            Console.WriteLine("Environment: {0}", EnvironmentHelper.GetValue<string>("ASPNETCORE_ENVIRONMENT"));
            Console.WriteLine("DB: {0}", db);
            Console.WriteLine("USER: {0}", usr);
            Console.WriteLine("PASSWORD: {0}", pwd);
            Console.WriteLine("HOST: {0}", host);
            Console.WriteLine("SEARCH_PATH: {0}", searchPath);

            ArgumentException.ThrowIfNullOrEmpty(db);
            
            connectionString =
                $"Server={host};Port={port};Database={db};User Id={usr};Password={pwd};SearchPath={searchPath};";
            services.AddDbContext<ApplicationDbContext>(opts =>
                opts.UseNpgsql(connectionString));
        }
        catch (Exception e) {
            Console.WriteLine($"DB Configuration error: {e.Message}");
            Console.WriteLine($"Configuring DefaultConnection");
            connectionString = 
                configuration["ConnectionStrings:localhost"];
            services.AddDbContext<ApplicationDbContext>(opts =>
                opts.UseNpgsql($"{connectionString};"));
        }
        Console.WriteLine($"Connection String: {connectionString}");
        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        return services;
    }
}

public class EnvironmentHelper {
    public static T GetValue<T>(string key) {
        ArgumentException.ThrowIfNullOrEmpty(key);

        string? value = Environment.GetEnvironmentVariable(key);

        try {
            return (T)Convert.ChangeType(value, typeof(T));
        }
        catch (Exception ex) {
            Console.WriteLine($"Error converting environment variable: {ex.Message}");
            throw new InvalidCastException($"Unable to convert env var: {nameof(value)}");
        }
    }
}
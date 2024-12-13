using Domain.Interface;
using Domain.Repository;
using Infrastructure.Persistence;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // var
        var uploaderApi = configuration["AppSettings:UploaderApi"] ?? throw new Exception("UploaderApi not found");
        var uploaderToken = configuration["AppSettings:UploaderToken"] ?? throw new Exception("UploaderToken not found");

        // Context
        var cs = configuration.GetConnectionString("Default");
        services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(cs));

         using (var serviceProvider = services.BuildServiceProvider())
        {
            try
            {
                var dbContext = serviceProvider.GetRequiredService<AppDbContext>();
                dbContext.Database.OpenConnection(); // Abre la conexión
                dbContext.Database.CloseConnection(); // Cierra la conexión
                Console.WriteLine("Conexión a la base de datos inicializada correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al conectar a la base de datos: {ex.Message}");
            }
        }

        // Repositories
        // services.AddScoped<typeof(IAsyncRepository<>), typeof(AsyncRepository<>)>();
        services.AddScoped(typeof(IAsyncRepository<>), typeof(AsyncRepository<>));
        
        // singleton
        services.AddSingleton<IUploaderRepository>(new UploaderRepositoryImpl(
            uploaderToken,
            uploaderApi
        ));

        return services;
    }
}


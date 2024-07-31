using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineShop.Application.Common.Interfaces;
using OnlineShop.Infrastructure.Persistance.MongoDb;

namespace OnlineShop.Infrastructure;

/// <summary>
/// Configures services for the infrastructure layer, including database and repository setup.
/// </summary>
public static class ConfigureServices
{
	/// <summary>
	/// Adds infrastructure layer services to the service collection.
	/// </summary>
	/// <param name="services">The service collection to add services to.</param>
	/// <param name="configuration">The configuration for setting up services.</param>
	/// <returns>The updated service collection.</returns>
	public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        MongoDbOptions mongoDbOptions = new MongoDbOptions();
        configuration.GetSection("MongoDb").Bind(mongoDbOptions);
        services.AddSingleton(mongoDbOptions);

        services.AddSingleton<MongoDbContext>();
        services.AddScoped(typeof(IReadRepository<>), typeof(MongoDbReadRepository<>));
        services.AddScoped(typeof(IRepository<>), typeof(MongoDbRepository<>));
		services.AddScoped<IShoppingCartRepository, MongoDbShoppingCartRepository>();

		return services;
    }
}

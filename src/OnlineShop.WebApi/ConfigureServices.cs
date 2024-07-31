namespace OnlineShop.WebApi;

/// <summary>
/// Configures services for the Web API layer, including controllers and middleware.
/// </summary>
public static class ConfigureServices
{
    public const string CorsPolicy = "CorsPolicy";

	/// <summary>
	/// Adds Web API layer services to the service collection.
	/// </summary>
	/// <param name="services">The service collection to add services to.</param>
	/// <returns>The updated service collection.</returns>
	public static IServiceCollection AddWebApiServices(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.CustomSchemaIds(x => x.FullName);
        });

        services.AddCors(o => o.AddPolicy(CorsPolicy,
            builder =>
            {
                builder.WithOrigins("http://localhost:5000", "http://localhost:44464")
                .AllowAnyMethod()
                .AllowAnyHeader();
            }));

        return services;
    }
}


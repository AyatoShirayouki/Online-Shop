using System.Reflection;
using MediatR;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using OnlineShop.Application.Services;

namespace OnlineShop.Application;

/// <summary>
/// Configures services for the application layer, including MediatR, AutoMapper, and validators.
/// </summary>
public static class ConfigureServices
{
	/// <summary>
	/// Adds application layer services to the service collection.
	/// </summary>
	/// <param name="services">The service collection to add services to.</param>
	/// <returns>The updated service collection.</returns>
	public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        Assembly executingAssembly = Assembly.GetExecutingAssembly();

        services.AddMediatR(executingAssembly);
        services.AddAutoMapper(executingAssembly);
		services.AddValidatorsFromAssembly(executingAssembly);
		services.AddScoped<DiscountCalculationService>();

		return services;
    }
}

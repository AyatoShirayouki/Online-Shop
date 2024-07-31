using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using OnlineShop.Application.Common.Interfaces;
using OnlineShop.Domain.Entities;

namespace OnlineShop.Infrastructure.Persistance.MongoDb.Seed;

public static class DbInitializerExtension
{
    public static IApplicationBuilder UseMongoDbInitializer(this IApplicationBuilder app)
    {
        IServiceScope scope = app.ApplicationServices.CreateScope();

        IRepository<Product> productRepository = scope.ServiceProvider.GetService<IRepository<Product>>();

        MongoDbInitializer.Initialize(productRepository).GetAwaiter().GetResult();
       
        return app;
    }
}

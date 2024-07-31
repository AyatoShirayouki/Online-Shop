using System;
using System.Reflection;
using System.Text.Json;
using OnlineShop.Application.Common.Interfaces;
using OnlineShop.Domain.Entities;

namespace OnlineShop.Infrastructure.Persistance.MongoDb.Seed;

public class MongoDbInitializer
{
    public static async Task Initialize(IRepository<Product> productRepository)
    {
        if (await productRepository.Count() > 0)
        {
            return;
        }

        string seedJson = await ReadSeedFile("Products.json");
        Product[] products = JsonSerializer.Deserialize<Product[]>(seedJson);

        foreach (Product product in products)
        {
            await productRepository.Add(product);
        }
    }

	private static async Task<string> ReadSeedFile(string seedFileName)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        string embededResourcePath = $"OnlineShop.Infrastructure.Persistance.MongoDb.Seed.{seedFileName}";
        Stream? seedFileStream = assembly.GetManifestResourceStream(embededResourcePath);

        if (seedFileStream is null)
        {
            throw new Exception($"Embeded resource {embededResourcePath} not found.");
        }

        using (StreamReader reader = new(seedFileStream))
        {
            return await reader.ReadToEndAsync();
        }
    }
}

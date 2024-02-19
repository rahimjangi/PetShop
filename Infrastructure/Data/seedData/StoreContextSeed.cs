using Core.Entities;
using System.Text.Json;

namespace Infrastructure.Data.seedData;

public class StoreContextSeed
{
    public static async Task SeadAsync(StoreContext context)
    {
        if (!context.ProductBrands.Any())
        {
            var brandData = File.ReadAllText("../Infrastructure\\Data\\seedData\\brands.json");
            var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
            context.ProductBrands.AddRange(brands);

        }
        if (!context.ProductTypes.Any())
        {
            var typesData = File.ReadAllText("../Infrastructure\\Data\\seedData\\types.json");
            var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
            context.ProductTypes.AddRange(types);

        }
        if (!context.Products.Any())
        {
            var productsData = File.ReadAllText("../Infrastructure\\Data\\seedData\\products.json");
            var products = JsonSerializer.Deserialize<List<Product>>(productsData);
            context.Products.AddRange(products);

        }
        if (context.ChangeTracker.HasChanges()) { await context.SaveChangesAsync(); }
    }
}

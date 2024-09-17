using Marten.Schema;

namespace Catalog.API.Data;

public class CatalogInitialData: IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        await using var session = store.LightweightSession();
        if (await session.Query<Product>().AnyAsync(cancellation))
            return;
        
        session.Store(new Product
        {
            Id = Guid.NewGuid(),
            Name = "Product 1",
            Description = "Description 1",
            Price = 100,
            Category = ["Category 1"],
            ImageFile = "Image 1"
        });

        await session.SaveChangesAsync(cancellation);
    }
}
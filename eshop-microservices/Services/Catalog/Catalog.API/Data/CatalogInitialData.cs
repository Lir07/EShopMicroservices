using Marten.Schema;

namespace Catalog.API.Data
{
    public class CatalogInitialData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            using var session = store.LightweightSession();

            if (await session.Query<Product>().AnyAsync())
                return;

            session.Store<Product>(GetPreconfiguredProducts());
            await session.SaveChangesAsync();
        }

        private static IEnumerable<Product> GetPreconfiguredProducts() => new List<Product>
        {
            new Product()
            {
                Id = new Guid(),
                Name = "Iphone X",
                Description = "This phone is the company's biggest change",
                ImageFile = "product-1.png",
                Price = 950.00M,
                Category = new List<string>{"Smart Phone"}
            },
            new Product()
            {
                Id = new Guid(),
                Name = "Iphone 7",
                Description = "This phone is the company's biggest change",
                ImageFile = "product-1.png",
                Price = 950.00M,
                Category = new List<string>{"Smart Phone"}
            },
            new Product()
            {
                Id = new Guid(),
                Name = "Iphone 6",
                Description = "This phone is the company's biggest change",
                ImageFile = "product-1.png",
                Price = 950.00M,
                Category = new List<string>{"Smart Phone"}
            },
            new Product()
            {
                Id = new Guid(),
                Name = "Iphone X",
                Description = "This phone is the company's biggest chanfe",
                ImageFile = "product-1.png",
                Price = 950.00M,
                Category = new List<string>{"Smart Phone"}
            },
        };
    }
}

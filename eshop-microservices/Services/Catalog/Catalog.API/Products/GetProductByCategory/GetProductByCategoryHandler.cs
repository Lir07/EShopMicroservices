namespace Catalog.API.Products.GetProductById;

public record GetProductsByCategoryQuery(string category) : IQuery<GetProductsByCategoryResult>;
public record GetProductsByCategoryResult(IEnumerable<Product> Products);

internal class GetProductsByCategoryQueryHandler
    (IDocumentSession documentSession, ILogger<GetProductsByCategoryQueryHandler> logger) : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
{
    public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryQuery query, CancellationToken cancellationToken)
    {
        var products = await documentSession.Query<Product>()
            .Where(p => p.Category.Contains(query.category))
            .ToListAsync();

        return new GetProductsByCategoryResult(products);
    }
}

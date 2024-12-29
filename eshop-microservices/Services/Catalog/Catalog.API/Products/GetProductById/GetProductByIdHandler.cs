namespace Catalog.API.Products.GetProductById;

public record GetProductByIdQuery(Guid id) : IQuery<GetProductByIdResult>;
public record GetProductByIdResult(Product Product);

internal class GetProductByIdQueryHandler
    (IDocumentSession documentSession) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await documentSession.LoadAsync<Product>(query.id, cancellationToken);

        if (product is null)
        {
            throw new ProductNotFoundException(query.id);
        }

        return new GetProductByIdResult(product);

    }
}

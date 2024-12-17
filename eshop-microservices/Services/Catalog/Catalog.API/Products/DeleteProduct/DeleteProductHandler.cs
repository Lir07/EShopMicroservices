namespace Catalog.API.Products.UpdateProduct;

public record DeleteProductCommand(Guid id)
    : ICommand<DeleteProductResult>;

public record DeleteProductResult(bool IsSuccess);

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(x => x.id).NotEmpty().WithMessage("Product Id is required");
    }
}
internal class DeleteProductHandler
    (IDocumentSession session, ILogger<DeleteProductHandler> logger)
    : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteProductHandler.Handle called with {@Command}", command);

        var product = await session.LoadAsync<Product>(command.id, cancellationToken);

        if (product is null)
        {
            throw new ProductNotFoundException();
        }

        session.Delete<Product>(product);
        await session.SaveChangesAsync(cancellationToken);

        return new DeleteProductResult(true);
    }
}

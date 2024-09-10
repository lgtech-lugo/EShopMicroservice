namespace Catalog.API.Products.DeleteProduct;

internal record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;
internal record DeleteProductResult(bool IsSuccess);

internal class DeleteProductCommandHandler(IDocumentSession session): ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        session.Delete<Product>(command.Id);
        await session.SaveChangesAsync(cancellationToken);
        return new DeleteProductResult(true);
    }
}
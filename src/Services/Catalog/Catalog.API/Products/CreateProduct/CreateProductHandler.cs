using BuildingBlocks.CQRS;

namespace Catalog.API.Products.CreateProduct;

internal record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price) : ICommand<CreateProductResult>;
internal record CreateProductResult(Guid Id);

internal class CreateProductCommandHandler: ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
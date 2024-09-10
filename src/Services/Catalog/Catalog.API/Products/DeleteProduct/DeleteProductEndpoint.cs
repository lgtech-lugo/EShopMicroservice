namespace Catalog.API.Products.DeleteProduct;

internal record DeleteProductResponse(bool IsSuccess);

public class DeleteProductEndpoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("products/{id}", async (Guid id, ISender sender) =>
        {
            var command = new DeleteProductCommand(id);
            var result = await sender.Send(command);
            var response = result.Adapt<DeleteProductResponse>();
            return Results.Ok(response);
        }).Produces<DeleteProductResponse>()
            .Produces(StatusCodes.Status404NotFound)
            .WithSummary("Delete Product")
            .WithDescription("Delete Product");
    }
}
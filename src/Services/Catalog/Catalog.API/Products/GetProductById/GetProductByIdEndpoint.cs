using Catalog.API.Products.GetProducts;

namespace Catalog.API.Products.GetProductById;

internal record GetProductByIdResponse(Guid Id);
public class GetProductByIdEndpoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetProductByIdQuery(id));
            var response = result.Adapt<GetProductResponse>();
            return Results.Ok(response);
        }).WithName("GetProductById")
            .Produces<GetProductResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithSummary("Get Product By Id")
            .WithDescription("Get Product By Id");
    }
}
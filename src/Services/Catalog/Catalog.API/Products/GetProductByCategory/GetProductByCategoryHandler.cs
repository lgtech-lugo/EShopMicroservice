namespace Catalog.API.Products.GetProductByCategory;

internal record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;
internal record GetProductByCategoryResult(IEnumerable<Product> Products);

internal class GetProductByCategoryHandler(IDocumentSession session, ILogger<GetProductByCategoryHandler> logger): IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductByCategoryHandler");
        var products = await session.Query<Product>().Where(p => p.Category.Contains(query.Category))
            .ToListAsync(cancellationToken);
        return new GetProductByCategoryResult(products);
    }
}
namespace Catalog.API.Products.GetProductsById;

public record GetProductByIdQuery(Guid id) : IQuery<GetProductByIDResult>;

public record GetProductByIDResult (Product Product);
internal class GetProductsByIdHandler(IDocumentSession session, ILogger<GetProductsByIdHandler> logger) : IQueryHandler<GetProductByIdQuery, GetProductByIDResult>
{
    public async Task<GetProductByIDResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductByIdQueryHandleer. Handle called with {@Query}", query);
        var product = await session.LoadAsync<Product>(query.id, cancellationToken);
        if( product is null)
        {
            throw new ProductNotFoundException();
        }
        return new GetProductByIDResult(product);
    }
}

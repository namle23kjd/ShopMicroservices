
namespace Catalog.API.Products.GetProduct;

//publiv record GetProductsRequest();

public record GetProductReponse(IEnumerable<Product> Products);

public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (ISender sender) =>
        {
            var result = await sender.Send(new GetProductsQuery());
            var reponse = result.Adapt<GetProductReponse>();
            return Results.Ok(reponse);
        })
        .WithName("GetProducts")
        .Produces<GetProductReponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Products")
        .WithDescription("Get Products");
    }
}

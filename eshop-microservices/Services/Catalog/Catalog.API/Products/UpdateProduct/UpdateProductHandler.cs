
namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductCommand(Guid id, string Name, List<string> Category, string Description, string ImageFile, decimal Price)
    : ICommand<UpdateProductResult>;

public record UpdateProductResult(bool IsSuccess);

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(command => command.id).NotEmpty().WithMessage("Product Id is required");
        RuleFor(command => command.Name)
            .NotEmpty().WithMessage("Name is required")
            .Length(2, 150).WithMessage("Name must be between 2 and 150 characters");

        RuleFor(command => command.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0");
    }
}
internal class UpdateProductCommandHandler
    (IDocumentSession session)
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        //var product = await session.LoadAsync<Product>(cancellationToken);
        var productId = command.id; // Giả sử bạn có `ProductId` là Guid trong command
        var product = await session.LoadAsync<Product>(productId, cancellationToken);
        if (product is null)
            throw new ProductNotFoundException(productId);
        product.Name = command.Name;
        product.Category = command.Category;
        product.Description = command.Description;
        product.ImageFile = command.ImageFile;
        product.Price = command.Price;
        session.Update(product);
        await session.SaveChangesAsync(cancellationToken);
        return new UpdateProductResult(true);
    }
}

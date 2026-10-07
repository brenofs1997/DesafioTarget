using DesafioTarget.Models;
using DesafioTarget.Repositories;

namespace DesafioTarget.Services;

public class StockService : IStockService
{
    private readonly IProductRepository _productRepository;

    public StockService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Product> ProcessMovementAsync(
        StockMovement movement,
        CancellationToken ct = default)
    {
        var product = await _productRepository.GetByCodeAsync(
            movement.ProductCode,
            ct);

        if (product is null)
        {
            throw new InvalidOperationException("Produto não encontrado.");
        }

        if (movement.Type == StockMovementType.Entry)
        {
            product.Stock += movement.Quantity;
        }
        else if (movement.Type == StockMovementType.Exit)
        {
            product.Stock -= movement.Quantity;
        }

        await _productRepository.UpdateAsync(product, ct);

        return product;
    }
}
using DesafioTarget.Common;
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
        if (movement.Quantity <= 0)
            throw new DomainException("A quantidade deve ser maior que zero.");

        var product = await _productRepository.GetByCodeAsync(
            movement.ProductCode,
            ct);

        if (product is null)
        {
            throw new NotFoundException("Produto não encontrado.");
        }

        product.Stock += movement.Type switch
        {
            StockMovementType.Entry => movement.Quantity,
            StockMovementType.Exit => -movement.Quantity,
            _ => throw new DomainException("Tipo de movimentação inválido.")
        };

        if (product.Stock < 0)
            throw new DomainException("Estoque insuficiente.");

        await _productRepository.UpdateAsync(product, ct);

        return product;
    }
}
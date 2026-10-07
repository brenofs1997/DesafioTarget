using DesafioTarget.Models;

namespace DesafioTarget.Services
{
    public interface IStockService
    {
        Task<Product> ProcessMovementAsync( StockMovement movement, CancellationToken ct);
    }
}

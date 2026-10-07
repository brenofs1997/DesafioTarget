using DesafioTarget.Models;

namespace DesafioTarget.Repositories
{
    public interface IProductRepository
    {
        Task<IReadOnlyList<Product>> GetAllAsync( CancellationToken ct = default);

        Task<Product?> GetByCodeAsync(int productCode,CancellationToken ct = default);

        Task UpdateAsync(Product product,CancellationToken ct = default);
    }
}

using DesafioTarget.Models;

namespace DesafioTarget.Repositories
{
    public interface ISaleRepository
    {
        Task<IReadOnlyList<Sale>> GetAllAsync(CancellationToken ct);
    }
}

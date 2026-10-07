using DesafioTarget.DTOs;
using DesafioTarget.Models;
using DesafioTarget.Models.Dtos;

namespace DesafioTarget.Services
{
    public interface ICommissionService
    {
        IEnumerable<CommissionResult> CalculateCommission(SaleRequest sales);
        Task<IReadOnlyList<CommissionResult>> CalculateCommissionAsync(CancellationToken ct);
    }
}
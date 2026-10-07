using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DesafioTarget.DTOs;
using DesafioTarget.Models;
using DesafioTarget.Models.Dtos;
using DesafioTarget.Repositories;
using DesafioTarget.Services.Strategies;

namespace DesafioTarget.Services
{
    public class CommissionService : ICommissionService
    {
        private readonly IEnumerable<ICommissionStrategy> _strategies;
        private readonly ISaleRepository _repository;

        public CommissionService(IEnumerable<ICommissionStrategy> strategies, ISaleRepository repository)
        {
            _strategies = strategies;
            _repository = repository;
        }

        public IEnumerable<CommissionResult> CalculateCommission(SaleRequest sales)
        {
            var results = new List<CommissionResult>();

            foreach (var item in sales.Vendas)
            {
                var result = CreateResultFromSaleItem(item);
                results.Add(result);
            }

            return results;
        }

        public decimal CalculateCommission(decimal saleAmount)
        {
            return CalculateCommissionForAmount(saleAmount);
        }

        public async Task<IReadOnlyList<CommissionResult>> CalculateCommissionAsync(CancellationToken ct)
        {
            var sales = await _repository.GetAllAsync(ct) ?? Array.Empty<Sale>();

            var results = sales.Select(CreateResultFromSale).ToList();

            return results;
        }

        private ICommissionStrategy? GetStrategyForAmount(decimal amount)
        {
            return _strategies.FirstOrDefault(s => s.CanApply(amount));
        }

        private decimal CalculateCommissionForAmount(decimal amount)
        {
            var strategy = GetStrategyForAmount(amount);
            return strategy is null ? 0m : strategy.Calculate(amount);
        }

        private CommissionResult CreateResultFromSale(Sale sale)
        {
            return new CommissionResult
            {
                Seller = sale.Seller,
                SaleAmount = sale.Amount,
                Commission = CalculateCommissionForAmount(sale.Amount)
            };
        }

        private CommissionResult CreateResultFromSaleItem(SaleItemDto item)
        {
            return new CommissionResult
            {
                Seller = item.Vendedor,
                SaleAmount = item.Valor,
                Commission = CalculateCommissionForAmount(item.Valor)
            };
        }
    }
}

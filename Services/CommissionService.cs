using DesafioTarget.Models;
using DesafioTarget.Services.Strategy;

namespace DesafioTarget.Services
{
    public class CommissionService
    {
        private readonly IEnumerable<ICommissionStrategy> _strategies;

        public CommissionService(IEnumerable<ICommissionStrategy> strategies)
        {
            _strategies = strategies;
        }

        public decimal CalculateCommission(Sale sale)
        {
            var strategy = _strategies.FirstOrDefault(
                strategy => strategy.AppliesTo(sale));

            if (strategy is null)
            {
                throw new InvalidOperationException("Nenhuma regra de comissão foi encontrada para a venda.");
            }

            return strategy.Calculate(sale);
        }
    }
}

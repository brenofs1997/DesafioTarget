using DesafioTarget.Models;
using DesafioTarget.Services.Strategies;

namespace DesafioTarget.Services
{
    public class CommissionService
    {
        private readonly IEnumerable<ICommissionStrategy> _strategies;

        public CommissionService(IEnumerable<ICommissionStrategy> strategies)
        {
            _strategies = strategies;
        }

        public decimal CalculateCommission(decimal saleAmount)
        {
            var strategy = _strategies.FirstOrDefault(
                            strategy => strategy.CanApply(saleAmount));

            if (strategy is null)
            {
                throw new InvalidOperationException("Nenhuma regra de comissão foi encontrada para a venda.");
            }

            return strategy.Calculate(saleAmount);
        }
    }
}

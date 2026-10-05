using DesafioTarget.Models;

namespace DesafioTarget.Services.Strategy
{
    public class HighCommissionStrategy : ICommissionStrategy
    {
        public bool AppliesTo(Sale sale)
        {
            return sale.Amount >= 500;
        }

        public decimal Calculate(Sale sale)
        {
            return sale.Amount * 0.05m;
        }
    }
}

using DesafioTarget.Models;

namespace DesafioTarget.Services.Strategy
{
    public class LowCommissionStrategy : ICommissionStrategy
    {

        public bool AppliesTo(Sale sale)
        {
            return sale.Amount >= 100 && sale.Amount < 500;
        }

        public decimal Calculate(Sale sale)
        {
            return sale.Amount * 0.01m;
        }
        
    }
}

using DesafioTarget.Models;

namespace DesafioTarget.Services.Strategies
{
    public class FivePercentCommissionStrategy : ICommissionStrategy
    {
        public bool CanApply(decimal saleAmount)
        {
            return saleAmount >= 500m;
        }

        public decimal Calculate(decimal saleAmount)
        {
            return saleAmount * 0.05m;
        }
    }
}

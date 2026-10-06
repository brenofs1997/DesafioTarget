using DesafioTarget.Models;

namespace DesafioTarget.Services.Strategies
{
    public class OnePercentCommissionStrategy : ICommissionStrategy
    {

        public bool CanApply(decimal saleAmount)
        {
            return saleAmount >= 100m && saleAmount < 500m;
        }

        public decimal Calculate(decimal saleAmount)
        {
            return saleAmount * 0.01m;
        }
        
    }
}

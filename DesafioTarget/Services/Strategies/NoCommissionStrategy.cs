namespace DesafioTarget.Services.Strategies
{

    using DesafioTarget.Models;

    public class NoCommissionStrategy : ICommissionStrategy
    {
        public bool CanApply(decimal saleAmount)
        {
            return saleAmount < 100m;
        }

        public decimal Calculate(decimal saleAmount)
        {
            return 0m;
        }
    }
}

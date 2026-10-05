namespace DesafioTarget.Services.Strategy
{

    using DesafioTarget.Models;

    public class NoCommissionStrategy : ICommissionStrategy
    {
        public bool AppliesTo(Sale sale)
        {
            return sale.Amount < 100;
        }

        public decimal Calculate(Sale sale)
        {
            return 0m;
        }
    }
}

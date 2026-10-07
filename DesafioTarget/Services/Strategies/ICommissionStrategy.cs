using DesafioTarget.Models;

namespace DesafioTarget.Services.Strategies
{
    public interface ICommissionStrategy
    {
        bool CanApply(decimal saleAmount);

        decimal Calculate(decimal saleAmount);
    }
}

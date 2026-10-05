using DesafioTarget.Models;

namespace DesafioTarget.Services.Strategy
{
    public interface ICommissionStrategy
    {
        bool AppliesTo(Sale sale);

        decimal Calculate(Sale sale);
    }
}

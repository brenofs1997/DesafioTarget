using DesafioTarget.Models.Dtos;

namespace DesafioTarget.Services
{
    public interface IInterestService
    {
        InterestResult CalculateInterest(decimal value, DateOnly dueDate);
    }
}

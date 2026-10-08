using DesafioTarget.Common;
using DesafioTarget.Models.Dtos;

namespace DesafioTarget.Services
{
    public class InterestService : IInterestService
    {

        public InterestResult CalculateInterest(decimal value, DateOnly dueDate)
        {
            if (value <= 0)
                throw new DomainException("A quantidade deve ser maior que zero.");

            int daysOverdue = CalculateDueDays(dueDate);
            decimal interestAmount = CalculateInterestAmount(value, daysOverdue);

            return new InterestResult(value, interestAmount, daysOverdue);
        }

        public int CalculateDueDays(DateOnly dueDate)
        {
            
            var currentDate = DateOnly.FromDateTime(DateTime.UtcNow);
            if (dueDate > currentDate)
            {
                return 0;
            }
         
            int daysOverdue = Math.Max(0, currentDate.DayNumber - dueDate.DayNumber);

            return daysOverdue;
        }

        public decimal CalculateInterestAmount(decimal value, int daysOverdue)
        {
            decimal interestRate = 0.025m;
            decimal interestAmount = value * interestRate * daysOverdue;
            return interestAmount;
        }
    }
}

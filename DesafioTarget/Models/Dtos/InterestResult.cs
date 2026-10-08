namespace DesafioTarget.Models.Dtos
{
    public class InterestResult
    {
        public decimal Value { get; set; }
        public decimal InterestAmount { get; set; }
        public int DaysOverdue { get; set; }

        public InterestResult(decimal value, decimal interestAmount, int daysOverdue)
        {
            Value = value;
            InterestAmount = interestAmount;
            DaysOverdue = daysOverdue;
        }
    }
}

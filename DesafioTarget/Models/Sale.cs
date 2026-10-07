namespace DesafioTarget.Models
{
    public class Sale
    {
        public string Seller { get; set; } = string.Empty;

        public decimal Amount { get; set; }   
        public Sale() { }

        public Sale(string seller, decimal amount)
        {
            Seller = seller;
            Amount = amount;
        }
    }
}

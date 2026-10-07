namespace DesafioTarget.Models
{
    public class StockMovement
    {
        public Guid Id { get; set; }

        public int ProductCode { get; set; }

        public int Quantity { get; set; }

        public StockMovementType Type { get; set; }

        public string Description { get; set; } = string.Empty;
    }
}

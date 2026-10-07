namespace DesafioTarget.Models.Dtos
{
    public class StockMovementRequest
    {
        public int ProductCode { get; set; }

        public int Quantity { get; set; }

        public StockMovementType Type { get; set; }

        public string Description { get; set; } = string.Empty;
    }
}
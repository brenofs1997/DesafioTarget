namespace DesafioTarget.Models.Dtos
{
    public class StockMovementResult
    {
        public Guid MovementId { get; set; }

        public int ProductCode { get; set; }

        public string ProductDescription { get; set; } = string.Empty;

        public StockMovementType MovementType { get; set; }

        public string Description { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public int FinalStock { get; set; }
    }
}

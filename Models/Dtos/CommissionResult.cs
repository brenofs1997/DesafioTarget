namespace DesafioTarget.DTOs;

public class CommissionResult
{
    public string Seller { get; set; } = string.Empty;

    public decimal SaleAmount { get; set; }

    public decimal Commission { get; set; }
}
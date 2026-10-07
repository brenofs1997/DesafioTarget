using System.Text.Json.Serialization;

namespace DesafioTarget.Models.Dtos
{
    public class SaleItemDto
    {
        [JsonPropertyName("vendedor")]
        public string Vendedor { get; set; } = string.Empty;

        [JsonPropertyName("valor")]
        public decimal Valor { get; set; }
    }
}

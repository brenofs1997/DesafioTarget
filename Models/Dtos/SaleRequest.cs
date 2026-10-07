using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DesafioTarget.Models.Dtos
{
    public class SaleRequest
    {
        [JsonPropertyName("vendas")]
        public List<SaleItemDto> Vendas { get; set; } = new();
    }


}

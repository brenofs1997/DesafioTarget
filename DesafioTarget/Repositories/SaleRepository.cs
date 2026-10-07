using DesafioTarget.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DesafioTarget.Repositories
{
    
    public sealed class SaleRepository(string filePath) : ISaleRepository
    {
        private sealed record SalesFile([property: JsonPropertyName("vendas")] List<SaleDto> Sales);
        private sealed record SaleDto(
            [property: JsonPropertyName("vendedor")] string Seller,
            [property: JsonPropertyName("valor")] decimal Amount);

        public async Task<IReadOnlyList<Sale>> GetAllAsync(CancellationToken ct = default)
        {
            await using var stream = File.OpenRead(filePath);
            var file = await JsonSerializer.DeserializeAsync<SalesFile>(stream, cancellationToken: ct)
                       ?? throw new InvalidOperationException("Arquivo de vendas inválido.");

            return file.Sales.Select(s => new Sale(s.Seller, s.Amount)).ToList();
        }
    }
}

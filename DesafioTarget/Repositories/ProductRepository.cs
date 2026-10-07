using DesafioTarget.Common;
using DesafioTarget.Models;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DesafioTarget.Repositories
{
    public sealed class ProductRepository(string filePath) : IProductRepository
    {
        private sealed record ProductsFile([property: JsonPropertyName("estoque")] List<ProductDto> Products);
        private sealed record ProductDto(
            [property: JsonPropertyName("codigoProduto")] int ProductCode,
            [property: JsonPropertyName("descricaoProduto")] string Description,
            [property: JsonPropertyName("estoque")] int Stock);

        public async Task<IReadOnlyList<Product>> GetAllAsync(
        CancellationToken ct = default)
        {
            await using var stream = File.OpenRead(filePath);

            var file = await JsonSerializer.DeserializeAsync<ProductsFile>(
                stream,
                cancellationToken: ct);

            if (file is null)
            {
                throw new NotFoundException("Arquivo de estoque inválido.");
            }

            return file.Products
                .Select(product => new Product
                {
                    ProductCode = product.ProductCode,
                    Description = product.Description,
                    Stock = product.Stock
                })
                .ToList();
        }

        public async Task<Product?> GetByCodeAsync(
            int productCode,
            CancellationToken ct = default)
        {
            var products = await GetAllAsync(ct);

            return products.FirstOrDefault( product => product.ProductCode == productCode);
        }

        public async Task UpdateAsync(
            Product product,
            CancellationToken ct = default)
        {
            var products = (await GetAllAsync(ct)).ToList();

            var existingProduct = products.FirstOrDefault(
                item => item.ProductCode == product.ProductCode);

            if (existingProduct is null)
            {
                throw new NotFoundException("Produto não encontrado.");
            }

            existingProduct.Stock = product.Stock;

            var file = new ProductsFile(
                products.Select(item => new ProductDto(
                    item.ProductCode,
                    item.Description,
                    item.Stock))
                .ToList());

            await using var stream = File.Create(filePath);

            await JsonSerializer.SerializeAsync(
                stream,
                file,
                new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                },
                ct);
        }
    }
}

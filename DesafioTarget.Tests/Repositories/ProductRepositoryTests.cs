using DesafioTarget.Common;
using DesafioTarget.Models;
using DesafioTarget.Repositories;

namespace DesafioTarget.Tests.Repositories
{
    public class ProductRepositoryTests : IDisposable
    {
        private const string DefaultJson = """
        {
          "estoque": [
            { "codigoProduto": 101, "descricaoProduto": "Camiseta Polo", "estoque": 50 },
            { "codigoProduto": 102, "descricaoProduto": "Calça Jeans", "estoque": 20 },
            { "codigoProduto": 103, "descricaoProduto": "Tênis Esportivo", "estoque": 0 }
          ]
        }
        """;

        private readonly string _tempDir;

        public ProductRepositoryTests()
        {
            _tempDir = Path.Combine(Path.GetTempPath(), $"product-repo-tests-{Guid.NewGuid()}");
            Directory.CreateDirectory(_tempDir);
        }

        public void Dispose()
        {
            if (Directory.Exists(_tempDir))
                Directory.Delete(_tempDir, recursive: true);
        }

        private string CreateFile(string content = DefaultJson)
        {
            var path = Path.Combine(_tempDir, $"{Guid.NewGuid()}.json");
            File.WriteAllText(path, content);
            return path;
        }

        [Fact]
        public async Task GetAllAsync_ValidFile_ReturnsAllProducts()
        {
            var repository = new ProductRepository(CreateFile());

            var products = await repository.GetAllAsync();

            Assert.Equal(3, products.Count);
        }

        [Fact]
        public async Task GetAllAsync_JsonIsNull_ThrowsNotFoundException()
        {
            var repository = new ProductRepository(CreateFile("null"));

            var ex = await Assert.ThrowsAsync<NotFoundException>(() => repository.GetAllAsync());

            Assert.Equal("Arquivo de estoque inválido.", ex.Message);
        }

        [Fact]
        public async Task GetByCodeAsync_ExistingCode_ReturnsProduct()
        {
            var repository = new ProductRepository(CreateFile());

            var product = await repository.GetByCodeAsync(102);

            Assert.NotNull(product);
            Assert.Equal(102, product!.ProductCode);
            Assert.Equal("Calça Jeans", product.Description);
            Assert.Equal(20, product.Stock);
        }

        [Fact]
        public async Task GetByCodeAsync_NonExistingCode_ReturnsNull()
        {
            var repository = new ProductRepository(CreateFile());

            var product = await repository.GetByCodeAsync(999);

            Assert.Null(product);
        }

        [Fact]
        public async Task UpdateAsync_ExistingProduct_PersistsNewStock()
        {
            var repository = new ProductRepository(CreateFile());

            await repository.UpdateAsync(new Product { ProductCode = 101, Description = "Camiseta Polo", Stock = 75 });

            var updated = await repository.GetByCodeAsync(101);
            Assert.Equal(75, updated!.Stock);
        }

        [Fact]
        public async Task UpdateAsync_NonExistingProduct_ThrowsNotFoundException()
        {
            var repository = new ProductRepository(CreateFile());

            var ex = await Assert.ThrowsAsync<NotFoundException>(() =>
                repository.UpdateAsync(new Product { ProductCode = 999, Description = "X", Stock = 1 }));

            Assert.Equal("Produto não encontrado.", ex.Message);
        }

        [Fact]
        public async Task UpdateAsync_CalledTwice_AccumulatesChanges()
        {
            var repository = new ProductRepository(CreateFile());

            await repository.UpdateAsync(new Product { ProductCode = 101, Description = "", Stock = 60 });
            await repository.UpdateAsync(new Product { ProductCode = 102, Description = "", Stock = 30 });

            var products = await repository.GetAllAsync();
            Assert.Equal(60, products.Single(p => p.ProductCode == 101).Stock);
            Assert.Equal(30, products.Single(p => p.ProductCode == 102).Stock);
        }
    }
}

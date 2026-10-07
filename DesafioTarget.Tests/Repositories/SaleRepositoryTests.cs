using DesafioTarget.Repositories;

namespace DesafioTarget.Tests.Repositories
{
    public class SaleRepositoryTests : IDisposable
    {
        private readonly string _tempDir;

        public SaleRepositoryTests()
        {
            _tempDir = Path.Combine(Path.GetTempPath(), $"sale-repo-tests-{Guid.NewGuid()}");
            Directory.CreateDirectory(_tempDir);
        }

        public void Dispose()
        {
            if (Directory.Exists(_tempDir))
                Directory.Delete(_tempDir, recursive: true);
        }

        private string CreateFile(string content)
        {
            var path = Path.Combine(_tempDir, $"{Guid.NewGuid()}.json");
            File.WriteAllText(path, content);
            return path;
        }

        [Fact]
        public async Task GetAllAsync_ValidFile_ReturnsAllSales()
        {
            var path = CreateFile("""
        {
          "vendas": [
            { "vendedor": "João Silva", "valor": 1200.50 },
            { "vendedor": "Maria Souza", "valor": 1500.00 },
            { "vendedor": "Carlos Lima", "valor": 300.75 }
          ]
        }
        """);
            var repository = new SaleRepository(path);

            var sales = await repository.GetAllAsync();

            Assert.Equal(3, sales.Count);
        }

        [Fact]
        public async Task GetAllAsync_JsonIsNull_ThrowsInvalidOperationException()
        {
            var path = CreateFile("null");
            var repository = new SaleRepository(path);

            var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => repository.GetAllAsync());

            Assert.Equal("Arquivo de vendas inválido.", ex.Message);
        }
    }
}

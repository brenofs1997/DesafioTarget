using DesafioTarget.Models;
using DesafioTarget.Repositories;
using DesafioTarget.Services;
using Moq;

namespace DesafioTarget.Tests.Services;

public class StockServiceTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly StockService _service;

    public StockServiceTests()
    {
        _productRepositoryMock = new Mock<IProductRepository>();

        _service = new StockService(
            _productRepositoryMock.Object);
    }

    [Fact]
    public async Task ShouldIncreaseStock_WhenMovementIsEntry()
    {
        // Arrange
        var product = new Product
        {
            ProductCode = 101,
            Description = "Caneta Azul",
            Stock = 150
        };

        var movement = new StockMovement
        {
            Id = Guid.NewGuid(),
            ProductCode = 101,
            Quantity = 20,
            Type = StockMovementType.Entry,
            Description = "Stock entry"
        };

        _productRepositoryMock
            .Setup(repository => repository.GetByCodeAsync(
                101,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(product);

        // Act
        var result = await _service.ProcessMovementAsync(
            movement);

        // Assert
        Assert.Equal(170, result.Stock);

        _productRepositoryMock.Verify(
            repository => repository.UpdateAsync(
                product,
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task ShouldDecreaseStock_WhenMovementIsExit()
    {
        // Arrange
        var product = new Product
        {
            ProductCode = 101,
            Description = "Caneta Azul",
            Stock = 150
        };

        var movement = new StockMovement
        {
            Id = Guid.NewGuid(),
            ProductCode = 101,
            Quantity = 30,
            Type = StockMovementType.Exit,
            Description = "Stock exit"
        };

        _productRepositoryMock
            .Setup(repository => repository.GetByCodeAsync(
                101,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(product);

        // Act
        var result = await _service.ProcessMovementAsync(
            movement);

        // Assert
        Assert.Equal(120, result.Stock);

        _productRepositoryMock.Verify(
            repository => repository.UpdateAsync(
                product,
                It.IsAny<CancellationToken>()),
            Times.Once);
    }
}
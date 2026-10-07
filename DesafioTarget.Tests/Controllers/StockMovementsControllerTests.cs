using DesafioTarget.Controllers;
using DesafioTarget.Models;
using DesafioTarget.Models.Dtos;
using DesafioTarget.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace DesafioTarget.Tests.Controllers
{
    public class StockMovementsControllerTests
    {
        private readonly Mock<IStockService> _stockServiceMock;
        private readonly StockMovementsController _controller;

        public StockMovementsControllerTests()
        {
            _stockServiceMock = new Mock<IStockService>();
            _controller = new StockMovementsController(_stockServiceMock.Object);
        }

       private static StockMovementRequest CreateRequest(StockMovementType type = StockMovementType.Entry, int quantity = 10) => new()
       {
           ProductCode = 101,
           Quantity = quantity,
           Type = type,
           Description = "Movimentação de teste"
       };

        private static Product CreateProduct(int stock) => new()
        {
            ProductCode = 101,
            Description = "Produto de teste",
            Stock = stock
        };

        [Fact]
        public async Task Create_ValidRequest_ReturnsOkWithStockMovementResult()
        {
            var request = CreateRequest(StockMovementType.Entry, 10);
            _stockServiceMock
                .Setup(s => s.ProcessMovementAsync(It.IsAny<StockMovement>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(CreateProduct(stock: 60));

            var response = await _controller.Create(request, CancellationToken.None);

            var ok = Assert.IsType<OkObjectResult>(response.Result);
            var result = Assert.IsType<StockMovementResult>(ok.Value);

            Assert.Equal(101, result.ProductCode);
            Assert.Equal("Produto de teste", result.ProductDescription);
            Assert.Equal(StockMovementType.Entry, result.MovementType);
            Assert.Equal("Movimentação de teste", result.Description);
            Assert.Equal(10, result.Quantity);
            Assert.Equal(60, result.FinalStock);
        }

        [Fact]
        public async Task Create_CalledTwice_GeneratesDifferentMovementIds()
        {
            _stockServiceMock
                .Setup(s => s.ProcessMovementAsync(It.IsAny<StockMovement>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(CreateProduct(stock: 10));

            var first = await _controller.Create(CreateRequest(), CancellationToken.None);
            var second = await _controller.Create(CreateRequest(), CancellationToken.None);

            var firstId = ((StockMovementResult)((OkObjectResult)first.Result!).Value!).MovementId;
            var secondId = ((StockMovementResult)((OkObjectResult)second.Result!).Value!).MovementId;

            Assert.NotEqual(firstId, secondId);
        }

        [Fact]
        public async Task Create_ServiceThrows_Exception()
        {
            _stockServiceMock
                .Setup(s => s.ProcessMovementAsync(It.IsAny<StockMovement>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new InvalidOperationException("Produto não encontrado."));

            var ex = await Assert.ThrowsAsync<InvalidOperationException>(
                () => _controller.Create(CreateRequest(), CancellationToken.None));

            Assert.Equal("Produto não encontrado.", ex.Message);
        }
    }
}

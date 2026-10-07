using DesafioTarget.Controllers;
using DesafioTarget.DTOs;
using DesafioTarget.Models.Dtos;
using DesafioTarget.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace DesafioTarget.Tests.Controllers;

public class CommissionsControllerTests
{
    private readonly Mock<ICommissionService> _commissionServiceMock;
    private readonly CommissionsController _controller;

    public CommissionsControllerTests()
    {
        _commissionServiceMock = new Mock<ICommissionService>();

        _controller = new CommissionsController(
            _commissionServiceMock.Object);
    }

    [Fact]
    public void ShouldReturnOk_WhenCalculateCommission()
    {
        // Arrange
        var input = new SaleRequest
        {
            Vendas = new List<SaleItemDto>
            {
                new()
                {
                    Vendedor = "João Silva",
                    Valor = 1200.50m
                },
                new()
                {
                    Vendedor = "Maria Souza",
                    Valor = 300m
                }
            }
        };

        var expectedResult = new List<CommissionResult>
        {
            new()
            {
                Seller = "João Silva",
                SaleAmount = 1200.50m,
                Commission = 60.025m
            },
            new()
            {
                Seller = "Maria Souza",
                SaleAmount = 300m,
                Commission = 3m
            }
        };

        _commissionServiceMock
            .Setup(service => service.CalculateCommission(input))
            .Returns(expectedResult);

        // Act
        var result = _controller.Calculate(input);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);

        var commissions =
            Assert.IsAssignableFrom<IEnumerable<CommissionResult>>(
                okResult.Value);

        Assert.Equal(2, commissions.Count());
        Assert.Equal("João Silva", commissions.First().Seller);
        Assert.Equal(60.025m, commissions.First().Commission);
    }
}
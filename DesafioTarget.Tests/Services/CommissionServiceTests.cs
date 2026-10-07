using DesafioTarget.Models;
using DesafioTarget.Repositories;
using DesafioTarget.Services;
using DesafioTarget.Services.Strategies;
using Moq;

namespace DesafioTarget.Tests.Services;

public class CommissionServiceTests
{
    private readonly CommissionService _service;

    public CommissionServiceTests()
    {
        var repositoryMock = new Mock<ISaleRepository>();
        var strategies = new ICommissionStrategy[]

        {
            new NoCommissionStrategy(),
            new OnePercentCommissionStrategy(),
            new FivePercentCommissionStrategy()
        };

        _service = new CommissionService(strategies, repositoryMock.Object);
    }

    [Fact]
    public void ShouldReturnZero_WhenSaleIsBelow100()
    {
        var result = _service.CalculateCommission(99.99m);

        Assert.Equal(0m, result);
    }

    [Fact]
    public void ShouldReturnOnePercent_WhenSaleIsBetween100And499_99()
    {
        var result = _service.CalculateCommission(200m);

        Assert.Equal(2m, result);
    }

    [Fact]
    public void ShouldReturnFivePercent_WhenSaleIs500OrMore()
    {
        var result = _service.CalculateCommission(1000m);

        Assert.Equal(50m, result);
    }

    [Fact]
    public void ShouldReturnFivePercent_WhenSaleIsExactly500()
    {
        var result = _service.CalculateCommission(500m);

        Assert.Equal(25m, result);
    }

    [Fact]
    public void ShouldCalculateCommissionForMultipleSales()
    {
        var sales = new[]
        {
        new Sale { Seller = "João Silva", Amount = 1200.50m },
        new Sale { Seller = "Maria Souza", Amount = 300m },
        new Sale { Seller = "Carlos Oliveira", Amount = 80m },
        new Sale { Seller = "Ana Lima", Amount = 500m }
    };

        var results = sales
            .Select(sale => _service.CalculateCommission(sale.Amount))
            .ToList();

        Assert.Equal(4, results.Count);
        Assert.Equal(60.025m, results[0]);
        Assert.Equal(3m, results[1]);
        Assert.Equal(0m, results[2]);
        Assert.Equal(25m, results[3]);
    }
}


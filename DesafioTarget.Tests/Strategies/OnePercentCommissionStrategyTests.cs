using DesafioTarget.Services.Strategies;

namespace DesafioTarget.Tests.Strategies;


public class OnePercentCommissionStrategyTests
{
    private readonly OnePercentCommissionStrategy _strategy = new();

    [Fact]
    public void ShouldApply_WhenSaleAmountIsBetween100And499_99()
    {
        var result = _strategy.CanApply(100m);

        Assert.True(result);
    }

    [Fact]
    public void ShouldApply_WhenSaleAmountIs499_99()
    {
        var result = _strategy.CanApply(499.99m);

        Assert.True(result);
    }

    [Fact]
    public void ShouldNotApply_WhenSaleAmountIsBelow100()
    {
        var result = _strategy.CanApply(99.99m);

        Assert.False(result);
    }

    [Fact]
    public void ShouldNotApply_WhenSaleAmountIs500OrMore()
    {
        var result = _strategy.CanApply(500m);

        Assert.False(result);
    }

    [Fact]
    public void ShouldCalculateOnePercentCommission()
    {
        var result = _strategy.Calculate(200m);

        Assert.Equal(2m, result);
    }
}
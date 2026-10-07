using DesafioTarget.Services.Strategies;

namespace DesafioTarget.Tests.Strategies;

public class FivePercentCommissionStrategyTests
{
    private readonly FivePercentCommissionStrategy _strategy = new();

    [Fact]
    public void ShouldApply_WhenSaleAmountIs500()
    {
        var result = _strategy.CanApply(500m);

        Assert.True(result);
    }

    [Fact]
    public void ShouldApply_WhenSaleAmountIsAbove500()
    {
        var result = _strategy.CanApply(1000m);

        Assert.True(result);
    }

    [Fact]
    public void ShouldNotApply_WhenSaleAmountIsBelow500()
    {
        var result = _strategy.CanApply(499.99m);

        Assert.False(result);
    }

    [Fact]
    public void ShouldCalculateFivePercentCommission()
    {
        var result = _strategy.Calculate(1000m);

        Assert.Equal(50m, result);
    }
}

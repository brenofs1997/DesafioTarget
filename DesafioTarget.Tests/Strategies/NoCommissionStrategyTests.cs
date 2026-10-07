
using DesafioTarget.Services.Strategies;

namespace DesafioTarget.Tests.Strategies;

public class NoCommissionStrategyTests
{
    private readonly NoCommissionStrategy _strategy = new();

    [Fact]
    public void ShouldApply_WhenSaleAmountIsBelow100()
    {
        var result = _strategy.CanApply(99.99m);

        Assert.True(result);
    }

    [Fact]
    public void ShouldNotApply_WhenSaleAmountIs100OrMore()
    {
        var result = _strategy.CanApply(100m);

        Assert.False(result);
    }

    [Fact]
    public void ShouldReturnZeroCommission()
    {
        var result = _strategy.Calculate(99.99m);

        Assert.Equal(0m, result);
    }
}
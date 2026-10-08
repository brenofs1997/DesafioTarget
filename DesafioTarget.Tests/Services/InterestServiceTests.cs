using DesafioTarget.Common;
using DesafioTarget.Models.Dtos;
using DesafioTarget.Services;

namespace DesafioTarget.Tests.Services;

public class InterestServiceTests
{
    private readonly InterestService _service;

    public InterestServiceTests()
    {
        _service = new InterestService();
    }

    [Fact]
    public void CalculateInterest_ShouldThrow_WhenValueIsZeroOrNegative()
    {
        Assert.Throws<DomainException>(() => _service.CalculateInterest(0m, DateOnly.FromDateTime(DateTime.UtcNow)));
        Assert.Throws<DomainException>(() => _service.CalculateInterest(-10m, DateOnly.FromDateTime(DateTime.UtcNow)));
    }

    [Fact]
    public void CalculateDueDays_ShouldReturnZero_WhenDueDateInFuture()
    {
        var future = DateOnly.FromDateTime(DateTime.UtcNow).AddDays(5);
        var days = _service.CalculateDueDays(future);

        Assert.Equal(0, days);
    }

    [Fact]
    public void CalculateDueDays_ShouldReturnPositive_WhenDueDateInPast()
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var past = today.AddDays(-10);
        var days = _service.CalculateDueDays(past);

        Assert.Equal(10, days);
    }

    [Fact]
    public void CalculateInterestAmount_ShouldReturnExpectedValue()
    {
        decimal value = 100m;
        int days = 4;

        var amount = _service.CalculateInterestAmount(value, days);

        Assert.Equal(value * 0.025m * days, amount);
    }

    [Fact]
    public void CalculateInterest_ShouldReturnInterestResult_WithCorrectValues()
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var past = today.AddDays(-3);
        decimal value = 200m;

        var result = _service.CalculateInterest(value, past);

        Assert.IsType<InterestResult>(result);
        Assert.Equal(value, result.Value);
        Assert.Equal(3, result.DaysOverdue);
        Assert.Equal(value * 0.025m * 3, result.InterestAmount);
    }
}

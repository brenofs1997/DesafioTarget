using DesafioTarget.Controllers;
using DesafioTarget.Models.Dtos;
using DesafioTarget.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace DesafioTarget.Tests.Controllers;

public class InterestControllerTests
{
    private readonly Mock<IInterestService> _interestServiceMock;
    private readonly InterestController _controller;

    public InterestControllerTests()
    {
        _interestServiceMock = new Mock<IInterestService>();

        _controller = new InterestController(
            _interestServiceMock.Object);
    }

    [Fact]
    public void Calculate_ShouldReturnOk_WithInterestResult()
    {
        var expected = new InterestResult(100m, 5m, 2);
        var dueDate = DateOnly.FromDateTime(DateTime.UtcNow);

        _interestServiceMock.Setup(s => s.CalculateInterest(100m, dueDate)).Returns(expected);

        var actionResult = _controller.Calculate(100m, dueDate);

        var ok = Assert.IsType<OkObjectResult>(actionResult.Result);
        var value = Assert.IsType<InterestResult>(ok.Value);

        Assert.Equal(expected.Value, value.Value);
        Assert.Equal(expected.InterestAmount, value.InterestAmount);
        Assert.Equal(expected.DaysOverdue, value.DaysOverdue);
    }
}

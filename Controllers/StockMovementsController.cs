using DesafioTarget.Models;
using DesafioTarget.Models.Dtos;
using DesafioTarget.Services;
using Microsoft.AspNetCore.Mvc;

namespace DesafioTarget.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StockMovementsController : ControllerBase
{
    private readonly IStockService _stockService;

    public StockMovementsController(IStockService stockService)
    {
        _stockService = stockService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(StockMovementResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<StockMovementResult>> Create([FromBody] StockMovementRequest request,CancellationToken ct)
    {
        var movement = new StockMovement
        {
            Id = Guid.NewGuid(),
            ProductCode = request.ProductCode,
            Quantity = request.Quantity,
            Type = request.Type,
            Description = request.Description
        };

        var product = await _stockService.ProcessMovementAsync(
            movement,
            ct);

        var result = new StockMovementResult
        {
            MovementId = movement.Id,
            ProductCode = product.ProductCode,
            ProductDescription = product.Description,
            MovementType = movement.Type,
            Description = movement.Description,
            Quantity = movement.Quantity,
            FinalStock = product.Stock
        };

        return Ok(result);
    }
}
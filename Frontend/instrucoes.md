namespace DesafioTarget.Models.Dtos
{
    public class InterestResult
    {
        public decimal Value { get; set; }
        public decimal InterestAmount { get; set; }
        public int DaysOverdue { get; set; }

        public InterestResult(decimal value, decimal interestAmount, int daysOverdue)
        {
            Value = value;
            InterestAmount = interestAmount;
            DaysOverdue = daysOverdue;
        }
    }
}

namespace DesafioTarget.DTOs;

public class CommissionResult
{
    public string Seller { get; set; } = string.Empty;

    public decimal SaleAmount { get; set; }

    public decimal Commission { get; set; }
}

namespace DesafioTarget.Models.Dtos
{
    public class StockMovementResult
    {
        public Guid MovementId { get; set; }

        public int ProductCode { get; set; }

        public string ProductDescription { get; set; } = string.Empty;

        public StockMovementType MovementType { get; set; }

        public string Description { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public int FinalStock { get; set; }
    }
}

namespace DesafioTarget.Models.Dtos
{
    public class StockMovementRequest
    {
        public int ProductCode { get; set; }

        public int Quantity { get; set; }

        public StockMovementType Type { get; set; }

        public string Description { get; set; } = string.Empty;
    }
}

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DesafioTarget.Models.Dtos
{
    public class SaleRequest
    {
        [JsonPropertyName("vendas")]
        public List<SaleItemDto> Vendas { get; set; } = new();
    }


}

using DesafioTarget.DTOs;
using DesafioTarget.Models.Dtos;
using DesafioTarget.Services;
using Microsoft.AspNetCore.Mvc;

namespace DesafioTarget.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommissionsController : ControllerBase
{
    private readonly ICommissionService _commissionService;

    public CommissionsController(ICommissionService commissionService)
    {
        _commissionService = commissionService;
    }

    [HttpPost("calculate")]
    [ProducesResponseType(typeof(CommissionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IEnumerable<CommissionResult>> Calculate([FromBody] SaleRequest inputModel)
    {
        var results = _commissionService.CalculateCommission(inputModel);

        return Ok(results);
    }

    [HttpGet("calculate")]
    [ProducesResponseType(typeof(CommissionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<CommissionResult>>> Calculate(CancellationToken ct)
    {
        var results = await _commissionService.CalculateCommissionAsync(ct);

        return Ok(results);
    }
}
using DesafioTarget.Models.Dtos;
using DesafioTarget.Services;
using Microsoft.AspNetCore.Mvc;

namespace DesafioTarget.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InterestController: ControllerBase
    {
        private readonly IInterestService _interestService;
        public InterestController(IInterestService interestService)
        {
            _interestService = interestService;
        }

        [HttpGet("calculate")]
        [ProducesResponseType(typeof(InterestResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<InterestResult> Calculate([FromQuery] decimal value,  [FromQuery] DateOnly dueDate)
        {
            var result = _interestService.CalculateInterest(value, dueDate);
            return Ok(result);
        }

    }
}
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
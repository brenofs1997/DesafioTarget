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
    public ActionResult<IEnumerable<CommissionResult>> Calculate([FromBody] SaleRequest inputModel)
    {
        var results = _commissionService.CalculateCommission(inputModel);

        return Ok(results);
    }

    [HttpGet("calculate")]
    public async Task<ActionResult<IEnumerable<CommissionResult>>> Calculate(CancellationToken ct)
    {
        var results = await _commissionService.CalculateCommissionAsync(ct);

        return Ok(results);
    }
}

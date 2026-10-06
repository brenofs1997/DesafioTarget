using DesafioTarget.DTOs;
using DesafioTarget.Models;
using DesafioTarget.Services;
using Microsoft.AspNetCore.Mvc;

namespace DesafioTarget.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommissionsController : ControllerBase
{
    private readonly CommissionService _commissionService;

    public CommissionsController(CommissionService commissionService)
    {
        _commissionService = commissionService;
    }

    [HttpPost("calculate")]
    public ActionResult<IEnumerable<CommissionResult>> Calculate(
    IEnumerable<Sale> sales)
    {
        var results = sales.Select(sale => new CommissionResult
        {
            Seller = sale.Seller,
            SaleAmount = sale.Amount,
            Commission = _commissionService.CalculateCommission(sale.Amount)
        });

        return Ok(results);
    }
}
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

using System.Threading.Tasks;
using MDC.ContributionService.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MDC.ValidationService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValidationController : ControllerBase
    {
       
        private readonly ILogger<ValidationController> _logger;
        private readonly IMediator _bus;

        public ValidationController(ILogger<ValidationController> logger, IMediator bus)
        {
            _logger = logger;
            _bus = bus;
        }

        [HttpPost]
        public async Task<IActionResult> Contribute([FromBody] ValidationRequest request)
        {
            _logger.LogInformation($"New {request.ContributionRequest.ContributionType}");
            await _bus.Send(request);
            return Ok();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetValues()
        {
            return Ok("Success");
        }
    }
}

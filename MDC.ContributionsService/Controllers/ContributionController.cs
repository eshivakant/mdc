using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MDC.ContributionService.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;

namespace MDC.ContributionsService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContributionController : ControllerBase
    {
       
        private readonly ILogger<ContributionController> _logger;
        private readonly IMediator _bus;

        public ContributionController(ILogger<ContributionController> logger, IMediator bus)
        {
            _logger = logger;
            _bus = bus;
        }

        [HttpPost]
        public async Task<IActionResult> Contribute([FromBody] ContributionRequest request)
        {
            _logger.LogInformation($"New {request.ContributionType}");
            await _bus.Send(request);
            var key = HttpContext.Request.Headers["Authorization"];
            var validationRequest = new ValidationRequest() {ContributionRequest = request, AuthToken = key[0].Split(' ').Last()};
            await _bus.Send(validationRequest);
            return Ok();
        }

        [HttpGet("{contributionType}/{requestId}")]
        [AllowAnonymous]
        public async Task<IActionResult> Contribute(string contributionType, string requestId)
        {
            var request = new ContributionGetRequest() {RequestId = requestId, ContributionType = contributionType};
            _logger.LogInformation($"New request: {request.ContributionType}");
            await _bus.Send(request);
            return Ok();
        }
    }
}

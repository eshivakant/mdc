using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MDC.ContributionService.Common;
using MDC.ContributionsService.Hubs;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace MDC.ContributionsService.Commands
{
    public class ContributionRequestHandler : IRequestHandler<ContributionRequest>
    {
        private readonly IHubContext<ContributionsHub> _hub;

        public ContributionRequestHandler(IHubContext<ContributionsHub> chatHubContext)
        {
            this._hub = chatHubContext;
        }

        public async Task<Unit> Handle(ContributionRequest request, CancellationToken cancellationToken)
        {
            await _hub.Clients.All.SendAsync("broadcastMessage", "ContributionRequest", "Received contribution request: " + request.ToString());
            await _hub.Clients.All.SendAsync("broadcastMessage", "ContributionRequest", "Submitting to validation service " + request.ToString());
            await Task.Delay(2000);
            return Unit.Value;
        }
    }

    public class ValidationRequestHandler : IRequestHandler<ValidationRequest>
    {
        private readonly IValidationService _validationService;

        public ValidationRequestHandler(IValidationService validationService)
        {
            _validationService = validationService;
        }

        public async Task<Unit> Handle(ValidationRequest request, CancellationToken cancellationToken)
        {
            await _validationService.Validate(request, request.AuthToken);
            return Unit.Value;
        }
    }
}

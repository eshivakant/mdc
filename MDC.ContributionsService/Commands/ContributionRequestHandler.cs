using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MDC.ContributionService.Common;
using MDC.ContributionsService.Data;
using MDC.ContributionsService.Hubs;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace MDC.ContributionsService.Commands
{
    public class ContributionRequestHandler : IRequestHandler<ContributionRequest>
    {
        private readonly IHubContext<ContributionsHub> _hub;
        private readonly IContributionsRepo _contributionsRepo;

        public ContributionRequestHandler(IHubContext<ContributionsHub> chatHubContext, IContributionsRepo contributionsRepo)
        {
            this._hub = chatHubContext;
            _contributionsRepo = contributionsRepo;
        }

        public async Task<Unit> Handle(ContributionRequest request, CancellationToken cancellationToken)
        {
            await _hub.Clients.All.SendAsync("broadcastMessage", "ContributionRequest", "Received contribution request: " + request.ToString());
            _contributionsRepo.Save(request);
            await _hub.Clients.All.SendAsync("broadcastMessage", "ContributionRequest", "Submitting to validation service " + request.ToString());
            return Unit.Value;
        }
    }
}

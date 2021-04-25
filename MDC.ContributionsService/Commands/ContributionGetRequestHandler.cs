using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MDC.ContributionService.Common;
using MDC.ContributionsService.Data;
using MDC.ContributionsService.Hubs;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace MDC.ContributionsService.Commands
{
    public class ContributionGetRequestHandler : IRequestHandler<ContributionGetRequest>
    {
        private readonly IHubContext<ContributionsHub> _hub;
        private readonly IContributionsRepo _contributionsRepo;

        public ContributionGetRequestHandler(IHubContext<ContributionsHub> chatHubContext, IContributionsRepo contributionsRepo)
        {
            this._hub = chatHubContext;
            _contributionsRepo = contributionsRepo;
        }

        public async Task<Unit> Handle(ContributionGetRequest request, CancellationToken cancellationToken)
        {
            await _hub.Clients.All.SendAsync("broadcastContributions", "ContributionGetRequest", _contributionsRepo.GetAll().ToList());
            return Unit.Value;
        }
    }
}
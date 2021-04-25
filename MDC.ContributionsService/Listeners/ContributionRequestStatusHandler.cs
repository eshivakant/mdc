using System.Threading;
using System.Threading.Tasks;
using MDC.ContributionService.Common;
using MDC.ContributionsService.Hubs;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace MDC.ContributionsService.Listeners
{
    public class ContributionRequestStatusHandler : INotificationHandler<RequestStatus>
    {
        private readonly IHubContext<ContributionsHub> _hub;

        public ContributionRequestStatusHandler(IHubContext<ContributionsHub> hub)
        {
            _hub = hub;
        }

        public async Task Handle(RequestStatus notification, CancellationToken cancellationToken)
        {
            await _hub.Clients.All.SendAsync("RequestStatusUpdated", notification, cancellationToken);
        }
    }

   
}
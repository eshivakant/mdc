using System.Threading;
using System.Threading.Tasks;
using MDC.ContributionService.Common;
using MDC.ValidationService.Hubs;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace MDC.ValidationService.Listeners
{
    public class ContributionRequestStatusHandler : INotificationHandler<RequestStatus>
    {
        private readonly IHubContext<ValidationHub> _hub;

        public ContributionRequestStatusHandler(IHubContext<ValidationHub> hub)
        {
            _hub = hub;
        }

        public async Task Handle(RequestStatus notification, CancellationToken cancellationToken)
        {
            await _hub.Clients.All.SendAsync("RequestStatusUpdated", notification, cancellationToken);
        }
    }

   
}
using System.Threading;
using System.Threading.Tasks;
using MDC.ContributionService.Common;
using MDC.ValidationService.Hubs;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace MDC.ValidationService.Commands
{
    public class ValidationRequestHandler : IRequestHandler<ValidationRequest>
    {
        private readonly IHubContext<ValidationHub> _hub;

        public ValidationRequestHandler(IHubContext<ValidationHub> chatHubContext)
        {
            this._hub = chatHubContext;
        }

        public async Task<Unit> Handle(ValidationRequest request, CancellationToken cancellationToken)
        {
            await _hub.Clients.All.SendAsync("broadcastMessage", "ValidationRequest", "Received validation request: " + request.ToString());

            await _hub.Clients.All.SendAsync("broadcastMessage", "ValidationRequest", "Validation Successful: " + request.ToString());
            return Unit.Value;
        }
    }

  
}

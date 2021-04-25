using System.Threading;
using System.Threading.Tasks;
using MDC.ContributionService.Common;
using MDC.ValidationService.Hubs;
using MDC.ValidationService.Validation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace MDC.ValidationService.Commands
{
    public class ValidationRequestHandler : IRequestHandler<ValidationRequest>
    {
        private readonly IHubContext<ValidationHub> _hub;
        private readonly ValidatorFactory _validatorFactory;

        public ValidationRequestHandler(IHubContext<ValidationHub> chatHubContext, ValidatorFactory validatorFactory)
        {
            this._hub = chatHubContext;
            _validatorFactory = validatorFactory;
        }

        public async Task<Unit> Handle(ValidationRequest request, CancellationToken cancellationToken)
        {
            await _hub.Clients.All.SendAsync("broadcastMessage", "ValidationRequest", "Received validation request: " + request.ToString());

            var errors = _validatorFactory.CreateValidator(request.ContributionRequest.ContributionType)
                .Validate(request.ContributionRequest);

            if (!string.IsNullOrEmpty(errors))
            {
                await _hub.Clients.All.SendAsync("broadcastMessage", "ValidationRequest",
                    "Validation Failed: " + request.ToString() + $" {errors}");
            }
            else
            {
                await _hub.Clients.All.SendAsync("broadcastMessage", "ValidationRequest",
                    "Validation Successful: " + request.ToString());
            }

            return Unit.Value;
        }
    }
}

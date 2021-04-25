using System.Threading;
using System.Threading.Tasks;
using MDC.ContributionService.Common;
using MediatR;

namespace MDC.ContributionsService.Commands
{
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
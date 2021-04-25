using MediatR;

namespace MDC.ContributionService.Common
{
    public class ValidationRequest : IRequest
    {
        public string AuthToken { get; set; }
        public ContributionRequest ContributionRequest { get; set; }

        public override string ToString()
        {
            return $"Validate {ContributionRequest}";
        }
    }
}
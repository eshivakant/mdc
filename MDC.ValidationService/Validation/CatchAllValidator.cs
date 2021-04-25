using MDC.ContributionService.Common;
using MDC.ValidationService.Commands;

namespace MDC.ValidationService.Validation
{
    public class CatchAllValidator : IContributionRequestValidator
    {
        public string Validate(ContributionRequest request)
        {
            return "Wrong contribution type";
        }
    }
}
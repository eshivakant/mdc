using MDC.ContributionService.Common;

namespace MDC.ValidationService.Validation
{
    public interface IContributionRequestValidator
    {
        string Validate(ContributionRequest request);
    }
}
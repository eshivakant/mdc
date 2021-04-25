using System.Threading.Tasks;

namespace MDC.ContributionService.Common
{
    public interface IValidationService
    {
        Task<string> Validate(ValidationRequest request, string authToken);
    }
}
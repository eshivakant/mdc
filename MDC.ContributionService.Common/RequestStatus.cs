using MediatR;

namespace MDC.ContributionService.Common
{
    public class RequestStatus : INotification
    {
        public ContributionRequest ContributionRequest { get; set; }
        public StatusEnum Status { get; set; }
        public string Message { get; set; }
    }
}
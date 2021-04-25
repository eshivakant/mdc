using System.Collections.Generic;
using MediatR;

namespace MDC.ContributionService.Common
{
    public class ContributionRequest : IRequest
    {
        public string ContributionType { get; set; } //e.g. FxQuote

        public IEnumerable<RequestField> RequestFields { get; set; }

        public override string ToString()
        {
            return $"{ContributionType} {string.Join(",", RequestFields)}";
        }
    }

    public class ContributionGetRequest : IRequest
    {
        public string RequestId { get; set; }
        public string ContributionType { get; set; } //e.g. FxQuote

    }
}

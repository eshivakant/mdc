using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MDC.ContributionService.Common;

namespace MDC.ContributionsService.Data
{
    public interface IContributionsRepo
    {
        void Save(ContributionRequest request);
        IEnumerable<ContributionRequest> GetAll();
    }

    /// <summary>
    /// To avoid an external database dependency, I am using an in memory db - Ideally this should be something durable
    /// Wondering if a time series db will be a good fit - don't know a lot about data retrieval pattern
    /// </summary>
    public class ContributionsRepo : IContributionsRepo
    {
        public ConcurrentBag<ContributionRequest> ContributionRequests { get; set; } =
            new ConcurrentBag<ContributionRequest>();

        public ContributionsRepo()
        {
            ContributionRequests.Add(new ContributionRequest(){ContributionType = "FxQuote", RequestFields = new List<RequestField>(){new RequestField(){FieldName = "Ticker",Type = FieldType.StringField,Value = "GBP/USD"}, new RequestField(){FieldName = "Bid", Type = FieldType.FloatField, Value = "1.3333"}, new RequestField() { FieldName = "Ask", Type = FieldType.FloatField, Value = "1.3334" } } });
        }

        public void Save(ContributionRequest request)
        {
            ContributionRequests.Add(request);
        }

        public IEnumerable<ContributionRequest> GetAll()
        {
            return ContributionRequests;
        }
    }
}

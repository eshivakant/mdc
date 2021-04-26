using System.Linq;
using System.Text.RegularExpressions;
using MDC.ContributionService.Common;

namespace MDC.ValidationService.Validation
{
    public class FxQuoteValidator: IContributionRequestValidator
    {
        public string Validate(ContributionRequest request)
        {
            var ticker = request.RequestFields.FirstOrDefault(f => f.FieldName == "Ticker");
            if (ticker?.Value == null)
                return "Missing ticker";

            if (!Regex.IsMatch(ticker.Value.ToString() ?? string.Empty, "([A-Z]{3})/+([A-Z]{3})"))
                return "Incorrect ticket format";

            var bid = request.RequestFields.FirstOrDefault(r => r.FieldName == "Bid");
            if (bid == null)
                return "Bid price not found";

            var bp = bid.Value as double?;
            if (bp == null || bp < 0)
                return "Invalid bid price";

            var ask = request.RequestFields.FirstOrDefault(r => r.FieldName == "Ask");
            if (ask == null)
                return "Ask price not found";

            var ap = ask.Value as double?;
            if (ap == null || ap < 0)
                return "Invalid ask price";

            return "";
        }
    }
}
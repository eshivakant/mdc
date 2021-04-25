using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MDC.ContributionService.Common;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MDC.WebClient.Data
{
    public class ContributionService
    {
        private readonly HttpClient _httpClient;
        private readonly State _state;
        private readonly ILogger<ContributionService> _logger;

        public ContributionService(HttpClient httpClient, State state, ILogger<ContributionService> logger)
        {
            this._httpClient = httpClient;
            _state = state;
            _logger = logger;
        }

        public async Task<string> Contribute(ContributionRequest request)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _state.AuthToken);
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync("api/Contribution", content);
            return !result.IsSuccessStatusCode ? result.ReasonPhrase : String.Empty;
        }

    }
}
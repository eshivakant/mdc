using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MDC.ContributionService.Common;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MDC.ContributionsService.Data
{
    public class ValidationService : IValidationService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ValidationService> _logger;

        public ValidationService(HttpClient httpClient, ILogger<ValidationService> logger)
        {
            this._httpClient = httpClient;
            _logger = logger;
        }

        public async Task<string> Validate(ValidationRequest request, string authToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync("api/Validation", content);
            return !result.IsSuccessStatusCode ? result.ReasonPhrase : String.Empty;
        }

    }
}
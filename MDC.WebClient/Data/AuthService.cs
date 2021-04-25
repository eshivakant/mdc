using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MDC.ContributionService.Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MDC.WebClient.Data
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly State _state;
        private readonly ILogger<AuthService> _logger;

        public AuthService(HttpClient httpClient, State state, ILogger<AuthService> logger)
        {
            this._httpClient = httpClient;
            _state = state;
            _logger = logger;
        }

        public async Task<string> Authenticate(AuthRequest request)
        {
            var content = new StringContent(JsonConvert.SerializeObject(request) , Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync("api/User",content);
            if (!result.IsSuccessStatusCode) return result.ReasonPhrase;
            _state.AuthToken =  await result.Content.ReadAsStringAsync();

            if(_httpClient.DefaultRequestHeaders.Contains("Authorization")) 
                _httpClient.DefaultRequestHeaders.Remove("Authorization");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _state.AuthToken);
            return String.Empty;
        }

        public void SignOut()
        {
            if (_httpClient.DefaultRequestHeaders.Contains("Authorization"))
                _httpClient.DefaultRequestHeaders.Remove("Authorization");

            _state.AuthToken = null;
            _state.User = null;
        }

        public async Task<string> TestAuth()
        {
            try
            {
                var result = await _httpClient.GetAsync("api/User");
                if (result.IsSuccessStatusCode)
                    return "";
                return result.ReasonPhrase;

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return e.Message;
            }
        }
    }
}

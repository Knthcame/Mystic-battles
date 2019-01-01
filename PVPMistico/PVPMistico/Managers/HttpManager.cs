using Models.ApiResponses.Interfaces;
using Newtonsoft.Json;
using PVPMistico.Logging.Interfaces;
using PVPMistico.Managers.Interfaces;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PVPMistico.Managers
{
    public class HttpManager : IHttpManager
    {
        private readonly ICustomLogger _logger;
        private HttpClient _client;

        public HttpManager(ICustomLogger logger)
        {
            _logger = logger;
            _client = new HttpClient();
        }

        public async Task<ApiResponse> DeleteAsync<ApiResponse>(string url, object body) where ApiResponse : IApiResponse
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse> GetAsync<ApiResponse>(string url, object body) where ApiResponse : IApiResponse
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse> PostAsync<ApiResponse>(string url, object body) where ApiResponse : IApiResponse
        {
            try
            {
                var json = JsonConvert.SerializeObject(body);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _client.PostAsync(url, content);
                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ApiResponse>(responseContent);
            }
            catch(Exception e)
            {
                _logger.Error(e.Message);
                return default(ApiResponse);
            }
        }

        public async Task<ApiResponse> PutAsync<ApiResponse>(string url, object body) where ApiResponse : IApiResponse
        {
            throw new NotImplementedException();
        }
    }
}

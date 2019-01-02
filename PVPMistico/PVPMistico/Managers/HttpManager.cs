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

        public async Task<ApiResponse> DeleteAsync<ApiResponse>(string url, string extension = "", string parameter = "")
        {
            try
            {
                var uri = url + extension + parameter;
                return await ParseResponse<ApiResponse>(await _client.DeleteAsync(uri));
            }
            catch (Exception e)
            {
                return HandleClientError<ApiResponse>(e);
            }
        }

        public async Task<ApiResponse> GetAsync<ApiResponse>(string url, string extension = "", string parameter = "")
        {
            try
            {
                var uri = url + extension + parameter;
                return await ParseResponse<ApiResponse>(await _client.GetAsync(uri));
            }
            catch(Exception e)
            {
                return HandleClientError<ApiResponse>(e);
            }
        }

        public async Task<ApiResponse> PostAsync<ApiResponse>(string url, object body, string extension = "", string parameter = "")
        {
            try
            {
                var uri = url + extension + parameter;
                var json = JsonConvert.SerializeObject(body);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                return await ParseResponse<ApiResponse>(await _client.PostAsync(uri, content));
            }
            catch(Exception e)
            {
                return HandleClientError<ApiResponse>(e);
            }
        }

        public async Task<ApiResponse> PutAsync<ApiResponse>(string url, object body, string extension = "", string parameter = "")
        {
            try
            {
                var uri = url + extension + parameter;
                var json = JsonConvert.SerializeObject(body);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                return await ParseResponse<ApiResponse>(await _client.PutAsync(uri, content));
            }
            catch (Exception e)
            {
                return HandleClientError<ApiResponse>(e);
            }
        }

        private async Task<ApiResponse> ParseResponse<ApiResponse>(HttpResponseMessage response)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ApiResponse>(responseContent);
        }

        private ApiResponse HandleClientError<ApiResponse>(Exception e)
        {
            _logger.Error(e.Message);
            return default(ApiResponse);
        }
    }
}

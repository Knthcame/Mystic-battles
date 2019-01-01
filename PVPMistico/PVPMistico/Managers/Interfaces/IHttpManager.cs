using Models.ApiResponses.Interfaces;
using System.Threading.Tasks;

namespace PVPMistico.Managers.Interfaces
{
    public interface IHttpManager
    {
        Task<ApiResponse> GetAsync<ApiResponse>(string url, object body) where ApiResponse : IApiResponse;

        Task<ApiResponse> PostAsync<ApiResponse>(string url, object body) where ApiResponse : IApiResponse;

        Task<ApiResponse> PutAsync<ApiResponse>(string url, object body) where ApiResponse : IApiResponse;

        Task<ApiResponse> DeleteAsync<ApiResponse>(string url, object body) where ApiResponse : IApiResponse;
    }
}

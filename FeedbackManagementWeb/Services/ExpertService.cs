using Domain.Entities;
using Domain.Model;
using FeedbackManagementWeb.Helper;
using FeedbacksManagementApi.Model;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Runtime;
using System.Text;

namespace FeedbackManagementWeb.Services
{
    public interface IExpertService
    {
        Task AddExpert(ExpertBase expert);
        Task DeleteExpert(int expertId);
        Task<IEnumerable<Expert>> GetExperts();
        Task UpdateExpert(ExpertBase expert, int expertId);
        Task<IEnumerable<Expert>> GetExpertReports(ExpertReportFilterModel filter);
        Task<int> GetExpertReportCount(ExpertReportFilterModel filter);
    }

    public class ExpertService : ApiServiceBase, IExpertService
    {
        public ExpertService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public async Task<IEnumerable<Expert>> GetExperts()
        {
            var client = _httpClientFactory.CreateClient("HttpClientWithSSLUntrusted");
            var response = await client.GetAsync($"{AppSettings.ApiBaseUrl}/api/Experts/GetExperts");
            return await new ApiResult<Expert>().GetResultAsList(response);
        }

        public async Task AddExpert(ExpertBase expert)
        {
            var client = _httpClientFactory.CreateClient("HttpClientWithSSLUntrusted");
            var response = await client.PostAsJsonAsync($"{AppSettings.ApiBaseUrl}/api/Experts/AddExpert", expert);
            await new ApiResult<string>().GetResultAsVoid(response);
        }

        public async Task UpdateExpert(ExpertBase expert, int expertId)
        {
            var client = _httpClientFactory.CreateClient("HttpClientWithSSLUntrusted");

            var content = new StringContent(JsonConvert.SerializeObject(expert), Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"{AppSettings.ApiBaseUrl}/api/Experts/UpdateExpert/{expertId}", content);
            await new ApiResult<string>().GetResultAsVoid(response);
        }

        public async Task DeleteExpert(int expertId)
        {
            var client = _httpClientFactory.CreateClient("HttpClientWithSSLUntrusted");
            var response = await client.DeleteAsync($"{AppSettings.ApiBaseUrl}/api/Experts/DeleteExpert/{expertId}");
            await new ApiResult<string>().GetResultAsVoid(response);
        }

        public async Task<IEnumerable<Expert>> GetExpertReports(ExpertReportFilterModel filter)
        {
            var client = _httpClientFactory.CreateClient("HttpClientWithSSLUntrusted");

            var response = await client.PostAsJsonAsync($"{AppSettings.ApiBaseUrl}/api/Experts/GetExpertReports", filter);
            return await new ApiResult<Expert>().GetResultAsList(response);
        }

        public async Task<int> GetExpertReportCount(ExpertReportFilterModel filter)
        {
            var client = _httpClientFactory.CreateClient("HttpClientWithSSLUntrusted");

            var response = await client.PostAsJsonAsync($"{AppSettings.ApiBaseUrl}/api/Experts/GetExpertReportCount", filter);
            return await new ApiResult<int>().GetResultAsObject(response);
        }
    }
}

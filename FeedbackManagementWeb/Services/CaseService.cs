using Domain.Entities;
using FeedbackManagementWeb.Helper;
using FeedbackManagementWeb.Interface;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime;
using System.Text;

namespace FeedbackManagementWeb.Services
{
    public class CaseService : ApiServiceBase , ICaseService
    {
        public CaseService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public async Task<IEnumerable<CaseReport>> GetCases(int take , int skip)
        {
            //_ = new ValidatorAtrrbute<ExamBase>(experiment);
            var client = _httpClientFactory.CreateClient("HttpClientWithSSLUntrusted");
            var response = await client.GetAsync($"{AppSettings.ApiBaseUrl}/api/Cases/GetCases?skip={skip}&take={take}");
            return await new ApiResult<CaseReport>().GetResultAsList(response);
        }

        public async Task<int> GetCasesCount()
        {
            var client = _httpClientFactory.CreateClient("HttpClientWithSSLUntrusted");
            var response = await client.GetAsync($"{AppSettings.ApiBaseUrl}/api/Cases/GetCasesCount");
            return await new ApiResult<int>().GetResultAsObject(response);
        }

        public async Task AddCase(CaseBase @case)
        {
            var client = _httpClientFactory.CreateClient("HttpClientWithSSLUntrusted");
            var response = await client.PostAsJsonAsync($"{AppSettings.ApiBaseUrl}/api/Cases/AddCase", @case);
            await new ApiResult<string>().GetResultAsVoid(response);
        }

        public async Task UpdateCase(CaseBase @case, int caseId)
        {
            var client = _httpClientFactory.CreateClient("HttpClientWithSSLUntrusted");

            var content = new StringContent(JsonConvert.SerializeObject(@case), Encoding.UTF8, "application/json");

            var response = await client.PatchAsync($"{AppSettings.ApiBaseUrl}/api/Cases/UpdateCase/{caseId}", content);
            await new ApiResult<string>().GetResultAsVoid(response);
        }

        public async Task DeleteCases(int[] caseIds)
        {
            var client = _httpClientFactory.CreateClient("HttpClientWithSSLUntrusted");
            var request = new HttpRequestMessage
            {
                Content = JsonContent.Create(caseIds),
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"{AppSettings.ApiBaseUrl}/api/Cases/DeleteMultipleCases")
            };
            var response = await client.SendAsync(request);
            await new ApiResult<string>().GetResultAsVoid(response);
        }

        public async Task SubmitCaseForAnswer(int caseId)
        {
            var client = _httpClientFactory.CreateClient("HttpClientWithSSLUntrusted");

            var content = new StringContent(JsonConvert.SerializeObject("something"), Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{AppSettings.ApiBaseUrl}/api/Cases/SubmitCaseForAnswer/{caseId}", content);
            await new ApiResult<string>().GetResultAsVoid(response);
        }
    }
}

using Domain.Entities;
using Domain.Model;
using Domain.Shared.Enums;
using FeedbackManagementWeb.Helper;
using FeedbackManagementWeb.Interface;
using FeedbackManagementWeb.Pages;
using FeedbacksManagementApi.Model;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime;
using System.Text;

namespace FeedbackManagementWeb.Services
{
    public class FeedbackService : ApiServiceBase, IFeedbackService
    {
        public FeedbackService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public async Task<IEnumerable<FeedbackReport>> GetFeedbacks(int take, int skip , FeedbackState state)
        {
            var client = _httpClientFactory.CreateClient("HttpClientWithSSLUntrusted");
            var response = await client.GetAsync($"{AppSettings.ApiBaseUrl}/api/Feedbacks/GetFeedbacks?take={take}&skip={skip}&state={state}");
            return await new ApiResult<FeedbackReport>().GetResultAsList(response);
        }

        public async Task<int> GetFeedbackCount(FeedbackState state)
        {
            var client = _httpClientFactory.CreateClient("HttpClientWithSSLUntrusted");
            var response = await client.GetAsync($"{AppSettings.ApiBaseUrl}/api/Feedbacks/GetFeedbacksCount?state={state}");
            return await new ApiResult<int>().GetResultAsObject(response);
        }

        public async Task<IEnumerable<FeedbackReport>> GetFeedbackReport(FeedbackReportFilterModel filter)
        {
            var client = _httpClientFactory.CreateClient("HttpClientWithSSLUntrusted");

            var response = await client.PostAsJsonAsync($"{AppSettings.ApiBaseUrl}/api/Feedbacks/GetFeedbackReports", filter);
            return await new ApiResult<FeedbackReport>().GetResultAsList(response);
        }

        public async Task<int> GetFeedbackReportCount(FeedbackReportFilterModel filter)
        {
            var client = _httpClientFactory.CreateClient("HttpClientWithSSLUntrusted");

            var response = await client.PostAsJsonAsync($"{AppSettings.ApiBaseUrl}/api/Feedbacks/GetFeedbackReportCount", filter);
            return await new ApiResult<int>().GetResultAsObject(response);
        }

        public async Task<FeedbackReport> GetFeedbackBySerialNumber(string serialNumber)
        {
            var client = _httpClientFactory.CreateClient("HttpClientWithSSLUntrusted");
            var response = await client.GetAsync($"{AppSettings.ApiBaseUrl}/api/Feedbacks/GetFeedbackDetailsBySerialNumber?serialNumber={serialNumber}");
            return await new ApiResult<FeedbackReport>().GetResultAsObject(response);
        }

        public async Task<FeedbackReport> GetFeedbackById(int feedbackId)
        {
            var client = _httpClientFactory.CreateClient("HttpClientWithSSLUntrusted");
            var response = await client.GetAsync($"{AppSettings.ApiBaseUrl}/api/Feedbacks/GetFeedbackById/feedbackId?feedbackId={feedbackId}");
            return await new ApiResult<FeedbackReport>().GetResultAsObject(response);
        }

        public async Task<IEnumerable<Tag>> GetTagsForOneFeedback(int feedbackId)
        {
            var client = _httpClientFactory.CreateClient("HttpClientWithSSLUntrusted");
            var response = await client.GetAsync($"{AppSettings.ApiBaseUrl}/api/Feedbacks/GetTagsForOneFeedback/feedbackId?feedbackId={feedbackId}");
            return await new ApiResult<Tag>().GetResultAsList(response);
        }

        public async Task AddfeedbackByExpert(FeedbackBase feedback , int expertId)
        {
            var client = _httpClientFactory.CreateClient("HttpClientWithSSLUntrusted");
            var response = await client.PostAsJsonAsync($"{AppSettings.ApiBaseUrl}/api/Feedbacks/AddFeedbackByExpert/expertId?expertId={expertId}", feedback);
            await new ApiResult<string>().GetResultAsVoid(response);
        }

        public async Task UpdateFeedback(FeedbackBase feedback, int feedbackId)
        {
            var client = _httpClientFactory.CreateClient("HttpClientWithSSLUntrusted");

            var content = new StringContent(JsonConvert.SerializeObject(feedback), Encoding.UTF8, "application/json");

            var response = await client.PatchAsync($"{AppSettings.ApiBaseUrl}/api/Feedbacks/UpdateFeedback/feedbackId?feedbackId={feedbackId}", content);
            await new ApiResult<string>().GetResultAsVoid(response);
        }

        public async Task DeleteFeedbacks(int[] feedbackIds)
        {
            var client = _httpClientFactory.CreateClient("HttpClientWithSSLUntrusted");
            var request = new HttpRequestMessage
            {
                Content = JsonContent.Create(feedbackIds),
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"{AppSettings.ApiBaseUrl}/api/Feedbacks/DeleteFeedbacks")
            };
            var response = await client.SendAsync(request);
            await new ApiResult<string>().GetResultAsVoid(response);
        }

        public async Task ArchiveFeedbacks(int[] feedbackIds)
        {
            var client = _httpClientFactory.CreateClient("HttpClientWithSSLUntrusted");
            var response = await client.PostAsJsonAsync($"{AppSettings.ApiBaseUrl}/api/Feedbacks/ArchiveFeedbacks", feedbackIds);
            await new ApiResult<string>().GetResultAsVoid(response);
        }

        public async Task SendToReadyToSendCustomer(int[] feedbackIds)
        {
            var client = _httpClientFactory.CreateClient("HttpClientWithSSLUntrusted");
            var response = await client.PostAsJsonAsync($"{AppSettings.ApiBaseUrl}/api/Feedbacks/SendToReadyToSendCustomer", feedbackIds);
            await new ApiResult<string>().GetResultAsVoid(response);
        }

        public async Task RecycleFeedbacks(int[] feedbackIds)
        {
            var client = _httpClientFactory.CreateClient("HttpClientWithSSLUntrusted");
            var response = await client.PostAsJsonAsync($"{AppSettings.ApiBaseUrl}/api/Feedbacks/RecycleFeedbacks", feedbackIds);
            await new ApiResult<string>().GetResultAsVoid(response);
        }

        public async Task SubmitFeedbacksToExpert(SubmitFeedbacksToExpertRequestModel feedbackRequest)
        {
            var client = _httpClientFactory.CreateClient("HttpClientWithSSLUntrusted");
            var response = await client.PostAsJsonAsync($"{AppSettings.ApiBaseUrl}/api/Feedbacks/SubmitFeedbacksToExpert", feedbackRequest);
            await new ApiResult<string>().GetResultAsVoid(response);
        }

        public async Task SubmitFeedbacksToExpertGroup(SubmitFeedbackToExpertGroupRequestModel feedbackRequest)
        {
            var client = _httpClientFactory.CreateClient("HttpClientWithSSLUntrusted");
            var response = await client.PostAsJsonAsync($"{AppSettings.ApiBaseUrl}/api/Feedbacks/SubmitFeedbacksToExpertGroup", feedbackRequest);
            await new ApiResult<string>().GetResultAsVoid(response);
        }

        public async Task SubmitFeedbackToCustomer(int[] feedbackIds)
        {
            var client = _httpClientFactory.CreateClient("HttpClientWithSSLUntrusted");
            var response = await client.PostAsJsonAsync($"{AppSettings.ApiBaseUrl}/api/Feedbacks/SubmitFeedbacksToCustomer", feedbackIds);
            await new ApiResult<string>().GetResultAsVoid(response);
        }

        public async Task AddTagForFeedback(AddTagToFeedbackRequestModel model)
        {
            var client = _httpClientFactory.CreateClient("HttpClientWithSSLUntrusted");
            var response = await client.PostAsJsonAsync($"{AppSettings.ApiBaseUrl}/api/Feedbacks/AddTagToFeedback", model);
            await new ApiResult<string>().GetResultAsVoid(response);
        }

        public async Task DeleteTagFromFeedback(int feedbackId , int tagId)
        {
            var client = _httpClientFactory.CreateClient("HttpClientWithSSLUntrusted");
            var response = await client
                .DeleteAsync($"{AppSettings.ApiBaseUrl}/api/Feedbacks/DeleteTagFromFeedback/feedbackId/tagId?feedbackId={feedbackId}&tagId={tagId}");
            await new ApiResult<string>().GetResultAsVoid(response);
        }

        public async Task ConfirmFeedbackAnswer(int feedbackId, int answerId)
        {
            var client = _httpClientFactory.CreateClient("HttpClientWithSSLUntrusted");
            var content = new StringContent(string.Empty, Encoding.UTF8, "application/json");

            var response = await client
                .PostAsync($"{AppSettings.ApiBaseUrl}/api/Feedbacks/ConfirmFeedbackAnswer/feedbackId/answerId?feedbackId={feedbackId}&answerId={answerId}",content );
            await new ApiResult<string>().GetResultAsVoid(response);
        }
    }
}

using Domain.Entities;
using FeedbackManagementWeb.Helper;

namespace FeedbackManagementWeb.Services
{
    public interface IAnswerService
    {
        Task AddAnswerForFeedback(AnsowerBase answer);
        Task<IEnumerable<Ansower>> GetAnswersForFeedback(int feedbackId);
    }

    public class AnswerService : ApiServiceBase, IAnswerService
    {
        public AnswerService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public async Task<IEnumerable<Ansower>> GetAnswersForFeedback(int feedbackId)
        {
            //_ = new ValidatorAtrrbute<ExamBase>(experiment);
            var client = _httpClientFactory.CreateClient("HttpClientWithSSLUntrusted");
            var response = await client.GetAsync($"{AppSettings.ApiBaseUrl}/api/Answer/GetAnswersFroFeedback?feedbackId={feedbackId}");
            return await new ApiResult<Ansower>().GetResultAsList(response);
        }

        public async Task AddAnswerForFeedback(AnsowerBase answer)
        {
            var client = _httpClientFactory.CreateClient("HttpClientWithSSLUntrusted");
            var response = await client.PostAsJsonAsync($"{AppSettings.ApiBaseUrl}/api/Answer/AddAnswerForFeedback", answer);
            await new ApiResult<string>().GetResultAsVoid(response);
        }


    }
}

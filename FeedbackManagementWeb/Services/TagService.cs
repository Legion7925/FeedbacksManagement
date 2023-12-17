using Domain.Entities;
using FeedbackManagementWeb.Helper;
using Newtonsoft.Json;
using System.Text;

namespace FeedbackManagementWeb.Services
{
    public interface ITagService
    {
        Task AddTag(Tag tag);
        Task DeleteTag(int tagID);
        Task<IEnumerable<Tag>> GetTags();
        Task UpdateTag(Tag tag, int tagId);
    }

    public class TagService : ApiServiceBase, ITagService
    {
        public TagService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public async Task<IEnumerable<Tag>> GetTags()
        {
            var client = _httpClientFactory.CreateClient("HttpClientWithSSLUntrusted");
            var response = await client.GetAsync($"{AppSettings.ApiBaseUrl}/api/Tags/GetTags");
            return await new ApiResult<Tag>().GetResultAsList(response);
        }

        public async Task AddTag(Tag tag)
        {
            var client = _httpClientFactory.CreateClient("HttpClientWithSSLUntrusted");
            var response = await client.PostAsJsonAsync($"{AppSettings.ApiBaseUrl}/api/Tags/AddTag", tag);
            await new ApiResult<string>().GetResultAsVoid(response);
        }

        public async Task UpdateTag(Tag tag, int tagId)
        {
            var client = _httpClientFactory.CreateClient("HttpClientWithSSLUntrusted");
            var response = await client.PutAsJsonAsync($"{AppSettings.ApiBaseUrl}/api/Tags/UpdateTag/{tagId}", tag);
            await new ApiResult<string>().GetResultAsVoid(response);
        }

        public async Task DeleteTag(int tagID)
        {
            var client = _httpClientFactory.CreateClient("HttpClientWithSSLUntrusted");
            var response = await client.DeleteAsync($"{AppSettings.ApiBaseUrl}/api/Tags/DeleteTag/{tagID}");
            await new ApiResult<string>().GetResultAsVoid(response);
        }

    }
}

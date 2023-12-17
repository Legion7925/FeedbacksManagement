using Domain.Entities;
using Domain.Model;
using FeedbackManagementWeb.Helper;
using Newtonsoft.Json;
using System.Text;
using static MudBlazor.Icons.Custom;

namespace FeedbackManagementWeb.Services
{
    public interface IKnowledgeTreeService
    {
        Task AddBranch(BaseKnowledgeTree branch);
        Task DeleteBranch(int branchId);
        Task<HashSet<KnowledgeTreeReport>> GetKnowledgeTree();
        Task UpdateBranch(BaseKnowledgeTree branch, int branchId);
        Task<IEnumerable<ExpertsBranchModel>> GetBranchExperts(int branchId);
        Task<IEnumerable<TagsBranchModel>> GetBranchTags(int branchId);
        Task AddTagToBranch(Tag tag, int branchId);
        Task AddExpertToBranch(int expertId, int branchId);
        Task DeleteExpertFromBranch(int relationId);
        Task UpdateExpertBranch(UpdateExpertBranchRequestModel updateExpertBranchRequestModel);
        Task DeleteTagFromBranch(int relationId);
    }

    public class KnowledgeTreeService : ApiServiceBase, IKnowledgeTreeService
    {
        public KnowledgeTreeService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public async Task<HashSet<KnowledgeTreeReport>> GetKnowledgeTree()
        {
            var client = _httpClientFactory.CreateClient("HttpClientWithSSLUntrusted");
            var response = await client.GetAsync($"{AppSettings.ApiBaseUrl}/api/KnowledgeTree/GetKnowledgeTrees");
            return await new ApiResult<KnowledgeTreeReport>().GetResultAsHashetList(response);
        }

        public async Task<IEnumerable<ExpertsBranchModel>> GetBranchExperts(int branchId)
        {
            var client = _httpClientFactory.CreateClient("HttpClientWithSSLUntrusted");
            var response = await client
                .GetAsync($"{AppSettings.ApiBaseUrl}/api/KnowledgeTree/GetExpertsForBranch/branchId?branchId={branchId}");
            return await new ApiResult<ExpertsBranchModel>().GetResultAsList(response);
        }

        public async Task<IEnumerable<TagsBranchModel>> GetBranchTags(int branchId)
        {
            var client = _httpClientFactory.CreateClient("HttpClientWithSSLUntrusted");
            var response = await client
                .GetAsync($"{AppSettings.ApiBaseUrl}/api/KnowledgeTree/GetTagsForBranch/branchId?branchId={branchId}");
            return await new ApiResult<TagsBranchModel>().GetResultAsList(response);
        }

        public async Task AddBranch(BaseKnowledgeTree branch)
        {
            var client = _httpClientFactory.CreateClient("HttpClientWithSSLUntrusted");
            var response = await client.PostAsJsonAsync($"{AppSettings.ApiBaseUrl}/api/KnowledgeTree/AddBranch", branch);
            await new ApiResult<string>().GetResultAsVoid(response);
        }

        public async Task AddTagToBranch(Tag tag , int branchId)
        {
            var client = _httpClientFactory.CreateClient("HttpClientWithSSLUntrusted");
            var response = await client
                .PostAsJsonAsync($"{AppSettings.ApiBaseUrl}/api/KnowledgeTree/AddTagToBranch/nodeId?nodeId={branchId}", tag);
            await new ApiResult<string>().GetResultAsVoid(response);
        }

        public async Task AddExpertToBranch(int expertId , int branchId)
        {
            var client = _httpClientFactory.CreateClient("HttpClientWithSSLUntrusted");
            var content = new StringContent(string.Empty, Encoding.UTF8, "application/json");
            var response = await client
                .PostAsJsonAsync($"{AppSettings.ApiBaseUrl}/api/KnowledgeTree/AddExpertToBranch/nodeId/expertId?nodeId={branchId}&expertId={expertId}",content);
            await new ApiResult<string>().GetResultAsVoid(response);
        }

        public async Task UpdateBranch(BaseKnowledgeTree branch, int branchId)
        {
            var client = _httpClientFactory.CreateClient("HttpClientWithSSLUntrusted");

            var content = new StringContent(JsonConvert.SerializeObject(branch), Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"{AppSettings.ApiBaseUrl}/api/KnowledgeTree/UpdateBranch/{branchId}", content);
            await new ApiResult<string>().GetResultAsVoid(response);
        }

        public async Task UpdateExpertBranch(UpdateExpertBranchRequestModel updateExpertBranchRequestModel)
        {
            var client = _httpClientFactory.CreateClient("HttpClientWithSSLUntrusted");

            var content = new StringContent(JsonConvert.SerializeObject(updateExpertBranchRequestModel), Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"{AppSettings.ApiBaseUrl}/api/KnowledgeTree/UpdateExpertBranch", content);
            await new ApiResult<string>().GetResultAsVoid(response);
        }

        public async Task DeleteBranch(int branchId)
        {
            var client = _httpClientFactory.CreateClient("HttpClientWithSSLUntrusted");
            var response = await client.DeleteAsync($"{AppSettings.ApiBaseUrl}/api/KnowledgeTree/DeleteBranch/{branchId}");
            await new ApiResult<string>().GetResultAsVoid(response);
        }

        public async Task DeleteExpertFromBranch(int relationId)
        {
            var client = _httpClientFactory.CreateClient("HttpClientWithSSLUntrusted");
            var response = await client.DeleteAsync($"{AppSettings.ApiBaseUrl}/api/KnowledgeTree/DeleteExpertFromBranch/{relationId}");
            await new ApiResult<string>().GetResultAsVoid(response);
        }

        public async Task DeleteTagFromBranch(int relationId)
        {
            var client = _httpClientFactory.CreateClient("HttpClientWithSSLUntrusted");
            var response = await client.DeleteAsync($"{AppSettings.ApiBaseUrl}/api/KnowledgeTree/DeleteTagFromBranch/{relationId}");
            await new ApiResult<string>().GetResultAsVoid(response);
        }

    }
}

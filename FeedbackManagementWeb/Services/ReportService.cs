using Domain.Entities;
using FeedbackManagementWeb.Helper;
using System.Net.Http;

namespace FeedbackManagementWeb.Services
{
    public interface IReportService
    {
        Task<IEnumerable<Customer>> GetCustomers();
        Task<IEnumerable<Product>> GetProdcuts();
        Task<IEnumerable<Specialty>> GetSpecialties();
    }

    public class ReportService : ApiServiceBase, IReportService
    {
        public ReportService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public async Task<IEnumerable<Product>> GetProdcuts()
        {
            var client = _httpClientFactory.CreateClient("HttpClientWithSSLUntrusted");
            var response = await client.GetAsync($"{AppSettings.ApiBaseUrl}/api/Products/GetProducts");
            return await new ApiResult<Product>().GetResultAsList(response);
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            var client = _httpClientFactory.CreateClient("HttpClientWithSSLUntrusted");
            var response = await client.GetAsync($"{AppSettings.ApiBaseUrl}/api/Customers/GetCustomers");
            return await new ApiResult<Customer>().GetResultAsList(response);
        }
        public async Task<IEnumerable<Specialty>> GetSpecialties()
        {
            var client = _httpClientFactory.CreateClient("HttpClientWithSSLUntrusted");
            var response = await client.GetAsync($"{AppSettings.ApiBaseUrl}/api/Specialties/GetSpecialties");
            return await new ApiResult<Specialty>().GetResultAsList(response);
        }
    }
}

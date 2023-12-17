using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http.Json;
using Azure;
using Infrastructure.Exceptions;

namespace FeedbackManagementWeb.Services;

public class ApiResult<T>
{

    private readonly JsonSerializerOptions _settingJson = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter() }
    };

    private void HandleHttpResponse(HttpStatusCode statusCode, string content)
    {
        if (statusCode == HttpStatusCode.BadRequest) throw new AppException(content);
        else if (statusCode == HttpStatusCode.NotFound) throw new AppException("داده مورد نظر یافت نشد");
        else if (statusCode == HttpStatusCode.Forbidden) throw new AppException("دسترسی شما به این بخش محدود شده است");
        else
        {
            //LogSvave.WriteLog("ApiCall", content);
            throw new AppException("در عملکرد سیستم مشکلی رخ داده است");
        }
    }

    public async Task<IEnumerable<T>> GetResultAsList(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            using (var responseStream = await response.Content.ReadAsStreamAsync())
            {
                return await JsonSerializer.DeserializeAsync<List<T>>(responseStream, _settingJson) ?? new List<T>();
            }
        }
        HandleHttpResponse(response.StatusCode, await response.Content.ReadAsStringAsync());

        return default!;
    }  

    public async Task<HashSet<T>> GetResultAsHashetList(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            using (var responseStream = await response.Content.ReadAsStreamAsync())
            {
                return await JsonSerializer.DeserializeAsync<HashSet<T>>(responseStream, _settingJson) ?? new HashSet<T>();
            }
        }
        HandleHttpResponse(response.StatusCode, await response.Content.ReadAsStringAsync());

        return default!;
    }




    public async Task<T> GetResultAsObject(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(result, _settingJson)!;
        }
        HandleHttpResponse(response.StatusCode, await response.Content.ReadAsStringAsync());
        return default!;
    }

    public async Task GetResultAsVoid(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode) return;
        HandleHttpResponse(response.StatusCode, await response.Content.ReadAsStringAsync());
    }

    public async Task<string> GetResultAsJsonString(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            return result ?? string.Empty;
        }
        HandleHttpResponse(response.StatusCode, await response.Content.ReadAsStringAsync());
        return string.Empty;
    }
}
